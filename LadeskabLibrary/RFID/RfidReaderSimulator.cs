using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.RFID
{
    public class RfidReaderSimulator : IRfidReader
    {
        public event EventHandler<RFIDDectedEventArgs> RFIDDectedEvent;
        public void OnRfidRead(int Id)
        {

        }
    }
}
