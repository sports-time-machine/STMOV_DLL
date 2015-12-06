﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Status
{
    /// <summary>
    /// ユニット関連の情報を格納するクラス.
    /// </summary>
    public class UnitStatus
    {

        public int TotalFrameCount { get; private set; }

        public int TotalTime { get; private set; }

        /// <summary>
        /// 総フレーム数、総ミリ秒から構築
        /// </summary>
        public UnitStatus(int totalFrames,int totalTime )
        {
            TotalFrameCount = totalFrames;
            TotalTime = totalTime ;
        }
    }
}
