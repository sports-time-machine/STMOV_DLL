using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Units
{
    /// <summary>
    /// バージョン情報を表す
    /// </summary>
    public class Version
    {
        /// <summary>
        /// メジャーバージョン
        /// </summary>
        public int Major { get; set; }

        /// <summary>
        /// マイナーバージョン
        /// </summary>
        public int Minor { get; set; }

        public Version(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString();
        }
    }
}
