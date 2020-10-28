using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    public class DisplaySimulator : IDisplay
    {
        private IEncapsulateIDisplay encapsulateIDisplay;
        public void DisplayConnectTelephone()
        {
            encapsulateIDisplay.WriteLine("Tilslut din telefon.");
        }

        public void DisplayReadRfid()
        {
            encapsulateIDisplay.WriteLine("Placer RFID tag mod scanner.");
        }

        public void DisplayConnectionError()
        {
            encapsulateIDisplay.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void DisplayReserved()
        {
            encapsulateIDisplay.WriteLine("Ladeskabet er optaget.");
        }

        public void DisplayChargingDoorLocked()
        {
            encapsulateIDisplay.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void DisplayRfidError()
        {
            encapsulateIDisplay.WriteLine("Forkert RFID tag");
        }

        public void DisplayRemoveTelephone()
        {
            encapsulateIDisplay.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void DisplayChargeingCorrect()
        {
            encapsulateIDisplay.WriteLine("Opladningen af telefonen er igang og foregår normalt.");
        }

        public void DisplayChargeDone()
        {
            encapsulateIDisplay.WriteLine("Opladning af din telefon er færdig.");
        }

    }
}
