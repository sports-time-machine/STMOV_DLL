using SportsTimeMachine.Data.Commons;
using System;

namespace SportsTimeMachine.Data.Depth
{

	/// <summary>
	/// スクリーン上の深度情報.
	/// </summary>
	internal class DepthPosition
	{
		/// <summary>
		/// 座標を取得する.
		/// </summary>
		/// <value>座標</value>
		public Vector2 Position{get; private set;}

		/// <summary>
		/// 深度情報を取得する.
		/// </summary>
		/// <value>深度情報</value>
		public int Depth{get; private set;}

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="pos">座標.</param>
		/// <param name="value">深度.</param>
		public DepthPosition (Vector2 pos, int depth)
		{
			Position = pos;
			Depth = depth;
		}
	}
}

