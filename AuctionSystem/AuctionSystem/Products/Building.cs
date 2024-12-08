using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Products
{
    public abstract class Building : IProduct
    {
        public Dictionary<string, string> Properties { get ; set ; }
        public Building(List<double> roomsSize, Dictionary<string,string> prop)
        {
            Properties = prop;
            Properties.TryAdd("roomsCount", roomsSize.Count.ToString());
            for (int i = 0; i < roomsSize.Count; i++)
            {
                Properties.TryAdd($"room{i+1}", roomsSize[i].ToString());
            }
        }
    }
}
