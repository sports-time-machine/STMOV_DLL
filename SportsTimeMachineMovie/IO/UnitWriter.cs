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
                writer.Write(System.Text.Encoding.ASCII.GetBytes(unit.FileStatus.Signature.ToCharArray(), 0, 6));
                writer.Write((byte)unit.FileStatus.Version.Major);
                writer.Write((byte)unit.FileStatus.Version.Minor);
                writer.Write(BitConverter.GetBytes(unit.MovieStatus.TotalFrameCount));
                writer.Write(BitConverter.GetBytes(unit.MovieStatus.TotalTime));
                writer.Write(System.Text.Encoding.ASCII.GetBytes(unit.CompressFormat.GetName().ToCharArray(), 0, 16));
                
                // メタデータ部
                // writer.Write()


                // ボディ部


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
