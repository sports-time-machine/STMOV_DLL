using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Tracks
{
    public class CompleteEventArgs : EventArgs
    {
        public Track Track { get; private set; }

        public CompleteEventArgs(Track track)
        {
            Track = track;
        }
    }
}
