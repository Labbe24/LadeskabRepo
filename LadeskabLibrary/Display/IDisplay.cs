using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    public interface IDisplay
    {
        void DisplayConnectTelephone();
        void DisplayReadRfid();
        void DisplayConnectionError();
        void DisplayReserved();
        void DisplayRfidError();
        void DisplayRemoveTelephone();
        void DisplayChargeingCorrect();
        void DisplayChargeDone();

        void DisplayChargingDoorLocked();

    }
}
