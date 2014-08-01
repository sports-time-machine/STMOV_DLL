using System;
using System.IO;
using System.Collections.Generic;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Transformer;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Frames;
using System.Text;
using SportsTimeMachine.Data.Units;
using SportsTimeMachine.Data.Commons;

namespace SportsTimeMachine.IO
{
	/// <summary>
	/// stmovデータからデータを読み取るクラス.
	/// </summary>
	public class UnitReader : IDisposable
	{
        /// <summary>
        /// インスタンスが破棄されたかどうか.
        /// </summary>
		private bool disposed;

        /// <summary>
        /// ストリーム.
        /// </summary>
		private Stream stream;


        /// <summary>
        /// ファイルパスからReaderを構築する.
        /// </summary>
        /// <param name="filename"></param>
        public UnitReader(String filepath)
            : this(new FileStream(filepath, FileMode.Open))
        {

        }

		/// <summary>
		/// ストリームからReaderを構築する.
		/// </summary>
		/// <param name="stream">ストリーム.</param>
		public UnitReader (Stream stream)
		{
			disposed = false;
			this.stream = stream;
		}

        /// <summary>
        /// スポーツタイムマシンユニットデータを読み込む.
        /// </summary>
        /// <returns>ユニットデータ.</returns>
        public Unit Read()
        {
            FileStatus fileStatus = new FileStatus(ReadSignature(), ReadVersion());
            UnitStatus movieStatus = new UnitStatus(ReadTotalFrames(), ReadTotalMilliSeconds());
            List<FrameData> frames = ReadFrames();

            return new Unit(frames, fileStatus, movieStatus);
        }

		/// <summary>
		/// シグネチャを読み込む.
		/// </summary>
		/// <returns>シグネチャ.</returns>
		private String ReadSignature()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			stream.Seek(0,SeekOrigin.Begin);
			byte[] bytes = new byte[6];
			stream.Read (bytes, 0, 6);
			String signature = Encoding.ASCII.GetString (bytes);
			return signature;
		}

		/// <summary>
		/// バージョンを読み込む.
		/// </summary>
		/// <returns>バージョン</returns>
        private String ReadVersion()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			stream.Seek (6, SeekOrigin.Begin);
			int majorVersion = stream.ReadByte();
			int minorVersion = stream.ReadByte();
			String version = majorVersion + "." + minorVersion; 
			return version;
		}

		/// <summary>
		/// 総フレーム数を読み込む.
		/// </summary>
		/// <returns>総フレーム数</returns>
        private int ReadTotalFrames()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			stream.Seek (8, SeekOrigin.Begin);
			byte[] bytes = new byte[sizeof(int)];
			stream.Read (bytes, 0, sizeof(int));
			int totalFrames = BitConverter.ToInt32(bytes, 0);
			return totalFrames;
		}

		/// <summary>
		/// 総ミリ秒を読み込む.
		/// </summary>
		/// <returns>総ミリ秒</returns>
        private int ReadTotalMilliSeconds()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			stream.Seek (12, SeekOrigin.Begin);
			byte[] bytes = new byte[sizeof(int)];
			stream.Read (bytes, 0, sizeof(int));
			int totalFrames = BitConverter.ToInt32(bytes, 0);
			return totalFrames;
		}

		/// <summary>
		/// 深度情報圧縮フォーマットを読み込む.
		/// </summary>
		/// <returns>圧縮フォーマット</returns>
        private CompressFormat ReadCompressFormat()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			stream.Seek (16, SeekOrigin.Begin);
			byte[] bytes = new byte[16];
			stream.Read (bytes, 0, 16);
			string formatString = Encoding.ASCII.GetString(bytes);
			CompressFormat format = FormatFactory.GetFormat(formatString);
			return format;
		}

		/// <summary>
		/// 左カメラ情報を読み込む.
		/// </summary>
		/// <returns>カメラ情報</returns>
        private CameraStatus ReadLeftCameraInfo()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);
			stream.Seek (32, SeekOrigin.Begin);
			return ReadCameraInfo();
		}

		/// <summary>
		/// 右カメラ情報を読み込む.
		/// </summary>
		/// <returns>カメラ情報.</returns>
        private CameraStatus ReadRightCameraInfo()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);
			stream.Seek (68, SeekOrigin.Begin);
			return ReadCameraInfo();
		}

        /// <summary>
        /// カメラ情報読み込み.
        /// </summary>
        /// <returns>カメラ情報</returns>
        private CameraStatus ReadCameraInfo()
        {
			byte[] xBytes = new byte[sizeof(float)];
			byte[] yBytes = new byte[sizeof(float)];
			byte[] zBytes = new byte[sizeof(float)];
			
			// カメラ情報
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 pos = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 rot = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 scale = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			CameraStatus info = new CameraStatus (pos, rot, scale);
			return info;
		}

		/// <summary>
		/// すべてのフレーム情報を読み込む.
		/// </summary>
		/// <returns>フレーム情報のリスト</returns>
        private List<FrameData> ReadFrames()
        {
			if (disposed) throw new ObjectDisposedException(GetType().FullName);

			List<FrameData> frames = new List<FrameData>();

			int totalFrames = ReadTotalFrames();
			CompressFormat format = ReadCompressFormat ();

			VoxcelTransformer transformer = new VoxcelTransformer( ReadLeftCameraInfo(), ReadRightCameraInfo());

			stream.Seek (108, SeekOrigin.Begin);

			for (int i=0; i < totalFrames ; i++) 
			{
				// ボクセル数.
				byte[] voxcelCountBuffer = new byte[sizeof(Int32)];
				stream.Read(voxcelCountBuffer, 0, sizeof(Int32));

				// フレームのサイズ
				byte[] voxcelSizeBuffer = new byte[sizeof(Int32)];
				stream.Read(voxcelSizeBuffer, 0, sizeof(Int32));
				Int32 voxcelSize = BitConverter.ToInt32(voxcelSizeBuffer, 0);


				// フレームデータ.
				byte[] voxcelDataBuffer = new byte[voxcelSize];
				stream.Read(voxcelDataBuffer, 0, voxcelSize);

				frames.Add(new FrameData(voxcelDataBuffer, format, transformer));
			}

			return frames;
		}

		public void Dispose()
        {
			if (!disposed){
				stream.Dispose();
				GC.SuppressFinalize(this);
				disposed = true;
			}
		}

	}
}

