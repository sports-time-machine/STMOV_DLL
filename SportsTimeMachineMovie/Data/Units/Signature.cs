using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Units
{
    /// <summary>
    /// シグネチャを表すクラス
    /// </summary>
    public class Signature
    {
        public string Text { get; private set; }

        /// <summary>
        /// シグネチャを指定して構築
        /// </summary>
        /// <param name="text">シグネチャ</param>
        public Signature(string text)
        {
            Text = text;
        }

        /// <summary>
        /// バイト列を返す
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            return System.Text.Encoding.ASCII.GetBytes(Text.ToCharArray(), 0, 6);
        }
    }
}
