﻿using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.Door
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;
        public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;

        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

        public virtual void OnDoorOpen()
        {
            DoorOpenedEvent?.Invoke(this, new DoorOpenedEventArgs());
        }

        public virtual void OnDoorClose()
        {
            DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs());
        }
    }
}
