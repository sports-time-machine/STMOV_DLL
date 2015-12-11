using SportsTimeMachine.Data.Commons;
using SportsTimeMachine.Data.Depth;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Frames;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Transformer;
using SportsTimeMachine.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Units
{
    /// <summary>
    /// スポーツタイムマシンユニットデータを表す
    /// </summary>
    public class Unit
    {

        /// <summary>
        /// フレーム情報
        /// </summary>
        public List<FrameData> Frames { get; private set; }

        /// <summary>
        /// ファイル情報
        /// </summary>
        public FileStatus FileStatus { get; private set; }

        /// <summary>
        /// 変換方法
        /// </summary>
        private VoxcelTransformer transformer;

		/// <summary>
        /// ファイル情報
        /// フレームリスト
        /// から構築する
		/// </summary>
		/// <param name="stream">ストリーム.</param>
		public Unit (List<FrameData> frames, FileStatus fileStatus)
		{
            this.Frames = frames;
            this.FileStatus = fileStatus;
            transformer = new VoxcelTransformer(fileStatus.LeftCameraStatus, fileStatus.RightCameraStatus);
		}

        /// <summary>
        /// 深度情報を取得する
        /// </summary>
        /// <param name="frame">対象フレーム</param>
        /// <returns>深度情報</returns>
        public DepthUnit GetDepthUnit(int frame)
        {
            if (frame < 0) return null;
            if (frame >= Frames.Count) return null;

            return FileStatus.CompressFormat.Decompress(Frames[frame]);
        }

        /// <summary>
        /// 指定したフレームの点群データを取得する.
        /// </summary>
        /// <param name="frame">対象フレーム</param>
        /// <returns>点群情報</returns>
        public List<Vector3> GetUnitPointCloud(int frame)
        {
            return transformer.GetVocelList(GetDepthUnit(frame));
        }
    }
}
