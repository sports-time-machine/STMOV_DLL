using SportsTimeMachine.Data.Commons;
using SportsTimeMachine.Data.Status;
using SportsTimeMachine.Data.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Tracks
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
        public int TotalFrame { get { return units[0].FileStatus.TotalFrames; } }

        /// <summary>
        /// 総時間(ミリ秒)を取得する.
        /// </summary>
        public int TotalTime { get { return units[0].FileStatus.TotalTime; } }

        /// <summary>
        /// シグネチャを取得する.
        /// </summary>
        public String Signature { get { return units[0].FileStatus.Signature.Text; } }

        /// <summary>
        /// バージョンを取得する.
        /// </summary>
        public String Version { get { return units[0].FileStatus.Version.ToString(); } }

        /// <summary>
        /// ユニットのリストから構築.
        /// </summary>
        /// <param name="units"></param>
        public Track(List<Unit> units)
        {
            this.units = units;
        }

        /// <summary>
        /// 指定したフレームのユニットごとの点群データを取得する.
        /// </summary>
        /// <returns></returns>
        public List<List<Vector3>> GetUnitsPointCloud(int frame)
        {
            List<List<Vector3>> pointClouds = new List<List<Vector3>>(MAX_UNIT);
            foreach (Unit unit in units)
            {
                List<Vector3> pointCloud = unit.GetUnitPointCloud(frame);
                pointClouds.Add(pointCloud);
            }
            return pointClouds;
        }
    }
}
