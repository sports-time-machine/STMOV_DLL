using SportsTimeMachineMovie.Data.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachineMovie.Data.Tracks
{
    /// <summary>
    /// 各ユニットの番号.
    /// </summary>
    public enum UnitNumber
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX
    }

    /// <summary>
    /// トラックでの点群.点群が6ユニット分に分かれている.
    /// </summary>
    public class TrackPointCloud
    {
        List<UnitPointCloud> unitsPointCloud;

        internal TrackPointCloud(List<UnitPointCloud> unitsPointCloud)
        {
            this.unitsPointCloud = unitsPointCloud;
        }

        /// <summary>
        /// ユニットの点群データを取得する.
        /// </summary>
        /// <param name="num">取得するユニット番号.</param>
        /// <returns></returns>
        public UnitPointCloud GetUnitPointCloud(UnitNumber num)
        {
            switch (num)
            {
                case UnitNumber.ONE:
                    return unitsPointCloud[0];
                case UnitNumber.TWO:
                    return unitsPointCloud[1];
                case UnitNumber.THREE:
                    return unitsPointCloud[2];
                case UnitNumber.FOUR:
                    return unitsPointCloud[3];
                case UnitNumber.FIVE:
                    return unitsPointCloud[4];
                case UnitNumber.SIX:
                    return unitsPointCloud[5];
            }

            // ここまで到達することはない.
            throw new ArgumentException();
        }
    }
}
