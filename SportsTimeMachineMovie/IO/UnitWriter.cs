using SportsTimeMachine.Data.Frames;
using SportsTimeMachine.Data.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.IO
{
    public class UnitWriter : IDisposable
    {

        /// <summary>
        /// インスタンスが破棄されたかどうか.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// ストリーム.
        /// </summary>
        private Stream stream;

		/// <summary>
		/// ストリームライターからWriterを構築する.
		/// </summary>
		/// <param name="stream">ストリーム.</param>
        public UnitWriter(Stream stream)
		{
			disposed = false;
            this.stream = stream;
		}

        public void Write(Unit unit)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {   
                // ヘッダ部
                writer.Write(unit.FileStatus.Signature.ToBytes());
                writer.Write(unit.FileStatus.Version.ToBytes());
                writer.Write(BitConverter.GetBytes(unit.FileStatus.TotalFrames));
                writer.Write(BitConverter.GetBytes(unit.FileStatus.TotalTime));
                writer.Write(unit.FileStatus.CompressFormat.ToBytes());

                // メタデータ部
                writer.Write(unit.FileStatus.LeftCameraStatus.ToBytes());
                writer.Write(unit.FileStatus.RightCameraStatus.ToBytes());
                writer.Write(BitConverter.GetBytes(unit.FileStatus.DotSize));
                
                // ボディ部
                foreach (FrameData item in unit.Frames)
                {
                    writer.Write(item.ToBytes());
                }

                writer.Write(("[EOF]\0").ToCharArray());

            } 
        }

        public void Dispose()
        {
            if (!disposed)
            {
                stream.Dispose();
                GC.SuppressFinalize(this);
                disposed = true;
            }
        }
    }
}
