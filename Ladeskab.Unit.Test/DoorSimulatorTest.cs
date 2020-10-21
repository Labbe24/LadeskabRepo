using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Door;
using LadeskabLibrary.Events;
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
        }

        [Test]
        public void OnDoorOpen_OnDoorOpenCalled_EventFired()
        {
            _uut.OnDoorOpen();
            Assert.That(_recievedDoorOpenedEventArgs, Is.Not.Null);
        }


    }
}
