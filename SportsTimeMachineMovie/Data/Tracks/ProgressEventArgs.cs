using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Tracks
{
    public class ProgressEventArgs : EventArgs
    {
        public int Value { get; private set; }

        public ProgressEventArgs(int value)
        {
            Value = value;
        }
    }
}
