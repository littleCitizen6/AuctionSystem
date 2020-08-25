using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Products
{
    public class Building : IProduct
    {
        public Dictionary<string, string> Properties { get ; set ; }
        public Building(List<double> roomsSize, Dictionary<string,string> prop)
        {
            Properties = prop;
            Properties.Add("roomsCount", roomsSize)
        }
    }
}
