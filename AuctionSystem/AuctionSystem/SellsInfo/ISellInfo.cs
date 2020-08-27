using AuctionSystem.Common;
using AuctionSystem.Products;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsInfo
{
    public enum SellState 
    {
        Pending,
        WaitingForSubscriptions,
        InProgress,
        LastCall,
        finished
    }
    public interface ISellInfo
    {
        IBuyer ?LeadingBuyer { get; set; }
        IProduct Product { get; set; }
        double StartPrice { get; set; }
        double CurrentPrice { get; set; }
        double MinGrowth { get; set; }
        DateTime StartTime { get; set; }
        DateTime ?LastChange { get; set; }
        int Id { get; set; }
        SellState State { get; set; }
        int IntervalTime { get; set; }
        ConcurrentBag<IBuyer> Participates { get; set; }
        ConcurrentQueue<Offer> OfferHistory { get; set; }
    }
}
