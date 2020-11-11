using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary;
using LadeskabLibrary.Display;
using LadeskabLibrary.Door;
using LadeskabLibrary.Events;
using LadeskabLibrary.RFID;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class StationControlTest
    {
        private StationControl _uut;

        private IChargeControl _chargeControl;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfidReader;


        [SetUp]
        public void Setup()
        {
            _chargeControl = Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRfidReader>();

            _uut = new StationControl(_chargeControl, _door, _display, _rfidReader);

        }

        // DoorOpenedEvent
        [Test]
        public void DoorOpened_StateAvailable_DisplayConnectTelephone()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());

            _display.Received(1).DisplayConnectTelephone();
        }

        // DoorClosedEvent       
        [Test]
        public void DoorClosed_StateDoorOpen_DisplayReadRfid()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs());

            _display.Received(1).DisplayReadRfid();
        }

        // RfidDetectedEvent
        [Test]
        public void RfidDetected_StateAvailableChargerConnected_LockDoor()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _door.Received(1).LockDoor();
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerConnected_StartCharge()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _chargeControl.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_StateAvailableChargerConnected_DisplayChargingDoorLocked()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayChargingDoorLocked();
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerNotConnected_DisplayConnectionError()
        {
            _chargeControl.IsConnected().Returns(false);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayConnectionError();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_StopCharge()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _chargeControl.Received(1).StopCharge();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_UnlockDoor()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _door.Received(1).UnlockDoor();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_DisplayRemoveTelephone()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayRemoveTelephone();
        }
        
        [TestCase(2, 1)]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        public void RfidDetected_StateLockedNotCorrectId_DisplayRfidError(int oldId, int id)
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {Id = oldId});
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs{Id = id});

            _display.Received(1).DisplayRfidError();
        }
     

    }
}
