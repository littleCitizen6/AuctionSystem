using AuctionSystem.Common;
using AuctionSystem.SellsManeger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.AuctionManeger
{
    public interface IAuctionManeger
    {
        Dictionary<DateTime, List<ISellManeger>> Sells { get; set; }
        void Subscribe(IBuyer buyer);
        void Run();
        bool AddSell();
    }
}
