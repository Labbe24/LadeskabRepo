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
        public void RfidDetected_StateAvailable_LockDoor()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _door.Received(1).LockDoor();
        }
        
        [Test]
        public void RfidDetected_StateAvailable_StartCharge()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            _chargeControl.Received(1).StartCharge();
        }
        
        [TestCase(1)]
        public void RfidDetected_StateAvailable_OldIdIsCorrect(int id)
        {
            _uut.State = StationControl.LadeskabState.Available;
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs());

            Assert.That(_uut.OldId, Is.EqualTo(id));
        }
     

    }
}
