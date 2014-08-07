using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Status
{
    [TestClass]
    public class CameraStatusTest
    {
        /// <summary>
        /// カメラの位置、回転、拡縮から構築できること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Vector3 pos = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 rot = new Vector3(4.0f, 5.0f, 6.0f);
            Vector3 scale = new Vector3(7.0f, 8.0f, 9.0f);

            CameraStatus camera = new CameraStatus(pos, rot, scale);

            Assert.AreEqual(pos, camera.Position);
            Assert.AreEqual(rot, camera.Rotation);
            Assert.AreEqual(scale, camera.Scale);
        }

        /// <summary>
        /// カメラの行列を取得できること.
        /// </summary>
        [TestMethod]
        public void GetMatrixTest01()
        {
            Vector3 pos = new Vector3(10.0f, 20.0f, 30.0f);
            Vector3 rot = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);

            CameraStatus camera = new CameraStatus(pos, rot, scale);
            Matrix4x4 mat = camera.GetMatrix();

            Matrix4x4 expected = new Matrix4x4(
                1.0f, 0.0f, 0.0f, 10.0f,
                0.0f, 1.0f, 0.0f, 20.0f,
                0.0f, 0.0f, 1.0f, 30.0f,
                0.0f, 0.0f, 0.0f, 1.0f
                );

            Assert.AreEqual(expected, mat);
        }
    }
}
