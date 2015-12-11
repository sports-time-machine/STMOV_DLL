using System;
using System.Collections.Generic;
using System.IO;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Transformer;
using SportsTimeMachine.Data.Units;
using SportsTimeMachine.Data.Commons;
using SportsTimeMachine.Data.Depth;

namespace SportsTimeMachine.Data.Frames{

	/// <summary>
	/// フレーム情報を扱うクラス.
	/// フレーム情報は圧縮された状態で格納されており,そのままでは使用することができない.
    /// DecompressDepthUnit関数を利用することにより,フレーム情報から点群データを取得することができる.
	/// </summary>
    public class FrameData 
	{
		/// <summary>
		/// フレーム情報バイト数を取得する.
		/// </summary>
		/// <value>フレーム情報バイト数.</value>
		public int Size{ get; private set; }

        /// <summary>
        /// フレーム情報バイト列.
        /// </summary>
        public byte[] Bytes { get; private set; }

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		public FrameData (byte[] bytes)
		{
			Bytes = bytes;
			Size = bytes.Length;
		}

        /// <summary>
        /// バイト列を取得する
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            List<byte> byteReturn = new List<byte>();
    
            // ボクセル数
            byte[] voxelCount = new byte[sizeof(UInt32)];
            voxelCount = BitConverter.GetBytes(0);

            // フレームのサイズ
            byte[] byteSize = new byte[sizeof(UInt32)];
            byteSize = BitConverter.GetBytes(Bytes.Length);

            // フレームデータ
            byte[] byteData = new byte[Bytes.Length];
            byteData = Bytes;

            byteReturn.AddRange(voxelCount);
            byteReturn.AddRange(byteSize);
            byteReturn.AddRange(byteData);

            return byteReturn.ToArray();
        }
	}
}

