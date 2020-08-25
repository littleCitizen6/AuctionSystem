using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Products
{
    public class House : Building
    {
        public House(List<double> roomsSize, Dictionary<string, string> prop) : base(roomsSize, prop)
        {
        }
    }
}
