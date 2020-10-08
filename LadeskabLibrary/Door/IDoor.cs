using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.Door
{
    public interface IDoor
    {
        event EventHandler<DoorClosedEventArgs> DoorClosedEvent;
        event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent; 
        void LockDoor();
        void UnlockDoor();
    }
}
