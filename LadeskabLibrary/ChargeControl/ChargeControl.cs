using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Display;
using LadeskabLibrary.Events;
using LadeskabLibrary.UsbCharger;

namespace LadeskabLibrary.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        public event EventHandler<CurrentChangedEventArgs> CurrentChangedEvent;

        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            // Allow constructor-injection for tests
            _usbCharger = charger;
            _display = display;

            // Subscribe to Event's
            // with handler that should handle the event
            _usbCharger.CurrentChangedEvent += HandleCurrentChangedEvent;
        }

        private void HandleCurrentChangedEvent(object s, CurrentChangedEventArgs e)
        {
            CurrentChangeEvent(e.Current);
        }

        private void CurrentChangeEvent(double current)
        {
            switch (current)
            {
                case double n when (n == 0):
                    _usbCharger.CurrentValue = current;
                    break;

                case double n when (0 < n && n <= 5):
                    _usbCharger.CurrentValue = current;
                    _usbCharger.StopCharge();
                    _display.DisplayChargeDone();
                    break;

                case double n when (5 < n && n <= 500):
                    _usbCharger.CurrentValue = current;
                    _display.DisplayChargeingCorrect();
                    break;

                case double n when (n > 500):
                    _usbCharger.CurrentValue = current;
                    _usbCharger.StopCharge();
                    _display.DisplayConnectionError();
                    break;
            }
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}
