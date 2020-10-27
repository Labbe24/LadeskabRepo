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

        [Test]
        public void DoorOpenedEvent_StateAvailable_StateDoorOpen()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }
        
        [Test]
        public void DoorOpenedEvent_StateAvailable_DisplayConnectTelephone()
        {
            _uut.State = StationControl.LadeskabState.Available;
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());

            _display.Received(1).DisplayConnectTelephone();
        }

     

    }
}
