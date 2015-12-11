using SportsTimeMachine.Data.Formats;
using SportsTimeMachine.Data.Units;
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
        /// <summary>
        /// シグネチャを取得する
        /// </summary>
        public Signature Signature { get; private set; }

        /// <summary>
        /// バージョン情報を取得する
        /// </summary>
        public Units.Version Version { get; private set; }

        /// <summary>
        /// 総フレーム数を取得する
        /// </summary>
        public int TotalFrames { get; private set; }

        /// <summary>
        /// 総時間(ミリ秒)を取得する
        /// </summary>
        public int TotalTime { get; private set; }

        /// <summary>
        /// 圧縮形式を取得する
        /// </summary>
        public CompressFormat CompressFormat { get; private set; }

        /// <summary>
        /// 右カメラ情報を取得する
        /// </summary>
        public CameraStatus RightCameraStatus { get; private set; }

        /// <summary>
        /// 左カメラ情報を取得する
        /// </summary>
        public CameraStatus LeftCameraStatus { get; private set; }

        /// <summary>
        /// OpenGL内での1ドットの大きさ
        /// </summary>
        public float DotSize { get; private set; }

        /// <summary>
        /// ファイル情報を構築する
        /// </summary>
        /// <param name="signature">シグネチャ</param>
        /// <param name="version">バージョン</param>
        /// <param name="totalFrames">総フレーム数</param>
        /// <param name="totalTime">総時間(ミリ秒)</param>
        /// <param name="compressFormat">圧縮フォーマット</param>
        /// <param name="leftCameraStatus">左カメラ情報</param>
        /// <param name="rightCameraStatus">右カメラ情報</param>
        /// <param name="dotSize">ドットサイズ</param>
        public FileStatus(
            Signature signature,
            Units.Version version,
            int totalFrames, 
            int totalTime,
            CompressFormat compressFormat,
            CameraStatus leftCameraStatus,
            CameraStatus rightCameraStatus,
            float dotSize
        )
        {
            Signature = signature;
            Version = version;
            TotalFrames = totalFrames;
            TotalTime = totalTime;
            CompressFormat = compressFormat;
            RightCameraStatus = rightCameraStatus;
            LeftCameraStatus = leftCameraStatus;
            DotSize = dotSize;
        }
    }
}
