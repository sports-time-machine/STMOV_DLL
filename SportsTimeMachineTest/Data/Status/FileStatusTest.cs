using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Status;

namespace SportsTimeMachineTest.Data.Status
{
    [TestClass]
    public class FileStatusTest
    {
        /// <summary>
        /// シグネチャ、バージョンを指定して構築できること.
        /// </summary>
        [TestMethod]
        public void ConstactorTest01()
        {
            string signature = "STMOV";
            string version = "1.0";

            FileStatus file = new FileStatus(signature, version);

            Assert.AreEqual("STMOV", signature);
            Assert.AreEqual("1.0", version);   
        }
    }
}
