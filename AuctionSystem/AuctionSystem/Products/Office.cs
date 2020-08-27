using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Products
{
    public class Office : Building
    {
        public Office(List<double> roomsSize, Dictionary<string, string> prop) : base(roomsSize, prop)
        {
        }
    }
}
