using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.RFID
{
    public class RfidReaderSimulator : IRfidReader
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public void OnRfidRead(int userId)
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs(){Id = userId});
        }
    }
}
