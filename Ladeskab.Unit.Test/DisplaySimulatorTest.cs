using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private DisplaySimulator _uut;
        private IEncapsulateIDisplay _encapsulateIDisplay;
        private string _testText;

        [SetUp]
        public void Setup()
        {
            _encapsulateIDisplay = Substitute.For<IEncapsulateIDisplay>();
            _uut = new DisplaySimulator();
        }

        [Test]
        public void DisplayConnectTelephone_Called_ConsoleWriteLine()
        {
            _testText = "Tilslut din telefon.";
            _uut.DisplayConnectTelephone();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }

        [Test]
        public void DisplayReadRfid_Called_ConsoleWriteLine()
        {
            _testText = "Placer RFID tag mod scanner.";
            _uut.DisplayReadRfid();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayConnectionError_Called_ConsoleWriteLine()
        {
            _testText = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
            _uut.DisplayConnectionError();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayReserved_Called_ConsoleWriteLine()
        {
            _testText = "Ladeskabet er optaget.";
            _uut.DisplayReserved();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayChargingDoorLocked_Called_ConsoleWriteLine()
        {
            _testText = "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.";
            _uut.DisplayChargingDoorLocked();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayRfidError_Called_ConsoleWriteLine()
        {
            _testText = "Forkert RFID tag";
            _uut.DisplayRfidError();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayRemoveTelephone_Called_ConsoleWriteLine()
        {
            _testText = "Tag din telefon ud af skabet og luk døren";
            _uut.DisplayRemoveTelephone();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayChargeingCorrect_Called_ConsoleWriteLine()
        {
            _testText = "Opladningen af telefonen er igang og foregår normalt.";
            _uut.DisplayChargeingCorrect();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }
        
        [Test]
        public void DisplayChargeDone_Called_ConsoleWriteLine()
        {
            _testText = "Opladning af din telefon er færdig.";
            _uut.DisplayChargeDone();
            _encapsulateIDisplay.Received(1).WriteLine(_testText);
        }



    }
}
