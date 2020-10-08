using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.RFID
{
    public interface IRfidReader
    {
        event EventHandler<RFIDDectedEventArgs> RFIDDectedEvent; 
        void OnRfidRead(int Id);
    }
}
