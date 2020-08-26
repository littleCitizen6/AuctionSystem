using AuctionSystem.Common;
using AuctionSystem.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsInfo
{
    public interface ISellInfo
    {
        IBuyer LeadingBuyer { get; set; }
        IProduct Product { get; set; }
        double StartPrice { get; set; }
        double CurrentPrice { get; set; }
        double MinGrowth { get; set; }
        DateTime Time { get; set; }
        int Id { get; set; }
    }
}
