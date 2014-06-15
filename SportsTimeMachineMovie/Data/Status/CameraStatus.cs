using System;
using System.IO;
using UnityEngine;

namespace SportsTimeMachineMovie.Data.Status
{
	/// <summary>
	/// ユニットに備え付けられたカメラの位置,回転,拡縮情報を扱う.
	/// </summary>
    internal class CameraStatus
	{

		/// <summary>
		/// 位置情報を取得する.
		/// </summary>
		/// <value>位置</value>
		public Vector3 Position{ get; private set; }

		/// <summary>
		/// 回転情報を取得する.
		/// </summary>
		/// <value>回転.</value>
		public Vector3 Rotation{ get; private set; }

		/// <summary>
		/// 拡縮情報を取得する.
		/// </summary>
		/// <value>拡縮.</value>
		public UnityEngine.Vector3 Scale{ get; private set; }

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="pos">位置.</param>
		/// <param name="rotate">回転.</param>
		/// <param name="scale">拡縮.</param>
		public CameraStatus(Vector3 pos, Vector3 rotate, Vector3 scale)
		{
			Position = pos;
			Rotation = rotate;
			Scale = scale;
		}

		/// <summary>
		/// カメラの行列を取得する.
		/// 行列をかける順番はX軸回転,Y軸回転,Z軸回転,拡縮,平行移動
		/// の順番である.
		/// </summary>
		/// <returns>カメラから得られる行列.</returns>
		public Matrix4x4 GetMatrix(){

			Matrix4x4 mat = 
				CreateMatrix4x4(
					1,0,0,0,
					0,1,0,0,
					0,0,1,0,
					0,0,0,1
					);

			// X軸回転
			float cos = (float)Math.Cos(Rotation.x);
			float sin = (float)Math.Sin(Rotation.x);
			mat =  
				CreateMatrix4x4(
					1,  0,    0,  0,
					0, cos, -sin, 0,
					0, sin, cos,  0,
					0,  0,    0,  1
				) * mat;

			// Y軸回転
			cos = (float)Math.Cos(Rotation.y);
			sin = (float)Math.Sin(Rotation.y);
			mat =  
				CreateMatrix4x4(
					cos,  0, sin, 0,
					0,    1,  0,  0,
					-sin, 0, cos, 0,
					0,    0,  0,  1
					) * mat;

			// Z軸回転
			cos = (float)Math.Cos(Rotation.z);
			sin = (float)Math.Sin(Rotation.z);
			mat =  
				CreateMatrix4x4(
					cos, -sin, 0, 0,
					sin, cos, 0, 0,
					0, 0, 1, 0,
					0, 0, 0, 1
					) * mat;

			// 拡縮
			mat = 
				CreateMatrix4x4(
					Scale.x,0,0,0,
					0,Scale.y,0,0,
					0,0,Scale.z,0,
					0,0,0,1) * mat;

			mat = 
				CreateMatrix4x4(
					1,0,0,Position.x,
					0,1,0,Position.y,
					0,0,1,Position.z,
					0,0,0,1) * mat;
			return mat;
		}

		/// <summary>
		/// 行列を作成する.
		/// </summary>
		/// <returns>行列.</returns>
		private Matrix4x4 CreateMatrix4x4(
			float m00, float m01, float m02, float m03,
			float m10, float m11, float m12, float m13,
			float m20, float m21, float m22, float m23,
			float m30, float m31, float m32, float m33
		){
			Matrix4x4 mat = new Matrix4x4();
			mat.m00 = m00;
			mat.m10 = m10;
			mat.m20 = m20;
			mat.m30 = m30;
			mat.m01 = m01;
			mat.m11 = m11;
			mat.m21 = m21;
			mat.m31 = m31;
			mat.m02 = m02;
			mat.m12 = m12;
			mat.m22 = m22;
			mat.m32 = m32;
			mat.m03 = m03;
			mat.m13 = m13;
			mat.m23 = m23;
			mat.m33 = m33;
			return mat;
		}
	}
}


