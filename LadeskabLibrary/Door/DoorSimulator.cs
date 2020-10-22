using System;
using System.Collections.Generic;
using System.Text;
using LadeskabLibrary.Events;

namespace LadeskabLibrary.Door
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;
        public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;

        private bool locked;
        public bool Locked
        {
            get { return locked;}
        }

        public void LockDoor()
        {
            locked = true;
        }

        public void UnlockDoor()
        {
            locked = false;
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
