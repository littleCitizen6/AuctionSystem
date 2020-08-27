using AuctionSystem.Common;
using AuctionSystem.SellsInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionSystem.SellsManeger
{
    public class MasSellManeger : ISellManeger
    {
        private List<IBuyer> _disposeAtEnd;
        private IDisplayer _displayer;
        private Object _locker = new object();
        private event Action<ISellManeger> onOffer;
        public ISellInfo SellInfo { get; set; }
        
        public MasSellManeger(IDisplayer displayer, ISellInfo info)
        {
            SellInfo = info;
            _displayer = displayer;
            _disposeAtEnd = new List<IBuyer>();
        }

        public void Offer(IBuyer buyer, double price)
        {
            bool offerIsValid = false;
            lock (_locker)
            {
                offerIsValid = OfferIsValid(price);
                if (offerIsValid)
                {
                    SellInfo.CurrentPrice = price;
                    SellInfo.LeadingBuyer = buyer;
                    SellInfo.LastChange = DateTime.Now;
                    SellInfo.State = SellState.InProgress;
                }
            }
            if (offerIsValid)// same if as before because i want the lock part will be as short as i can
            {
                onOffer?.Invoke(this);
                _displayer.Display($"buyer {buyer.Name} is now leading with offer {price} for {SellInfo.Id} sell on {SellInfo.Product.Properties["name"]} ");
            }
        }

        private bool OfferIsValid(double price)
        {
            return price >= SellInfo.CurrentPrice + SellInfo.MinGrowth;
        }

        public void SellOver()
        {
            if (_disposeAtEnd.Count > 0)
            {
                _displayer.Display($"the sell {SellInfo.Id} of {SellInfo.Product.Properties["name"]} is over, buyer {SellInfo.LeadingBuyer.Name} has won... congradulation!!!!! ");
                _disposeAtEnd.ForEach(buyer => onOffer -= buyer.IsWantToRaise);
            }
            else
            {
                _displayer.Display($"the sell {SellInfo.Id} of {SellInfo.Product.Properties["name"]} is canceled because no one was intresting");
            }
            SellInfo.State = SellState.finished;//atomic function
        }

        public void Subscribe(IBuyer buyer)
        {
            onOffer += buyer.IsWantToRaise;
            _disposeAtEnd.Add(buyer);
            _displayer.Display($"agent {buyer.Name} have subscribe for {SellInfo.Id}");
            SellInfo.Participates.Add(buyer);
        }
        public void StartSell() //notice that this happend after the event
        {
            _displayer.Display($"start sell {SellInfo.Id} on {SellInfo.Product.Properties["name"]}");
            Thread.Sleep(SellInfo.IntervalTime);
            _displayer.Display($"finish wait for subscriptions for sell {SellInfo.Id}");
            if (_disposeAtEnd.Count==0)
            {
                SellOver();
            }
            else
            {
                Task manege = ManegeSell();
                manege.Wait();
            }
        }
        private async Task ManegeSell()
        {
            var lastChanged = SellInfo.LastChange;
            while (SellInfo.State < SellState.finished)
            {
                if (onOffer != null)
                {
                    var delegates = onOffer.GetInvocationList();
                    Parallel.ForEach(delegates, d => d.DynamicInvoke(this));
                }
                await Task.Delay(SellInfo.IntervalTime);
                if (lastChanged == SellInfo.LastChange)
                {
                    lock (_locker)
                    {
                        SellInfo.State++;// not atomic func
                    }
                }
                else
                {
                    lastChanged = SellInfo.LastChange;// atomic func
                }
            }
            SellOver();
        }
    }
}
