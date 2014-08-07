using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Commons
{
    [TestClass]
    public class Vector3Test
    {
        /// <summary>
        /// 3次元のデータが正しく構築されていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Vector3 vec = new Vector3(3.5f, 5.5f, 2.2f);

            Assert.AreEqual(3.5f, vec.x);
            Assert.AreEqual(5.5f, vec.y);
            Assert.AreEqual(2.2f, vec.z);

        }
    }
}
