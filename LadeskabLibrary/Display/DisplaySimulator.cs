using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    public class DisplaySimulator : IDisplay
    {
        public void DisplayConnectTelephone()
        {
            Console.WriteLine(("Tilslut din telefon."));
        }

        public void DisplayReadRfid()
        {
            Console.WriteLine(("Placer RFID tag mod scanner."));
        }

        public void DisplayConnectionError()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void DisplayReserved()
        {
            Console.WriteLine(("Ladeskabet er optaget."));
        }

        public void DisplayRfidError()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void DisplayRemoveTelephone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void DisplayChargeingCorrect()
        {
            Console.WriteLine("Opladningen af telefonen er igang og foregår normalt.");
        }

        public void DisplayChargeDone()
        {
            Console.WriteLine("Opladning af din telefon er færdig.");
        }

    }
}
