using AuctionSystem.Common;
using AuctionSystem.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsInfo
{
    public enum SellState 
    {
        Pending,
        InProgress,
        LastCall,
        finished
    }
    public interface ISellInfo
    {
        IBuyer LeadingBuyer { get; set; }
        IProduct Product { get; set; }
        double StartPrice { get; set; }
        double CurrentPrice { get; set; }
        double MinGrowth { get; set; }
        DateTime StartTime { get; set; }
        DateTime LastChange { get; set; }
        int Id { get; set; }
        SellState State { get; set; }
        double IntervalTime { get; set; }
    }
}
