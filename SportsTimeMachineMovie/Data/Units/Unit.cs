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
        private List<FrameData> frames;

        /// <summary>
        /// ファイル情報.
        /// </summary>
        private FileStatus fileStatus;

        /// <summary>
        /// ムービー情報.
        /// </summary>
        private UnitStatus movieStatus;

        /// <summary>
        /// 総フレーム数を取得する.
        /// </summary>
        public int TotalFrame { get { return movieStatus.TotalFrameCount; } }

        /// <summary>
        /// 総時間(ミリ秒)を取得する.
        /// </summary>
        public int TotalTime { get { return movieStatus.TotalTime; } }

        /// <summary>
        /// シグネチャを取得する.
        /// </summary>
        public String Signature { get { return fileStatus.Signature; } }

        /// <summary>
        /// バージョンを取得する.
        /// </summary>
        public String Version { get { return fileStatus.Version; } }

		/// <summary>
        /// フレームリスト、ファイル情報、ムービー情報から構築する.
		/// </summary>
		/// <param name="stream">ストリーム.</param>
		internal Unit (List<FrameData> frames, FileStatus fileStatus, UnitStatus movieStatus)
		{
            this.frames = frames;
            this.fileStatus = fileStatus;
            this.movieStatus = movieStatus;
		}

        /// <summary>
        /// 指定したフレームの点群データを取得する.
        /// 該当するフレームがない場合、null.
        /// </summary>
        /// <returns></returns>
        public UnitPointCloud GetUnitPointCloud(int frame)
        {
            if (frame < 0) return null;
            if (frame >= frames.Count) return null;
            UnitPointCloud pointCloud = frames[frame].GetUnitPointCloud();
            return pointCloud;
        }
    }
}
