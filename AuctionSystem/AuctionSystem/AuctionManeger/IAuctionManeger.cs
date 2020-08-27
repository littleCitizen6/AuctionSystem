using AuctionSystem.Common;
using AuctionSystem.SellsManeger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.AuctionManeger
{
    public interface IAuctionManeger
    {
        List<ISellManeger> AllSells { get; set; }
        ConcurrentDictionary<DateTime, List<ISellManeger>> Sells { get; set; }
        void Subscribe(IBuyer buyer);
        void Run();
        void AddSell(ISellManeger sellManeger);
    }
}
