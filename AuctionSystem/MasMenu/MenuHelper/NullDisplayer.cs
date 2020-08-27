using AuctionSystem.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasMenu.MenuHelper
{
    public class NullDisplayer : IDisplayer
    {
        public void Display(string message)
        {
            // do nothing on purpose for menu
        }
    }
}
