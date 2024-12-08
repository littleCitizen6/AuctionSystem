using AuctionSystem.Common;
using AuctionSystem.Common.Extensions;
using AuctionSystem.SellsInfo;
using AuctionSystem.SellsManeger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.AuctionManeger
{
    public class MasManeger : IAuctionManeger, IDisposable
    {
        private List<IBuyer> _dispeseAtEnd;
        private event Action<ISellManeger> onStart;
        public ConcurrentDictionary<DateTime, List<ISellManeger>> Sells { get; set; }
        public IDisplayer _displayer;
        public List<ISellManeger> AllSells { get; set; }
        public MasManeger(IDisplayer displayer)
        {
            Sells = new ConcurrentDictionary<DateTime, List<ISellManeger>>();
            _displayer = displayer;
            _dispeseAtEnd = new List<IBuyer>();
            AllSells = new List<ISellManeger>();
        }
        public void AddSell(ISellManeger sellManeger) //Todo: validate Sell StartDate
        {
            if (!Sells.ContainsKey(sellManeger.SellInfo.StartTime))
            {
                Sells.TryAdd(sellManeger.SellInfo.StartTime, new List<ISellManeger>());
            }
            Sells[sellManeger.SellInfo.StartTime].Add(sellManeger);
            AllSells.Add(sellManeger);
        }

        public void Run()
        {
            List<Task> tasks = new List<Task>();
            while (Sells.Keys.Count>0)
            {
                List<ISellManeger> sellsAtTime;
                DateTime excTime = Sells.Keys.Min();
                if (Sells.TryRemove(excTime, out sellsAtTime))
                {
                    _displayer.Display($"extract sell {sellsAtTime[0].SellInfo.Id}");
                    Task executer = new Task(() =>{
                        while (excTime > DateTime.Now)
                        {
                            Task.Delay(Convert.ToInt32((excTime - DateTime.Now).TotalSeconds).ZeroIfNegative());
                        }
                        Parallel.ForEach<ISellManeger>(sellsAtTime, (sell) => {
                            _displayer.Display($"starting subscription to sell {sell.SellInfo.Id}");
                            onStart?.Invoke(sell);
                            sell.SellInfo.State = SellState.WaitingForSubscriptions;
                            sell.StartSell();
                            });
                    });
                    tasks.Add(executer);
                    executer.Start();
                }
            }
            WaitForAll(tasks);
        }

        private void WaitForAll(List<Task> tasks)
        {
            foreach (var task in tasks)
            {
                task.Wait();
            }
        }

        public void Subscribe(IBuyer buyer)
        {
            onStart += buyer.IsIntresting;
            _dispeseAtEnd.Add(buyer);
        }

        public void Dispose()
        {
            _dispeseAtEnd.ForEach(buyer => onStart -= buyer.IsIntresting);
            _displayer.Display("displayed all in mas");
        }
    }
}
