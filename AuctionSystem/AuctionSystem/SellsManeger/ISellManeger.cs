using AuctionSystem.Common;
using AuctionSystem.SellsInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.SellsManeger
{
    public interface ISellManeger
    {
        ISellInfo SellInfo { get; set; }
        void Offer(IBuyer buyer, double price);
        void Subscribe(IBuyer buyer);
        void SellOver();
        void StartSell();
    }
}
