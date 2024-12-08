using AuctionSystem.Common;
using AuctionSystem.SellsManeger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agents
{
    public class PercentageAgent : Agent
    {
        private int _percentage;
        private Random _rander;
        private IDisplayer _displayer;
        public PercentageAgent(string name, int precentage, IDisplayer displayer) : base(name)
        {
            _percentage = precentage;
            _rander = new Random();
            _displayer = displayer;
        }

        public override void IsIntresting(ISellManeger sellManeger)
        {
            _displayer.Display($"{Name} get event for start sell of sell {sellManeger.SellInfo.Id}");
            Thread t = new Thread(() => {
                _displayer.Display($"{Name} started run  task at sell {sellManeger.SellInfo.Id}");
                if (_rander.Next(1,101)<=_percentage && sellManeger.SellInfo.LeadingBuyer != this && sellManeger.SellInfo.State != AuctionSystem.SellsInfo.SellState.finished)
                {
                    _displayer.Display($"{Name} wanna subscribe for sell of sell {sellManeger.SellInfo.Id}");
                    sellManeger.Subscribe(this);
                }
            });
            t.Start();
        }

        public override void IsWantToRaise(ISellManeger sellManeger)
        {
            _displayer.Display($"{Name} get event for raise sell of sell {sellManeger.SellInfo.Id}");
            Thread t = new Thread(() => { 
                if (_rander.Next(1, 101) <= _percentage && sellManeger.SellInfo.LeadingBuyer != this)
                {
                    sellManeger.Offer(new Offer(this, sellManeger.SellInfo.CurrentPrice + sellManeger.SellInfo.MinGrowth));
                }
            });
            t.Start();
        }
    }
}
