using System;
using System.Collections.Generic;
using SportsTimeMachine.Data.Depth;

namespace SportsTimeMachine.Data.Formats
{
	/// <summary>
	/// 深度情報圧縮フォーマットの抽象クラス.
	/// 深度情報圧縮フォーマットのクラスを作成する際はこれを継承する.
	/// </summary>
    internal abstract class CompressFormat
	{

		/// <summary>
		/// フレーム情報の幅.
		/// </summary>
        public int Width { get; private set; }
		
		/// <summary>
		/// フレーム情報の高さ.
		/// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// フレーム情報の幅と高さを指定して構築する.
        /// </summary>
        public CompressFormat(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("フレーム情報の幅と高さは0以下の値を指定することはできません.");
            
            Width = width;
            Height = height;
        }

		/// <summary>
		/// フォーマットの名称を取得する.
		/// </summary>
		/// <returns>フォーマット名を表す文字列.</returns>
		public abstract String GetName();
	
		/// <summary>
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		/// <returns>ユニット深度情報.</returns>
		public abstract DepthUnit Decompress(byte[] bytes);

	}
}

