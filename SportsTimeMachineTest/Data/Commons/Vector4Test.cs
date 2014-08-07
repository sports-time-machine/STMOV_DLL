using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Commons
{
    [TestClass]
    public class Vector4Test
    {
        /// <summary>
        /// 4次元のデータが正しく構築されていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Vector4 vec = new Vector4(3.5f, 5.5f, 2.3f, 1.0f);

            Assert.AreEqual(3.5f, vec.x);
            Assert.AreEqual(5.5f, vec.y);
            Assert.AreEqual(2.3f, vec.z);
            Assert.AreEqual(1.0f, vec.w);

        }
    }
}
