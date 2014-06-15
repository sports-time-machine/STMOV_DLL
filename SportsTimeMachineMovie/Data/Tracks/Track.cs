using SportsTimeMachineMovie.Data.Status;
using SportsTimeMachineMovie.Data.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachineMovie.Data.Tracks
{
    public class Track
    {
        public static int MAX_UNIT { get { return 6; } }

        /// <summary>
        /// ユニットリスト.
        /// </summary>
        private List<Unit> units;

        /// <summary>
        /// 総フレーム数を取得する.
        /// </summary>
        public int TotalFrame { get { return units[0].TotalFrame; } }

        /// <summary>
        /// 総時間(ミリ秒)を取得する.
        /// </summary>
        public int TotalTime { get { return units[0].TotalTime; } }

        /// <summary>
        /// シグネチャを取得する.
        /// </summary>
        public String Signature { get { return units[0].Signature; } }

        /// <summary>
        /// バージョンを取得する.
        /// </summary>
        public String Version { get { return units[0].Version; } }

        /// <summary>
        /// ユニットのリストから構築.
        /// </summary>
        /// <param name="units"></param>
        internal Track(List<Unit> units)
        {
            this.units = units;
        }

        /// <summary>
        /// 指定してフレームのに点群データを取得する.
        /// 該当するフレームがない場合、null.
        /// </summary>
        /// <returns></returns>
        public TrackPointCloud GetTrackPointCloud(int frame)
        {
            List<UnitPointCloud> pointClouds = new List<UnitPointCloud>(MAX_UNIT);
            foreach (Unit unit in units)
            {
                UnitPointCloud pointCloud = unit.GetUnitPointCloud(frame);
                if (pointCloud == null) return null;
                pointClouds.Add(pointCloud);
            }
            return new TrackPointCloud(pointClouds);
        }


    }
}
