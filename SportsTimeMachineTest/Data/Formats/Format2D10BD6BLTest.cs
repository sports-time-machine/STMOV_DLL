using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachineTest.Data.Formats
{
    [TestClass]
    public class Format2D10BD6BLTest
    {

        /// <summary>
        /// デフォルトコンストラクタで構築したとき,既定のデータが入っていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest01()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL();

            Assert.AreEqual(640, compress.Width);
            Assert.AreEqual(480, compress.Height);
            Assert.AreEqual(0, compress.NearClip);
            Assert.AreEqual(8000, compress.FarClip);
        }

        /// <summary>
        /// フレーム幅、高さを指定して構築したとき,既定のデータが入っていること.
        /// </summary>
        [TestMethod]        
        public void ConstractorTest02()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL(320,240);

            Assert.AreEqual(320, compress.Width);
            Assert.AreEqual(240, compress.Height);
            Assert.AreEqual(0, compress.NearClip);
            Assert.AreEqual(8000, compress.FarClip);
        }

        /// <summary>
        /// フレーム幅、高さ、ニアクリップ、ファークリップを指定して構築したとき,
        /// 既定のデータが入っていること.
        /// </summary>
        [TestMethod]
        public void ConstractorTest03()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL(320, 240, 100, 3000);

            Assert.AreEqual(320, compress.Width);
            Assert.AreEqual(240, compress.Height);
            Assert.AreEqual(100, compress.NearClip);
            Assert.AreEqual(3000, compress.FarClip);
        }

        /// <summary>
        /// フレーム幅、高さの値を0以下に指定した場合、ArgumentExceptionが送出されること.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstractorTestEx01()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL(0, -100);
        }

        /// <summary>
        /// ニアクリップ、ファークリップの値を0以下を指定した場合、
        /// ArgumentExceptionが送出されること.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstractorTestEx02()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL(640, 480, -100, 2000);
        }

        /// <summary>
        /// ニアクリップがファークリップの値を超えている場合
        /// ArgumentExceptionが送出されること.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstractorTestEx03()
        {
            Format2D10BD6BL compress = new Format2D10BD6BL(640, 480, 2000, 0);
        }

        /// <summary>
        /// フォーマット名を取得できること.
        /// </summary>
        [TestMethod]
        public void GetNameTest01()
        {
            CompressFormat compress = new Format2D10BD6BL();

            Assert.AreEqual("depth 2d 10b/6b", compress.GetName());
        }

        /// <summary>
        /// 圧縮されたフレーム情報を解凍できること.
        /// </summary>
        [TestMethod]
        public void DecompressTest01()
        {
            // テストのため、フレームを2*2にする.
            CompressFormat compress = new Format2D10BD6BL(2,2);

            // バイト情報.リトルエンディアンなので2バイト読み込むときは
            // 0,11は00001011(11)|00000000(0)となることに注意.
            // 左スクリーン.
            // 0,11 = 000010(3)|1100000000(768) = ラン(2+1)=3レングス(768)7506
            // 1,0  = 000000(0)|0000000001(1) = ラン(0+1)=1レングス(1)9
            // 右スクリーン.
            // 1,8  = 000010(3)|0000000001(1) = ラン(2+1)=3レングス(1)9
            // 0,3  = 000000(3)|1100000000(768) = ラン(0+1)=1レングス(768)7506
            byte[] bytes = {0,11,1,0,1,8,0,3};

            DepthUnit depth = compress.Decompress(bytes);


            Assert.AreEqual(new Vector2(0.0f,0.0f), depth.LeftScreen.DepthList[0].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[0].Depth);

            Assert.AreEqual(new Vector2(1.0f, 0.0f), depth.LeftScreen.DepthList[1].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[1].Depth);

            Assert.AreEqual(new Vector2(0.0f, 1.0f), depth.LeftScreen.DepthList[2].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[2].Depth);

            Assert.AreEqual(new Vector2(1.0f, 1.0f), depth.LeftScreen.DepthList[3].Position);
            Assert.AreEqual(9, depth.LeftScreen.DepthList[3].Depth);

            Assert.AreEqual(new Vector2(0.0f, 0.0f), depth.RightScreen.DepthList[0].Position);
            Assert.AreEqual(9, depth.RightScreen.DepthList[0].Depth);

            Assert.AreEqual(new Vector2(1.0f, 0.0f), depth.RightScreen.DepthList[1].Position);
            Assert.AreEqual(9, depth.RightScreen.DepthList[1].Depth);

            Assert.AreEqual(new Vector2(0.0f, 1.0f), depth.RightScreen.DepthList[2].Position);
            Assert.AreEqual(9, depth.RightScreen.DepthList[2].Depth);

            Assert.AreEqual(new Vector2(1.0f, 1.0f), depth.RightScreen.DepthList[3].Position);
            Assert.AreEqual(7506, depth.RightScreen.DepthList[3].Depth);
        }

        /// <summary>
        /// クリッピングされたデータは無視されること.
        /// </summary>
        [TestMethod]
        public void DecompressTest02()
        {
            // テストのため、フレームを2*2にする.
            CompressFormat compress = new Format2D10BD6BL(2, 2);

            // バイト情報.リトルエンディアンなので2バイト読み込むときは
            // 0,11は00001011(11)|00000000(0)となることに注意.
            // 0,11 = 000010(3)|1100000000(768) = ラン(2+1)=3レングス(768)7506
            // 0,0  = 000000(0)|0000000000(0) = ラン(0+1)=1レングス(0)0(無視される)
            // 0,8  = 000010(3)|0000000000(0) = ラン(2+1)=3レングス(0)0(無視される)
            // 0,3  = 000000(3)|1100000000(768) = ラン(0+1)=1レングス(768)7506
            byte[] bytes = { 0, 11, 0, 0, 0, 8, 0, 3 };

            DepthUnit depth = compress.Decompress(bytes);

            // 左スクリーンの(1,1)はクリッピング領域外なので無視される.
            Assert.AreEqual(3, depth.LeftScreen.DepthList.Count);

            Assert.AreEqual(new Vector2(0.0f, 0.0f), depth.LeftScreen.DepthList[0].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[0].Depth);

            Assert.AreEqual(new Vector2(1.0f, 0.0f), depth.LeftScreen.DepthList[1].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[1].Depth);

            Assert.AreEqual(new Vector2(0.0f, 1.0f), depth.LeftScreen.DepthList[2].Position);
            Assert.AreEqual(7506, depth.LeftScreen.DepthList[2].Depth);

            // 右スクリーンの(0,0),(1,0),(0,1)はクリッピング領域外なので無視される.
            Assert.AreEqual(1, depth.RightScreen.DepthList.Count);

            Assert.AreEqual(new Vector2(1.0f, 1.0f), depth.RightScreen.DepthList[0].Position);
            Assert.AreEqual(7506, depth.RightScreen.DepthList[0].Depth);
        }

    }
}
