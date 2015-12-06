using SportsTimeMachine.Data.Commons;
using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Frames;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Units
{
    /// <summary>
    /// スポーツタイムマシンユニットデータを表す.
    /// </summary>
    public class Unit
    {

        /// <summary>
        /// フレーム情報.
        /// </summary>
        public List<FrameData> Frames { get; private set; }

        /// <summary>
        /// ファイル情報.
        /// </summary>
        public FileStatus FileStatus { get; private set; }

        /// <summary>
        /// ムービー情報.
        /// </summary>
        public UnitStatus MovieStatus { get; private set; }

        /// <summary>
        /// 圧縮形式.
        /// </summary>
        public CompressFormat CompressFormat { get; private set; }

		/// <summary>
        /// フレームリスト
        /// ファイル情報
        /// ムービー情報
        /// 圧縮形式
        /// から構築する.
		/// </summary>
		/// <param name="stream">ストリーム.</param>
		public Unit (List<FrameData> frames, FileStatus fileStatus, UnitStatus movieStatus, CompressFormat format)
		{
            this.Frames = frames;
            this.FileStatus = fileStatus;
            this.MovieStatus = movieStatus;
            this.CompressFormat = format;
		}

        /// <summary>
        /// 指定したフレームの点群データを取得する.
        /// 該当するフレームがない場合、null.
        /// </summary>
        /// <returns></returns>
        public UnitPointCloud GetUnitPointCloud(int frame)
        {
            if (frame < 0) return null;
            if (frame >= Frames.Count) return null;
            UnitPointCloud pointCloud = Frames[frame].GetUnitPointCloud();
            return pointCloud;
        }
    }
}
