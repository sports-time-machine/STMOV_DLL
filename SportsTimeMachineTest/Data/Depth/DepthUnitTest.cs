using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Depth;

namespace SportsTimeMachineTest.Data.Depth
{
    [TestClass]
    public class DepthUnitTest
    {
        /// <summary>
        /// 縦解像度、横解像度を指定して
        /// 左右のスクリーンを持つDepthUnitが構築できること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            DepthUnit unit = new DepthUnit(640, 480);

            // 左右DepthUnitが640*480の解像度であり、深度リストの
            // キャパシティが307200であること.
            Assert.AreEqual(640, unit.LeftScreen.Width);
            Assert.AreEqual(480, unit.LeftScreen.Height);
            Assert.AreEqual(307200, unit.LeftScreen.DepthList.Capacity);
            Assert.AreEqual(0, unit.LeftScreen.DepthList.Count);

            Assert.AreEqual(640, unit.RightScreen.Width);
            Assert.AreEqual(480, unit.RightScreen.Height);
            Assert.AreEqual(307200, unit.RightScreen.DepthList.Capacity);
            Assert.AreEqual(0, unit.RightScreen.DepthList.Count);
        }
    }
}
