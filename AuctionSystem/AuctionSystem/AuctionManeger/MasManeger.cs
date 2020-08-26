using AuctionSystem.Common;
using AuctionSystem.SellsManeger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.AuctionManeger
{
    public class MasManeger : IAuctionManeger
    {
        private List<IBuyer> _dispeseAtEnd;
        private event Action<ISellManeger> onStart;
        public ConcurrentDictionary<DateTime, List<ISellManeger>> Sells { get; set; }
        public MasManeger()
        {
            _dispeseAtEnd = new List<IBuyer>();
        }
        public void AddSell(ISellManeger sellManeger) //Todo: validate Sell StartDate
        {
            if (!Sells.ContainsKey(sellManeger.SellInfo.StartTime))
            {
                Sells.TryAdd(sellManeger.SellInfo.StartTime, new List<ISellManeger>());
            }
            Sells[sellManeger.SellInfo.StartTime].Add(sellManeger);
        }

        public void Run()
        {
            while (Sells.Keys.Count>0)
            {
                List<ISellManeger> sellsAtTime;
                DateTime excTime = Sells.Keys.Min();
                if (Sells.TryRemove(excTime, out sellsAtTime))
                {
                    Task executer = new Task(() =>
                    {
                        while (excTime > DateTime.Now)
                        {
                            Task.Delay(excTime - DateTime.Now);
                        }
                        Parallel.ForEach<ISellManeger>(sellsAtTime, (sell) => {
                            onStart?.Invoke(sell);
                            sell.StartSell();
                            }
                        );//ToDo: add start sell for each sell and combine it in event
                    });
                }
            }
        }

        public void Subscribe(IBuyer buyer)
        {
            onStart += buyer.IsIntresting;
            _dispeseAtEnd.Add(buyer);
        }
    }
}
