using ICSharpCode.SharpZipLib.Zip;
using SportsTimeMachineMovie.Data.Tracks;
using SportsTimeMachineMovie.Data.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsTimeMachineMovie.IO
{
    /// <summary>
    /// トラックデータを読み取るクラス.
    /// </summary>
    public class TrackReader : IDisposable
    {       
        /// <summary>
        /// 読み込むバッファのデフォルトサイズ.
        /// </summary>
        private const int DEFAULT_BUFFER_SIZE = 2097152;

        /// <summary>
        /// 読み込み時のバッファのサイズを取得設定する.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// インスタンスが破棄されたかどうか.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// ストリーム.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// 読み込みの進捗が変化した.
        /// </summary>
        public event EventHandler<ProgressEventArgs> ReadProgressing = delegate { };

        /// <summary>
        /// 読み込みが完了した.
        /// </summary>
        public event EventHandler<CompleteEventArgs> ReadCompleted = delegate { };


        /// <summary>
        /// ファイルパスからReaderを構築する.
        /// </summary>
        /// <param name="filename"></param>
        public TrackReader(String filepath)
            :this(new FileStream(filepath, FileMode.Open))
        {
        }

        /// <summary>
        /// ストリームからReaderを構築する.
        /// </summary>
        /// <param name="stream"></param>
        public TrackReader(Stream stream)
        {
            disposed = false;
            this.stream = stream;
            BufferSize = DEFAULT_BUFFER_SIZE;
        }

        /// <summary>
        /// スポーツタイムマシンムービーデータを非同期で読み込む.
        /// 読み込みの進捗はReadProgressingイベントで、
        /// 読み込みの完了はReadCompletedイベントで取得することが可能.
        /// </summary>
        public IEnumerator ReadAsync()
        {
            List<Unit> units = new List<Unit>(Track.MAX_UNIT);

            using (ZipInputStream zipInputStream = new ZipInputStream(stream))
            {
                for (int i = 0; i < Track.MAX_UNIT; i++)
                {
                    // Zip解凍.
                    zipInputStream.GetNextEntry();
                    long maxLength = zipInputStream.Length;
                    using (MemoryStream unitStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[BufferSize];
                        int len;
                        while ((len = zipInputStream.Read(buffer, 0, BufferSize)) > 0)
                        {
                            unitStream.Write(buffer, 0, len);
                            int progress = (int)((unitStream.Length / (float)maxLength) * (100.0f / (float)Track.MAX_UNIT) + (100.0f / (float)Track.MAX_UNIT) * i);
                            ReadProgressing(this, new ProgressEventArgs(progress));
                            yield return null;
                        }
                        UnitReader reader = new UnitReader(unitStream);
                        units.Add(reader.Read());
                    }
                }
            }

            Track movie = new Track(units);
            ReadCompleted(this, new CompleteEventArgs(movie));   
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
