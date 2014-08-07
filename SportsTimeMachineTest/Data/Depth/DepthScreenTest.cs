using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Depth;

namespace SportsTimeMachineTest.Data.Depth
{
    [TestClass]
    public class DepthScreenTest
    {
        /// <summary>
        /// スクリーン横解像度と縦解像度を指定して、
        /// スクリーン上の深度情報を構築できること.
        /// 横解像度と、縦解像度、横解像度*縦解像度分のキャパシティを持った
        /// 深度リストが構築できていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            DepthScreen screen = new DepthScreen(640, 480);

            Assert.AreEqual(640, screen.Width);
            Assert.AreEqual(480, screen.Height);

            // 640*480 = 307200のキャパシティを持つ.
            Assert.AreEqual(307200, screen.DepthList.Capacity);

            // リストは空.
            Assert.AreEqual(0, screen.DepthList.Count);
        }
    }
}
