﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LadeskabLibrary.Display
{
    class EncapsulateDisplay : IEncapsulateIDisplay
    {
        public void WriteLine(String text)
        {
            System.Console.WriteLine(text);
        }
    }
}
