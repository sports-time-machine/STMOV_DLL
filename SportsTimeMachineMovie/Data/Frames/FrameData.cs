using System;
using System.Collections.Generic;
using System.IO;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Transformer;
using SportsTimeMachine.Data.Units;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachine.Data.Frames{

	/// <summary>
	/// フレーム情報を扱うクラス.
	/// フレーム情報は圧縮された状態で格納されており,そのままでは使用することができない.
    /// GetPointCloud関数を利用することにより,フレーム情報から点群データを取得することができる.
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
        private byte[] bytes;

		/// <summary>
		/// 圧縮情報フォーマット.
		/// </summary>
		private CompressFormat format;

        /// <summary>
        /// 深度情報変換クラス.
        /// </summary>
		private VoxcelTransformer transformer;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		/// <param name="format">圧縮フォーマット.</param>
        /// <param name="transformer">ボクセル変換方法.</param>
		public FrameData (byte[] bytes, CompressFormat format, VoxcelTransformer transformer)
		{
			this.bytes = bytes;
			Size = bytes.Length;
			this.format = format;
			this.transformer = transformer;
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍し,点群リストを作成する.
		/// </summary>
		/// <returns>The point cloud.</returns>
        public UnitPointCloud GetUnitPointCloud()
        {
            return new UnitPointCloud(transformer.GetVocelList(format.Decompress(bytes)));
		}
	}
}

