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
        private IUsbCharger usbCharger;
        private IDisplay display;

        public event EventHandler<CurrentChangedEventArgs> CurrentChangedEvent;

        private void HandleCurrentChangedEvent(object s, CurrentChangedEventArgs e)
        {
            CurrentChangeEvent(e.Current);
        }

        private void CurrentChangeEvent(double current)
        {
            switch (current)
            {
                case double n when (n == 0):
                    break;

                case double n when (0 < n && n <= 5):
                    usbCharger.StopCharge();
                    display.DisplayChargeDone();
                    break;

                case double n when (5 < n && n <= 500):
                    display.DisplayChargeingCorrect();
                    break;

                case double n when (n > 500):
                    usbCharger.StopCharge();
                    display.DisplayConnectionError();
                    break;
            }
        }

        public bool IsConnected()
        {
            return usbCharger.Connected;
        }

        public void StartCharge()
        {
            usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            usbCharger.StopCharge();
        }
    }
}
