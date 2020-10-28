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
    public class StationControlSimulatorTest
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
        public void DoorOpened_StateAvailable_StateDoorOpen()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }
        
        [Test]
        public void DoorOpened_StateAvailable_DisplayConnectTelephone()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());

            _display.Received(1).DisplayConnectTelephone();
        }

        // DoorClosedEvent
        [Test]
        public void DoorClosed_StateDoorOpen_StateAvailable()
        {
            _uut.State = StationControl.LadeskabState.DoorOpen;
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        }
        
        [Test]
        public void DoorClosed_StateDoorOpen_DisplayReadRfid()
        {
            _uut.State = StationControl.LadeskabState.DoorOpen;
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs());

            _display.Received(1).DisplayReadRfid();
        }

        // RfidDetectedEvent
        [Test]
        public void RfidDetected_StateAvailableChargerConnected_LockDoor()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _door.Received(1).LockDoor();
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerConnected_StartCharge()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _chargeControl.Received(1).StartCharge();
        }
        
        [TestCase(1)]
        public void RfidDetected_StateAvailableChargerConnected_OldIdIsCorrect(int id)
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { Id = id });

            Assert.That(_uut.OldId, Is.EqualTo(id));
        }

        [Test]
        public void RfidDetected_StateAvailableChargerConnected_DisplayChargingDoorLocked()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayChargingDoorLocked();
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerConnected_StateLocked()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerNotConnected_StateAvailable()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(false);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        }
        
        [Test]
        public void RfidDetected_StateAvailableChargerNotConnected_DisplayConnectionError()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(false);
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayConnectionError();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_StopCharge()
        {
            _uut.State = StationControl.LadeskabState.Locked;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _chargeControl.Received(1).StopCharge();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_UnlockDoor()
        {
            _uut.State = StationControl.LadeskabState.Locked;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _door.Received(1).UnlockDoor();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_DisplayRemoveTelephone()
        {
            _uut.State = StationControl.LadeskabState.Locked;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _display.Received(1).DisplayRemoveTelephone();
        }
        
        [Test]
        public void RfidDetected_StateLockedCorrectId_StateAvailable()
        {
            _uut.State = StationControl.LadeskabState.Locked;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        }
     

    }
}
