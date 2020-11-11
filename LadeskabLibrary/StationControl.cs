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
        // Made public for testability
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
            _state = LadeskabState.Available;

            // Subscribe to Event's
            // with handler that should handle the event
            _rfidReader.RFIDDetectedEvent += HandleRfidDetectedEvent;
            _door.DoorOpenedEvent += HandleDoorOpenedEvent;
            _door.DoorClosedEvent += HandleDoorClosedEvent;
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

                        _display.DisplayChargingDoorLocked();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayConnectionError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (CheckId(id))
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.DisplayRemoveTelephone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayRfidError();
                    }

                    break;
            }
        }

        private void DoorOpened()
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    _display.DisplayConnectTelephone();
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
                    _display.DisplayReadRfid();
                    _state = LadeskabState.Available;
                    break;

                case LadeskabState.Locked:
                    // Ignore
                    break;
            }
        }

        private bool CheckId(int id)
        {
            return _oldId == id;
        }
    }
}

