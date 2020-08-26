using AuctionSystem.SellsManeger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agents
{
    public class PercentageAgent : Agent
    {
        private int _percentage;
        private Random _rander;
        public PercentageAgent(string name, int precentage) : base(name)
        {
            _percentage = precentage;
            _rander = new Random();
        }

        public override void IsIntresting(ISellManeger sellManeger)
        {
            Task t = new Task(() => { 
                if(_rander.Next(1,101)<=_percentage && sellManeger.SellInfo.LeadingBuyer != this)
                {
                    sellManeger.Subscribe(this);
                }
            });
            t.Start();
        }

        public override void IsWantToRaise(ISellManeger sellManeger)
        {
            Task t = new Task(() => { 
                if (_rander.Next(1, 101) <= _percentage && sellManeger.SellInfo.LeadingBuyer != this)
                {
                    sellManeger.Offer(this, sellManeger.SellInfo.CurrentPrice + sellManeger.SellInfo.MinGrowth);
                }
            });
            t.Start();
        }
    }
}
