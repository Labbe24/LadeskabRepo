using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.RFID
{
    class RfidReaderSimulator
    {
        public event EventHandler<RFIDDectedEventArgs> RFIDDectedEvent;
        private void OnRfidRead(int Id)
        {

        }
    }
}
