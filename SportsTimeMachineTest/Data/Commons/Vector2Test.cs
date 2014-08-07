using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Commons
{
    [TestClass]
    public class Vector2Test
    {
        /// <summary>
        /// 2次元のデータが正しく構築されていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Vector2 vec = new Vector2(3.5f, 5.5f);

            Assert.AreEqual(3.5f, vec.x);
            Assert.AreEqual(5.5f, vec.y);

        }

    }
}
