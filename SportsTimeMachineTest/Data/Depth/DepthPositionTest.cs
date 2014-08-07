using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Depth
{
    [TestClass]
    public class DepthPositionTest
    {
        /// <summary>
        /// スクリーン上の深度情報が取得できること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            DepthPosition target = new DepthPosition(new Vector2(10.5f, 5.5f), 1000);

            Assert.AreEqual(10.5f, target.Position.x);
            Assert.AreEqual(5.5f, target.Position.y);
            Assert.AreEqual(1000, target.Depth);
        }
    }
}
