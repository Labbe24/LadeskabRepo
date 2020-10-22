using LadeskabLibrary.Events;
using LadeskabLibrary.RFID;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class RfidReaderSimulatorTest
    {
        private RfidReaderSimulator _uut;
        private RFIDDetectedEventArgs _recievedRfidDetectedEventArgs;

        [SetUp]
        public void SetUp()
        {
            _recievedRfidDetectedEventArgs = null;

            _uut = new RfidReaderSimulator();
            _uut.RFIDDetectedEvent += (o, args) =>
            {
                _recievedRfidDetectedEventArgs = args;
            };
        }

        [Test]
        public void OnRfidRead_OnRfidReadCalled_EventFired()
        {
            _uut.OnRfidRead(1);
            Assert.That(_recievedRfidDetectedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnRfidRead_OnRfidReadCalled_CorrectId()
        {
            _uut.OnRfidRead(1);
            Assert.That(_recievedRfidDetectedEventArgs, Is.EqualTo(1));
        }
    }
}