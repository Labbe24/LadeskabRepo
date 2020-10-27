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
        public void HandleRfidDetected_RfidDetectedCalled()
        {

        }

        [Test]
        public void DoorOpenedEvent_ValidArguments_DoorOpendCalled()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());
            _uut.Received(1).DoorOpened();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailable_LadeskabStateLocked()
        {

        }

    }
}
