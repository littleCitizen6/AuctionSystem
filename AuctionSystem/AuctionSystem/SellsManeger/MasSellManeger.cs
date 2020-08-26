using AuctionSystem.Common;
using AuctionSystem.SellsInfo;
using System;
using System.Collections.Generic;
using System.Text;
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
        
        private double? TimeFromLastCall => (DateTime.Now - SellInfo.LastChange).TotalMilliseconds;//ToDo: check if necessery

        public MasSellManeger(IDisplayer displayer)
        {
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
                }
            }
            if (offerIsValid)// same if as before because i want the lock part will be as short as i can
            {
                onOffer?.Invoke(this);
            }
        }

        private bool OfferIsValid(double price)
        {
            return price > SellInfo.CurrentPrice;
        }

        public void SellOver()
        {
            if (_disposeAtEnd.Count > 0)
            {
                _displayer.Display($"the sell {SellInfo.Id} of {SellInfo.Product.Properties["name"]} is over, buyer {SellInfo.LeadingBuyer.Name} has won... congeadulaition!!!!! ");
                _disposeAtEnd.ForEach(buyer => onOffer -= buyer.IsWantToRaise);
            }
            else
            {
                _displayer.Display($"the sell {SellInfo.Id} of {SellInfo.Product.Properties["name"]} is over because no one was intresting");
            }
            SellInfo.State = SellState.finished;
        }

        public void Subscribe(IBuyer buyer)
        {
            onOffer += buyer.IsWantToRaise;
            _disposeAtEnd.Add(buyer);
            if (SellInfo.State != SellState.InProgress)
            {
                SellInfo.State = SellState.InProgress;
            }
        }
        //ToDo:make start method() task.delay => if agents =>start sell >><< close  
        public void Start() { }
    }
}
