using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Formats;

namespace SportsTimeMachineTest.Data.Formats
{
    [TestClass]
    public class FormatFactoryTest
    {
        /// <summary>
        /// "depth 2d 10b/6b "の文字列から
        /// Format2D10BD6BL型が取得できること.
        /// </summary>
        [TestMethod]
        public void GetFormatTest01()
        {
            CompressFormat format = FormatFactory.GetFormat("depth 2d 10b/6b ");

            Assert.IsTrue(format is Format2D10BD6BL);
        }

        /// <summary>
        /// どのフォーマットにも対応しない文字列の場合、
        /// ArgumentExceptionが返却されること.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatTest02()
        {
            CompressFormat format = FormatFactory.GetFormat("unknown format ");
       }
    }
}
