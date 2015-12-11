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
        /// メジャーバージョンを取得設定する
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// マイナーバージョンを取得設定する
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// バージョンを構築する
        /// </summary>
        /// <param name="major">メジャーバージョン</param>
        /// <param name="minor">マイナーバージョン</param>
        public Version(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        /// <summary>
        /// バージョン情報を取得する
        /// </summary>
        /// <returns>バージョン情報</returns>
        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString();
        }

        /// <summary>
        /// バイト列を取得する
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)Major;
            bytes[1] = (byte)Minor;
            return bytes;
        }
    }
}
