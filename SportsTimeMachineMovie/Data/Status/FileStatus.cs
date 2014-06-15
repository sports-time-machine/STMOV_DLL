using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachineMovie.Data.Status
{
    /// <summary>
    /// ファイル情報を格納するクラス.
    /// </summary>
    internal class FileStatus
    {
        public String Signature { get; private set; }

        public String Version { get; private set; }

        /// <summary>
        /// シグネチャ、バージョンを指定して構築
        /// </summary>
        public FileStatus(string signature, string version)
        {
            Signature = signature;
            Version = version;
        }
    }
}
