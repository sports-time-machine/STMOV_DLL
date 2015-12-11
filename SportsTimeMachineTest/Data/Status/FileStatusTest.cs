using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Units;

namespace SportsTimeMachineTest.Data.Status
{
    [TestClass]
    public class FileStatusTest
    {
        /// <summary>
        /// 構築できること.
        /// </summary>
        [TestMethod]
        public void ConstactorTest01()
        {
            Signature signature = new Signature("STMOV");
            SportsTimeMachine.Data.Units.Version version = new SportsTimeMachine.Data.Units.Version(1, 0);
            int totalFrames = 10000;
            int totalTime = 10000;
            Format2D10BD6BL format = new Format2D10BD6BL();
            CameraStatus leftCamera = new CameraStatus();
            CameraStatus rightCamera = new CameraStatus();

            FileStatus file = new FileStatus(signature, version, totalFrames, totalTime, format, leftCamera, rightCamera, 1.0f);

            Assert.AreEqual("STMOV", signature.Text);
            Assert.AreEqual("1.0", version.ToString());
        }

    }
}
