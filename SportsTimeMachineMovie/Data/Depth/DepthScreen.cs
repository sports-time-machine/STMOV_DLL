using System;
using System.Collections.Generic;

namespace SportsTimeMachine.Data.Depth
{
	/// <summary>
	/// カメラが投影されるスクリーン上の深度情報を表す.
	/// </summary>
    internal class DepthScreen
	{
		/// <summary>
		/// スクリーン横解像度.
		/// </summary>
		public int Width{get; private set;}

		/// <summary>
		/// スクリーン縦解像度.
		/// </summary>
		public int Height {get; private set;}	

		/// <summary>
		/// 深度情報のリスト.
		/// </summary>
		public List<DepthPosition> DepthList {get; private set;}

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="width">スクリーン横解像度</param>
		/// <param name="height">スクリーン縦解像度</param>
		public DepthScreen (int width, int height)
		{
			Width = width;
			Height = height;
			DepthList = new List<DepthPosition>(Width * Height);
		}

	}
}

