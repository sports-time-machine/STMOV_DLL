using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Commons
{
    /// <summary>
    /// 2次元のデータを表す構造体.
    /// </summary>
    public struct Vector2
    {
        public float x;
        public float y;
        
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
