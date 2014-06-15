using System;
using System.Collections.Generic;
using SportsTimeMachineMovie.Data.Depth;

namespace SportsTimeMachineMovie.Data.Formats
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
		public const int WIDTH = 640;
		
		/// <summary>
		/// フレーム情報の高さ.
		/// </summary>
		public const int HEIGHT = 480;
	
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

