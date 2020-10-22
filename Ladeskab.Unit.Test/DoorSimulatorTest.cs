using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Door;
using LadeskabLibrary.Events;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class DoorSimulatorTest
    {
        private DoorSimulator _uut;
        private DoorOpenedEventArgs _recievedDoorOpenedEventArgs;
        private DoorClosedEventArgs _recievedDoorClosedEventArgs;

        [SetUp]
        public void Setup()
        {
            _recievedDoorOpenedEventArgs = null;
            _recievedDoorClosedEventArgs = null;

            _uut = new DoorSimulator();
            _uut.DoorOpenedEvent += (o, args) =>
            {
                _recievedDoorOpenedEventArgs = args;
            };
            _uut.DoorClosedEvent += (o, args) =>
            {
                _recievedDoorClosedEventArgs = args;
            };
        }

        [Test]
        public void OnDoorOpen_OnDoorOpenCalled_EventFired()
        {
            _uut.OnDoorOpen();
            Assert.That(_recievedDoorOpenedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorOpen_OnDoorOpenNotCalled_EventNotFired()
        {
            Assert.That(_recievedDoorOpenedEventArgs, Is.Null);
        }

        [Test]
        public void OnDoorClose_OnDoorCloseCalled_EventFired()
        {
            _uut.OnDoorClose();
            Assert.That(_recievedDoorClosedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorClose_OnDoorCloseNotCalled_EventNotFired()
        {
            Assert.That(_recievedDoorClosedEventArgs, Is.Null);
        }

        


    }
}
