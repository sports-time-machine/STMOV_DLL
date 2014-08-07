using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Commons
{
    [TestClass]
    public class Matrix4x4Test
    {
        /// <summary>
        /// 零行列が作成できること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Matrix4x4 mat = new Matrix4x4();

            Assert.AreEqual(0.0f, mat.m[0, 0]);
            Assert.AreEqual(0.0f, mat.m[1, 0]);
            Assert.AreEqual(0.0f, mat.m[2, 0]);
            Assert.AreEqual(0.0f, mat.m[3, 0]);

            Assert.AreEqual(0.0f, mat.m[0, 1]);
            Assert.AreEqual(0.0f, mat.m[1, 1]);
            Assert.AreEqual(0.0f, mat.m[2, 1]);
            Assert.AreEqual(0.0f, mat.m[3, 1]);

            Assert.AreEqual(0.0f, mat.m[0, 2]);
            Assert.AreEqual(0.0f, mat.m[1, 2]);
            Assert.AreEqual(0.0f, mat.m[2, 2]);
            Assert.AreEqual(0.0f, mat.m[3, 2]);

            Assert.AreEqual(0.0f, mat.m[0, 3]);
            Assert.AreEqual(0.0f, mat.m[1, 3]);
            Assert.AreEqual(0.0f, mat.m[2, 3]);
            Assert.AreEqual(0.0f, mat.m[3, 3]);
        }

        /// <summary>
        /// 指定された行列が作成できること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest02()
        {
            Matrix4x4 mat = new Matrix4x4(
                1.0f,2.0f,3.0f,4.0f,
                5.0f,6.0f,7.0f,8.0f,
                9.0f,10.0f,11.0f,12.0f,
                13.0f,14.0f,15.0f,16.0f
                );

            Assert.AreEqual(1.0f, mat.m[0, 0]);
            Assert.AreEqual(2.0f, mat.m[0, 1]);
            Assert.AreEqual(3.0f, mat.m[0, 2]);
            Assert.AreEqual(4.0f, mat.m[0, 3]);

            Assert.AreEqual(5.0f, mat.m[1, 0]);
            Assert.AreEqual(6.0f, mat.m[1, 1]);
            Assert.AreEqual(7.0f, mat.m[1, 2]);
            Assert.AreEqual(8.0f, mat.m[1, 3]);

            Assert.AreEqual(9.0f, mat.m[2, 0]);
            Assert.AreEqual(10.0f, mat.m[2, 1]);
            Assert.AreEqual(11.0f, mat.m[2, 2]);
            Assert.AreEqual(12.0f, mat.m[2, 3]);

            Assert.AreEqual(13.0f, mat.m[3, 0]);
            Assert.AreEqual(14.0f, mat.m[3, 1]);
            Assert.AreEqual(15.0f, mat.m[3, 2]);
            Assert.AreEqual(16.0f, mat.m[3, 3]);
        }

        /// <summary>
        /// Equalsが正しく機能すること.
        /// すべての行列の要素が等しいこと.
        /// </summary>
        [TestMethod]
        public void EqualsTest01()
        {
            Matrix4x4 a = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
                );

            Matrix4x4 b = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
                );

            Matrix4x4 c = new Matrix4x4(
                16, 15, 14, 13,
                12, 11, 10, 9,
                8, 7, 6, 5,
                4, 3, 2, 1
                );

            // 自分自身と等しいこと.
            Assert.IsTrue(a.Equals(a));
            
            // 同じ要素の行列と等しいこと.
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));

            // 異なる要素の行列と等しくないこと.
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(c.Equals(a));
            
        }

        /// <summary>
        /// 4次正方行列同士の掛け算が正しく行われること.
        /// </summary>
        [TestMethod]
        public void MultipliedTest01()
        {
            Matrix4x4 a = new Matrix4x4(
                1,2,3,4,
                5,6,7,8,
                9,10,11,12,
                13,14,15,16
                );

            Matrix4x4 b = new Matrix4x4(
                16, 15, 14, 13,
                12, 11, 10, 9,
                8, 7, 6, 5,
                4, 3, 2, 1
                );

            // A*B
            Matrix4x4 AmulB = a * b;
            Matrix4x4 AmulBExpected = new Matrix4x4(
                    80,70,60,50,
                    240,214,188,162,
                    400,358,316,274,
                    560,502,444,386
                );

            Assert.AreEqual(AmulBExpected, AmulB);

            // B*A
            Matrix4x4 BmulA = b * a;
            Matrix4x4 BmulAExpected = new Matrix4x4(
                    386,444,502,560,
                    274,316,358,400,
                    162,188,214,240,
                    50,60,70,80
                );

            Assert.AreEqual(BmulAExpected, BmulA);
            
        }

        /// <summary>
        /// 4次正方行列とVector4の掛け算が正しくできること.
        /// </summary>
        [TestMethod]
        public void MultipliedTest02()
        {
            Matrix4x4 a = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
                );

            Vector4 b = new Vector4(1, 2, 3, 4);
            Vector4 BmulAExpected = new Vector4(30, 70, 110, 150);

            Assert.AreEqual(BmulAExpected, b * a);

            // 本来、数学的には計算できない4次正方行列*Vector4は、
            // Vector4*4次正方行列として扱う.
            Assert.AreEqual(BmulAExpected, a * b);
            
        }
    }
}
