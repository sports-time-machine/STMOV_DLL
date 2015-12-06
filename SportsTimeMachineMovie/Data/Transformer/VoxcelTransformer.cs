using System;
using System.Collections.Generic;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachine.Data.Transformer
{
	/// <summary>
	/// 深度情報を三次元ボクセル情報に変換するクラス.
	/// カメラ2台で1つのスクリーンを投影させる標準的な
    /// スポーツタイムマシンの記録形式の変換を行う.
	/// </summary>
    public class VoxcelTransformer
	{
		/// <summary>
		/// 横方向解像度.
		/// </summary>
		private const int RESOLUTION_WIDTH = 640;

		/// <summary>
		/// 縦方向解像度.
		/// </summary>
		private const int RESOLUTION_HEIGHT = 480;

		/// <summary>
		/// カメラ1のカメラ情報.
		/// </summary>
		private CameraStatus camera1Info;

		/// <summary>
		/// カメラ2のカメラ情報.
		/// </summary>
        private CameraStatus camera2Info;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="camera1Info">カメラ1情報.</param>
		/// <param name="camera2Info">カメラ2情報.</param>
        public VoxcelTransformer(CameraStatus camera1Info, CameraStatus camera2Info)
		{
			this.camera1Info = camera1Info;
			this.camera2Info = camera2Info;
		}

		/// <summary>
		/// フレーム情報からボクセルのリストを作成する.
		/// </summary>
		/// <returns>ボクセルのリスト</returns>
		/// <param name="frame">フレーム.</param>
		public List<Vector3> GetVocelList(DepthUnit unit)
		{
			List<Vector3> voxcels = new List<Vector3>();
			voxcels.AddRange(GetScreenVoxcels(camera1Info, unit.LeftScreen));
			voxcels.AddRange(GetScreenVoxcels(camera2Info, unit.RightScreen));
			return voxcels;
		}

		/// <summary>
		/// カメラのスクリーンに投影された深度情報を元にボクセルのリストを作成する.
		/// </summary>
		/// <returns>ボクセルのリスト</returns>
		/// <param name="camera">カメラ情報.</param>
		/// <param name="depthList">深度情報のリスト.</param>
        private List<Vector3> GetScreenVoxcels(CameraStatus camera, DepthScreen screen)
        {
			int screenDepthCount = screen.DepthList.Count;
			List<Vector3> voxcels = new List<Vector3>(screenDepthCount);
			Matrix4x4 camMatrix = camera.GetMatrix();

			for (int i = 0; i < screenDepthCount; ++i){
				DepthPosition depth = screen.DepthList[i];

				Vector3 vec = new Vector3(
					(((RESOLUTION_WIDTH/2)- depth.Position.x)/(float)RESOLUTION_WIDTH),
					(((RESOLUTION_HEIGHT/2)- depth.Position.y)/(float)RESOLUTION_HEIGHT),
					depth.Depth/1000.0f
				);
				
				Vector4 vec4 = new Vector4(vec.x * vec.z,vec.y * vec.z, vec.z,1.0f);
				Vector4 point = camMatrix * vec4;
				voxcels.Add(new Vector3(point.x, point.y, point.z));
			}
			return voxcels;
		}
	}
}

