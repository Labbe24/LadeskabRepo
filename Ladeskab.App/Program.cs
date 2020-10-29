using System;
using LadeskabLibrary;
using LadeskabLibrary.ChargeControl;
using LadeskabLibrary.Display;
using LadeskabLibrary.RFID;
using LadeskabLibrary.Door;
using LadeskabLibrary.UsbCharger;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        DoorSimulator door = new DoorSimulator();
        RfidReaderSimulator rfidReader = new RfidReaderSimulator();
        EncapsulateDisplay encapDisplay = new EncapsulateDisplay();
        DisplaySimulator display = new DisplaySimulator(encapDisplay);
        UsbChargerSimulator usbCharger = new UsbChargerSimulator();
        ChargeControl chargeControl = new ChargeControl(usbCharger, display);
        StationControl stationControl = new StationControl(chargeControl, door, display, rfidReader);

        usbCharger.SimulateConnected(true);

        bool finish = false;
        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}
