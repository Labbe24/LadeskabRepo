using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary
{
    public interface IUsbCharger
    {
        event EventHandler<CurrentChangedEventArgs> CurrentChangedEvent; 
        void StartCharge();
        void StopCharge();
        public bool Connected { get; }
        public double CurrentValue { get; }
    }
}
