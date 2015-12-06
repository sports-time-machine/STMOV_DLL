using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Status
{
    /// <summary>
    /// ファイル情報を格納するクラス.
    /// </summary>
    public class FileStatus
    {
        public String Signature { get; private set; }

        public Units.Version Version { get; private set; }

        /// <summary>
        /// シグネチャ、バージョンを指定して構築
        /// </summary>
        public FileStatus(string signature, Units.Version version)
        {
            Signature = signature;
            Version = version;
        }
    }
}
