﻿using SportsTimeMachine.Data.Commons;
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
        /// 指定したフレームのユニットごとの点群データを取得する.
        /// </summary>
        /// <returns></returns>
        public List<UnitPointCloud> GetUnitsPointCloud(int frame)
        {
            List<UnitPointCloud> pointClouds = new List<UnitPointCloud>(MAX_UNIT);
            foreach (Unit unit in units)
            {
                UnitPointCloud pointCloud = unit.GetUnitPointCloud(frame);
                pointClouds.Add(pointCloud);
            }
            return pointClouds;
        }

        /// <summary>
        /// 指定したフレームのトラックの点群データを取得する.
        /// </summary>
        /// <returns></returns>
        public List<Vector3> GetTrackPointCloud(int frame)
        {
            List<UnitPointCloud> unitsPointClouds = GetUnitsPointCloud(frame);
            List<Vector3> trackPointCloud = new List<Vector3>();

            for (int i = 0; i < MAX_UNIT; i++)
            {
                if (unitsPointClouds[i] != null)
                {
                    for (int j = 0; j < unitsPointClouds[i].VectorList.Count; j++)
                    {
                        Vector3 vec = unitsPointClouds[i].VectorList[j];
                        vec.x += i * 320.0f;
                        trackPointCloud.Add(vec);
                    }
                }
            }

            return trackPointCloud;
        }


    }
}
