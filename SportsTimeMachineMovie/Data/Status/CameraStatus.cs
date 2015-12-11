using SportsTimeMachine.Data.Commons;
using System;
using System.Collections.Generic;
using System.IO;

namespace SportsTimeMachine.Data.Status
{
	/// <summary>
	/// ユニットに備え付けられたカメラの位置,回転,拡縮情報を扱う.
	/// </summary>
    public class CameraStatus
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
		public Vector3 Scale{ get; private set; }

        /// <summary>
        /// 既定のカメラ情報を構築する
        /// </summary>
        public CameraStatus()
        {
            Position = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(0, 0, 0);
        }

		/// <summary>
		/// カメラの位置、回転、拡縮を指定して構築する
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
				new Matrix4x4(
					1,0,0,0,
					0,1,0,0,
					0,0,1,0,
					0,0,0,1
					);

			// X軸回転
			float cos = (float)Math.Cos(Rotation.x);
			float sin = (float)Math.Sin(Rotation.x);
			mat =  
				new Matrix4x4(
					1,  0,    0,  0,
					0, cos, -sin, 0,
					0, sin, cos,  0,
					0,  0,    0,  1
				) * mat; 

			// Y軸回転
			cos = (float)Math.Cos(Rotation.y);
			sin = (float)Math.Sin(Rotation.y);
			mat =
                new Matrix4x4(
					cos,  0, sin, 0,
					0,    1,  0,  0,
					-sin, 0, cos, 0,
					0,    0,  0,  1
					) * mat;

			// Z軸回転
			cos = (float)Math.Cos(Rotation.z);
			sin = (float)Math.Sin(Rotation.z);
			mat =
                new Matrix4x4(
					cos, -sin, 0, 0,
					sin, cos, 0, 0,
					0, 0, 1, 0,
					0, 0, 0, 1
					) * mat;

			// 拡縮
			mat =
                new Matrix4x4(
					Scale.x,0,0,0,
					0,Scale.y,0,0,
					0,0,Scale.z,0,
					0,0,0,1
                    ) * mat;

			mat =
                new Matrix4x4(
					1,0,0,Position.x,
					0,1,0,Position.y,
					0,0,1,Position.z,
					0,0,0,1
                    ) * mat;
			return mat;
		}

        public byte[] ToBytes()
        {
            List<byte> bytes = new List<byte>();

            byte[] xBytes = new byte[sizeof(float)];
            byte[] yBytes = new byte[sizeof(float)];
            byte[] zBytes = new byte[sizeof(float)];

            xBytes = BitConverter.GetBytes(Position.x);
            yBytes = BitConverter.GetBytes(Position.y);
            zBytes = BitConverter.GetBytes(Position.z);

            bytes.AddRange(xBytes);
            bytes.AddRange(yBytes);
            bytes.AddRange(zBytes);

            xBytes = BitConverter.GetBytes(Rotation.x);
            yBytes = BitConverter.GetBytes(Rotation.y);
            zBytes = BitConverter.GetBytes(Rotation.z);

            bytes.AddRange(xBytes);
            bytes.AddRange(yBytes);
            bytes.AddRange(zBytes);

            xBytes = BitConverter.GetBytes(Scale.x);
            yBytes = BitConverter.GetBytes(Scale.y);
            zBytes = BitConverter.GetBytes(Scale.z);

            bytes.AddRange(xBytes);
            bytes.AddRange(yBytes);
            bytes.AddRange(zBytes);

            return bytes.ToArray();
        }
	}
}


