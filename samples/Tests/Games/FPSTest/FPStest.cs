﻿using System;
using NUnit.Framework;
using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Xenko.Games.Testing;

namespace FPSTest
{
    [TestFixture]
    public class FPSTest
    {
        private const string Path = "samplesGenerated\\FirstPersonShooter\\Bin\\Windows\\Debug\\FirstPersonShooter.exe";

#if TEST_ANDROID
        private const PlatformType TestPlatform = PlatformType.Android;
#elif TEST_IOS
        private const PlatformType TestPlatform = PlatformType.iOS;
#else
        private const PlatformType TestPlatform = PlatformType.Windows;
#endif

        [Test]
        public void TestLaunch()
        {
            using (var game = new GameTestingClient(Path, TestPlatform))
            {
                game.Wait(TimeSpan.FromMilliseconds(2000));
            }
        }

        [Test]
        public void TestInputs()
        {
            using (var game = new GameTestingClient(Path, TestPlatform))
            {
                game.Wait(TimeSpan.FromMilliseconds(2000));
                game.Tap(new Vector2(0.5f, 0.7f), TimeSpan.FromMilliseconds(500));
                game.Wait(TimeSpan.FromMilliseconds(500));
                game.KeyPress(Keys.Space, TimeSpan.FromMilliseconds(500));
                game.Wait(TimeSpan.FromMilliseconds(500));
                game.TakeScreenshot();
                game.Wait(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}