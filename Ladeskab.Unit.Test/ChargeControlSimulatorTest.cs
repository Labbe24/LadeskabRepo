using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.ChargeControl;
using LadeskabLibrary.Display;
using LadeskabLibrary.Events;
using LadeskabLibrary.UsbCharger;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class ChargeControlSimulatorTest
    {
        private ChargeControl _uut;

        private IUsbCharger _usbCharger;
        private IDisplay _display;


        [SetUp]
        public void SetUp()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_usbCharger,_display);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(500)]
        [TestCase(501)]
        public void CurrenChange_DifferentArguments_CurrentChangeIsCorrect(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs {Current = newCurrent});
            Assert.That(_usbCharger.CurrentValue, Is.EqualTo((newCurrent)));
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void CurrentChange_CurrentIsBetween0and5_StopCharge(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs {Current = newCurrent});
            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void CurrentChange_CurrentIsBetween0and5_DisplayChargeDone(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = newCurrent });
            _display.Received(1).DisplayChargeDone();
        }


        [TestCase(6)]
        [TestCase(500)]
        public void CurrentChange_CurrentIsBetween5and500_DisplayChargeingCorrect(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = newCurrent });
            _display.Received(1).DisplayChargeingCorrect();
        }

        
        [TestCase(501)]
        public void CurrentChange_CurrentIsOver500_StopCharge(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = newCurrent });
            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(501)]
        public void CurrentChange_CurrentIsBetween5and500_DisplayConnectionError(double newCurrent)
        {
            _usbCharger.CurrentChangedEvent += Raise.EventWith(new CurrentChangedEventArgs { Current = newCurrent });
            _display.Received(1).DisplayConnectionError();
        }
    }
}
