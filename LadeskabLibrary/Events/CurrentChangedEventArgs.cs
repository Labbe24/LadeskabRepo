using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Events
{
    public class CurrentChangedEventArgs : EventArgs
    {
        public double Current { get; set; }
    }
}
