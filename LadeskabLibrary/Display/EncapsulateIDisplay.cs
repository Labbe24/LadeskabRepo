using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    public class EncapsulateIDisplay
    {
        private IDisplay _iDisplay;

        void EnCapDisplayConnectTelephone()
        {
            _iDisplay.DisplayConnectTelephone();
        }

        void EncapDisplayReadRfid()
        {
            _iDisplay.DisplayReadRfid();
        }

        void EncapDisplayConnectionError()
        {
            _iDisplay.DisplayConnectionError();
        }

        void EncaoDisplayReserved()
        {
            _iDisplay.DisplayReserved();
        }

        void EncapDisplayRfidError()
        {
            _iDisplay.DisplayRfidError();
        }

        void EncapDisplayRemoveTelephone()
        {
            _iDisplay.DisplayRemoveTelephone();
        }
    }
}
