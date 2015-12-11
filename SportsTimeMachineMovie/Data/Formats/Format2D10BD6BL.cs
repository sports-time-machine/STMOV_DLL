using System;
using System.IO;
using System.Collections.Generic;
using SportsTimeMachine.IO;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Commons;
using SportsTimeMachine.Data.Frames;

namespace SportsTimeMachine.Data.Formats
{
	/// <summary>
	/// フォーマット depth 2d 10b/6b でランレングス圧縮されたデータを表すクラス
	/// 各ピクセルは2バイトで表現され,ランレングス長6bit,深度値10bitで表される
	/// また,解凍処理を高速に行うため,深度情報のクリッピングはこのクラスで行う
	/// </summary>
    public class Format2D10BD6BL : CompressFormat
	{
		/// <summary>
		/// ニアクリップ
		/// </summary>
        public int NearClip { get; private set; }

		/// <summary>
		/// ファークリップ
		/// </summary>
        public int FarClip { get; private set; }

		/// <summary>
		/// 圧縮されたデータのバイト数
		/// </summary>
		private const int BYTE_SIZE = 2;

		/// <summary>
		/// 既定のフレームの幅、高さ、ファークリップ、ニアクリップで構築する
		/// </summary>
		public Format2D10BD6BL ()
            :base(640, 480)
		{
            NearClip = 0;
            FarClip = 8000;
		}

        /// <summary>
        /// フレームの幅と高さを指定して既定のファークリップ、ニアクリップで構築する
        /// </summary>
        /// <param name="width">フレーム幅.</param>
        /// <param name="height">フレーム高さ.</param>
        public Format2D10BD6BL(int width, int height)
            :base(width, height)
        {
            NearClip = 0;
            FarClip = 8000;
        }

        /// <summary>
        /// フレームの幅と高さ、ファークリップ、ニアクリップを指定して構築する
        /// </summary>
        /// <param name="width">フレーム幅</param>
        /// <param name="height">フレーム高さ</param>
        /// <param name="nearClip">ニアクリップ</param>
        /// <param name="farClip">ファークリップ</param>
        public Format2D10BD6BL(int width, int height, int nearClip, int farClip)
            :base(width, height)
        {
            if (nearClip <= 0 || farClip <= 0)
                throw new ArgumentException("ニアクリップおよびファークリップは0以下の値を指定することはできません.");

            if (nearClip > farClip)
                throw new ArgumentException("ニアクリップの値がファークリップの値を超えてはいけません.");

            NearClip = nearClip;
            FarClip = farClip;
        }


		/// <summary>
		/// フォーマットの名称を取得する
		/// </summary>
		/// <returns>The format name.</returns>
		public override String GetName(){
			return "depth 2d 10b/6b ";
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍する
		/// </summary>
		/// <remarks>
		/// フレーム情報は,1Unit分の情報,つまり,カメラ2台分のスクリーンの情報を持っている
		/// 解凍後の深度情報はWidth*Heightのスクリーンを
		/// 左上から右下に走査するように記録されている
		/// 現在の読んでいる位置をcountとすると,
		/// X座標はcount % Width
		/// Y座標は(int)Math.Floor(count/(double)Width)
		/// で表すことができる
		/// </remarks>
		/// <param name="bytes">圧縮されたフレーム情報</param>
		public override DepthUnit Decompress(FrameData frameData)
		{
			DepthUnit unit = new DepthUnit(Width, Height);
			DepthScreen screen = unit.LeftScreen;

            int size = frameData.Size;
			int count = 0;
			
			for(int i=0; i < size; i+=BYTE_SIZE)
			{
				byte[] compressBytes = new byte[BYTE_SIZE];
				for (int j = 0; j < BYTE_SIZE; ++j){
					compressBytes[j] = frameData.Bytes[i + j];
				}
				
				int first = compressBytes[0];
				int second = compressBytes[1];
				
				int runLength = (second >> 2) + 1;
				int depth = ((first) | ((second & 0x03) << 8)) * 2502 >> 8;
				
				if (depth <= NearClip || depth >= FarClip){
					// 深度がクリッピング深度に入っていれば深度情報に追加しない.
					// ラン分読み飛ばす.
					count+=runLength;
				}else{
					for (int j = 0; j < runLength; ++j)
					{
						// 深度情報を追加.
						screen.DepthList.Add(new DepthPosition(GetPosition(count),depth));
						count++;
					}
				}
				
				// 左のスクリーンの走査終了.右のスクリーンの走査を始める.
				if (count == Width * Height){
					screen = unit.RightScreen;
					count=0;
				}
			}
			
			return unit;
		}

        /// <summary>
        /// ユニット深度情報を圧縮する
        /// </summary>
        /// <param name="depthUnit">ユニット深度情報</param>
        /// <returns>フレームデータ</returns>
        public override FrameData Compress(DepthUnit depthUnit)
        {

            DepthScreen leftScreen = depthUnit.LeftScreen;
            DepthScreen rightScreen = depthUnit.RightScreen;

            int[,] leftScreenMap = new int[Width, Height];
            int[,] rightScreenMap = new int[Width, Height];

            foreach (DepthPosition item in leftScreen.DepthList)
            {
                leftScreenMap[(int)item.Position.x, (int)item.Position.y] = item.Depth;
            }

            foreach (DepthPosition item in rightScreen.DepthList)
            {
                rightScreenMap[(int)item.Position.x, (int)item.Position.y] = item.Depth;
            }

            List<byte> compressBytes = new List<byte>();


            int length = 0;
            int depth = leftScreenMap[0, 0];
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (leftScreenMap[i, j] == depth && length < 32)
                    {
                        length++;
                    }
                    else
                    {
                        compressBytes.AddRange(CreateRunLength(depth, length));

                        depth = leftScreenMap[i, j];
                        length = 1;
                    }
                }
            }

            compressBytes.AddRange(CreateRunLength(depth, length));

            length = 0;
            depth = rightScreenMap[0, 0];
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (rightScreenMap[i, j] == depth && length < 32)
                    {
                        length++;
                    }
                    else
                    {
                        int compressDepth = depth * 104 >> 10;
                        byte first = (byte)(compressDepth & 0xFF);
                        byte second = (byte)(compressDepth >> 8 | (length << 2));

                        compressBytes.Add(first);
                        compressBytes.Add(second);

                        depth = leftScreenMap[i, j];
                        length = 1;
                    }
                }
            }

            compressBytes.AddRange(CreateRunLength(depth, length));

            return new FrameData(compressBytes.ToArray());
        }

        private byte[] CreateRunLength(int run, int length)
        {
            byte[] bytes = new byte[2];

            int compressDepth = run * 104 >> 10;
            bytes[0] = (byte)(compressDepth & 0xFF);
            bytes[1] = (byte)(compressDepth >> 8 | (length << 2));

            return bytes;
        }

        /// <summary>
        /// カウント数からXY座標を取得する.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
		private Vector2 GetPosition(int count){
			Vector2 vec = new Vector2();
			vec.x = count % Width;
			vec.y = (int)Math.Floor(count/(double)Width);
			return vec;
		}

	}

}

