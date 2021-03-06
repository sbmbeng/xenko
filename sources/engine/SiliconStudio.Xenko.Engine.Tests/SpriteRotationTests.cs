using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering.Sprites;

namespace SiliconStudio.Xenko.Engine.Tests
{
    /// <summary>
    /// Tests that sprites with Orientation 90degrees are properly rendered and that their center is correctly calculated.
    /// Note: with texture packing there are 4 cases to test:
    ///   Before packing | After packing
    /// - Not Rotated   -> Not Rotated
    /// - Rotated       -> Not Rotated
    /// - Not Rotated   -> Rotated
    /// - Rotated       -> Rotated
    /// </summary>
    public class SpriteRotationTests : EngineTestBase
    {
        private const int ScreenWidth = 512;
        private const int ScreenHeight = 256;

        private readonly List<Entity> rotatedSprites = new List<Entity>();

        public SpriteRotationTests()
        {
            CurrentVersion = 2;
            GraphicsDeviceManager.PreferredBackBufferWidth = ScreenWidth;
            GraphicsDeviceManager.PreferredBackBufferHeight = ScreenHeight;
        }

        private static Entity CreateSpriteEntity(SpriteSheet sheet, string frameName)
        {
            return new Entity(frameName)
            {
                new SpriteComponent
                {
                    SpriteProvider = SpriteFromSheet.Create(sheet, frameName)
                }
            };
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            var rotationSheet = Content.Load<SpriteSheet>("RotationSheet");

            // create the rotated sprites
            var nrnr = CreateSpriteEntity(rotationSheet, "NRNR");
            var rnr = CreateSpriteEntity(rotationSheet, "RNR");
            var nrr = CreateSpriteEntity(rotationSheet, "NRR");
            var rr = CreateSpriteEntity(rotationSheet, "RR");

            // place the entities
            nrnr.Transform.Position = new Vector3(32, 16, 0);
            rnr.Transform.Position = new Vector3(32, 64 + 16, 0);
            nrr.Transform.Position = new Vector3(256 + 16, 224, 0);
            rr.Transform.Position = new Vector3(256 + 64 + 48, 32, 0);

            rotatedSprites.Add(nrnr);
            rotatedSprites.Add(rnr);
            rotatedSprites.Add(nrr);
            rotatedSprites.Add(rr);

            foreach (var rotatedSprite in rotatedSprites)
            {
                var center = CreateSpriteEntity(rotationSheet, "Round");
                center.Transform.Position = new Vector3(rotatedSprite.Transform.Position.XY(), 1);

                SceneSystem.SceneInstance.Scene.Entities.Add(rotatedSprite);
                SceneSystem.SceneInstance.Scene.Entities.Add(center);
            }

            CameraComponent.UseCustomProjectionMatrix = true;
            CameraComponent.ProjectionMatrix = Matrix.OrthoOffCenterRH(0, ScreenWidth, 0, ScreenHeight, -10, 10);
        }

        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.TakeScreenshot();
        }

        [Test]
        public void SpriteRender2DRun()
        {
            RunGameTest(new SpriteRotationTests());
        }

        public static void Main()
        {
            using (var game = new SpriteRotationTests())
                game.Run();
        }
    }
}
