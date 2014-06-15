using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SportsTimeMachineMovie.Data.Units
{
    /// <summary>
    /// ユニットでの点群.
    /// </summary>
    public class UnitPointCloud
    {
        public List<Vector3> VectorList { get; private set; }

        internal UnitPointCloud(List<Vector3> pointCloud)
        {
            VectorList = pointCloud;
        }
    }
}
