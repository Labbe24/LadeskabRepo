using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LadeskabLibrary;
using LadeskabLibrary.Display;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class DisplaySimulatorTest
    {
        private StationControl _uut;
        private EncapsulateIDisplay encapsulateIDisplay;

        [SetUp]
        public void Setup()
        {
            // = Substitute.For<IDisplay>();
            // _uut = new StationControl();
        }

        [Test]
        public void DoorOpened_EventFired_DisplayConnectTelephone()
        {
            
            //_display.DisplayConnectTelephone() +=
        }

    }
}
