using System;
using SportsTimeMachineMovie.Data.Depth;

namespace SportsTimeMachineMovie.Data.Depth
{

	/// <summary>
	/// 1Unitの深度情報を表すクラス.
	/// 1Unitには2Screenある.
	/// </summary>
    internal class DepthUnit
	{
		/// <summary>
		/// 左スクリーン深度情報を取得する.
		/// </summary>
		public DepthScreen LeftScreen {get; private set;}

		/// <summary>
		/// 右スクリーン深度情報を取得する.
		/// </summary>
		public DepthScreen RightScreen {get; private set;}

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="width">スクリーンの横解像度</param>
		/// <param name="height">スクリーンの縦解像度</param>
		public DepthUnit(int width, int height)
		{
			LeftScreen = new DepthScreen(width, height);
			RightScreen = new DepthScreen(width, height);
		}
	}
}

