using AuctionSystem.Common;
using AuctionSystem.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsInfo
{
    public class BasicSellInfo : ISellInfo
    {
        public BasicSellInfo(IProduct product, double startPrice, double minGrowth, DateTime startTime, int intervalTime, int id)
        {
            Product = product;
            StartPrice = startPrice;
            CurrentPrice = startPrice;
            MinGrowth = minGrowth;
            StartTime = startTime;
            IntervalTime = intervalTime;
            Id = id;
            State = SellState.Pending;
            Participates = new List<IBuyer>();

        }

        public IProduct Product { get ; set ; }
        public double StartPrice { get ; set; }
        public double CurrentPrice { get; set; }
        public double MinGrowth { get ; set ; }
        public DateTime StartTime { get ; set ; }
        public int IntervalTime { get; set; }
        public int Id { get ; set ; }
        public IBuyer ?LeadingBuyer { get; set; }
        public DateTime ?LastChange { get; set; }
        public SellState State { get; set; }
        public List<IBuyer> Participates { get; set ; }
    }
}
