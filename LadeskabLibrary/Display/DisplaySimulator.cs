using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    public class DisplaySimulator : IDisplay
    {
        private IEncapsulateIDisplay _encapsulateIDisplay;

        public DisplaySimulator(IEncapsulateIDisplay encapsulateIDisplay)
        {
            _encapsulateIDisplay = encapsulateIDisplay;
        }
        public void DisplayConnectTelephone()
        {
            _encapsulateIDisplay.WriteLine("Tilslut din telefon.");
        }

        public void DisplayReadRfid()
        {
            _encapsulateIDisplay.WriteLine("Placer RFID tag mod scanner.");
        }

        public void DisplayConnectionError()
        {
            _encapsulateIDisplay.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void DisplayReserved()
        {
            _encapsulateIDisplay.WriteLine("Ladeskabet er optaget.");
        }

        public void DisplayChargingDoorLocked()
        {
            _encapsulateIDisplay.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void DisplayRfidError()
        {
            _encapsulateIDisplay.WriteLine("Forkert RFID tag");
        }

        public void DisplayRemoveTelephone()
        {
            _encapsulateIDisplay.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void DisplayChargeingCorrect()
        {
            _encapsulateIDisplay.WriteLine("Opladningen af telefonen er igang og foregår normalt.");
        }

        public void DisplayChargeDone()
        {
            _encapsulateIDisplay.WriteLine("Opladning af din telefon er færdig.");
        }

    }
}
