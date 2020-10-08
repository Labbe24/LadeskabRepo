using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary
{
    public interface IChargeControl
    {
        event EventHandler<CurrentChangedEventArgs> CurrentChangedEvent; 
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}
