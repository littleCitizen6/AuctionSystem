﻿using AuctionSystem.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsInfo
{
    public class BasicSellInfo : ISellInfo
    {
        public IProduct Product { get ; set ; }
        public double StartPrice { get ; set; }
        public double CurrentPrice { get; set; }
        public double MinGrowth { get ; set ; }
        public DateTime Time { get ; set ; }
        public int Id { get ; set ; }
    }
}
