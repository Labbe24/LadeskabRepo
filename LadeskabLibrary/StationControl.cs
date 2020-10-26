using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabLibrary.Display;
using LadeskabLibrary.Door;
using LadeskabLibrary.Events;
using LadeskabLibrary.RFID;

namespace LadeskabLibrary
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private int _oldId;
        private IDisplay _display;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IChargeControl charger, IDoor door, IDisplay display, IRfidReader rfidReader)
        {
            // Allow constructor-injection for tests
            _charger = charger;
            _door = door;
            _display = display;
            _rfidReader = rfidReader;

            // Subscribe to Event's
            // with handler that should handle the event
            rfidReader.RFIDDetectedEvent += HandleRfidDetectedEvent;
            door.DoorOpenedEvent += HandleDoorOpenedEvent;
            door.DoorClosedEvent += HandleDoorClosedEvent;
        }

        private void HandleRfidDetectedEvent(object sender, RFIDDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }

        private void HandleDoorOpenedEvent(object sender, DoorOpenedEventArgs e)
        {
            DoorOpened();
        }

        private void HandleDoorClosedEvent(object sender, DoorClosedEventArgs e)
        {
            DoorClosed();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void DoorOpened()
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    Console.WriteLine("Tilslut telefon");
                    _state = LadeskabState.DoorOpen;
                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Ignore
                    break;
            }
        }

        private void DoorClosed()
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Ignore
                    break;

                case LadeskabState.DoorOpen:
                    Console.WriteLine("Indlæs RFID");
                    _state = LadeskabState.Available;
                    break;

                case LadeskabState.Locked:
                    // Ignore
                    break;
            }
        }
    }
}

