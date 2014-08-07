using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Status;

namespace SportsTimeMachineTest.Data.Status
{
    [TestClass]
    public class UnitStatusTest
    {
        /// <summary>
        /// 総フレーム数と総ミリ秒からユニットステータスが構築できること.
        /// </summary>
        [TestMethod]
        public void ConstracorTest01()
        {
            int totalFrames = 100;
            int totalTime = 3333;

            UnitStatus unit = new UnitStatus(totalFrames, totalTime);

            Assert.AreEqual(100, unit.TotalFrameCount);
            Assert.AreEqual(3333, unit.TotalTime);   
        }
    }
}
