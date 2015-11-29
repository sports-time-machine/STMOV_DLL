using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Exception
{
    /// <summary>
    /// スポーツタイムマシンでの例外を扱う.
    /// </summary>
    internal class SptmException : System.Exception
    {
        public SptmException() : base() { }
        public SptmException(string message) : base(message) { }
        public SptmException(string message, System.Exception inner) : base(message, inner) { }
    }
}
