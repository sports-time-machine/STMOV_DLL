using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using SportsTimeMachineMovie.IO;
using SportsTimeMachineMovie.Data.Depth;

namespace SportsTimeMachineMovie.Data.Formats
{
	/// <summary>
	/// フォーマット depth 2d 10b/6b でランレングス圧縮されたデータを表すクラス.
	/// 各ピクセルは2バイトで表現され,ランレングス長6bit,深度値10bitで表される.
	/// また,解凍処理を高速に行うため,深度情報のクリッピングはこのクラスで行う.
	/// </summary>
    internal class Format2D10BD6BL : CompressFormat
	{
		/// <summary>
		/// ニアクリップ.
		/// </summary>
		private const int NEAR_CLIP = 0;

		/// <summary>
		/// ファークリップ.
		/// </summary>
		private const int FAR_CLIP = 8000;

		/// <summary>
		/// 圧縮されたデータのバイト数.
		/// </summary>
		private const int BYTE_SIZE = 2;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		public Format2D10BD6BL ()
		{
		}

		/// <summary>
		/// フォーマットの名称を取得する.
		/// </summary>
		/// <returns>The format name.</returns>
		public override String GetName(){
			return "depth 2d 10b/6b";
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <remarks>
		/// フレーム情報は,1Unit分の情報,つまり,カメラ2台分のスクリーンの情報を持っている.
		/// 解凍後の深度情報はは640*480のスクリーンを
		/// 左上から右下に走査するように記録されている.
		/// 現在の読んでいる位置をcountとすると,
		/// X座標はcount % WIDTH
		/// Y座標は(int)Math.Floor(count/(double)WIDTH)
		/// で表すことができる.
		/// </remarks>
		/// <param name="bytes">圧縮されたフレーム情報.</param>
		public override DepthUnit Decompress(byte[] bytes)
		{
			DepthUnit unit = new DepthUnit(WIDTH, HEIGHT);
			DepthScreen screen = unit.LeftScreen;
			
			int size = bytes.Length;
			int count = 0;
			
			for(int i=0; i < size; i+=BYTE_SIZE)
			{
				byte[] compressBytes = new byte[BYTE_SIZE];
				for (int j = 0; j < BYTE_SIZE; ++j){
					compressBytes[j] = bytes[i + j];
				}
				
				int first = compressBytes[0];
				int second = compressBytes[1];
				
				int runLength = (second >> 2) + 1;
				int depth = ((first) | ((second&0x03) << 8)) * 2502 >> 8;
				
				
				if (depth <= NEAR_CLIP || depth >= FAR_CLIP){
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
				if (count == WIDTH * HEIGHT){
					screen = unit.RightScreen;
					count=0;
				}
			}
			
			return unit;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
		private Vector2 GetPosition(int count){
			Vector2 vec = new Vector2();
			vec.x = count % WIDTH;
			vec.y = (int)Math.Floor(count/(double)WIDTH);
			return vec;
		}

	}

}

