﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Common
{
    class ConsoleDisplayer : IDisplayer
    {
        public void Display(string message)
        {
            Console.WriteLine(message);
        }
    }
}
