using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Common
{
    public class Offer
    {
        public double Price { get; set; }
        public DateTime Time { get; set; }
        public IBuyer Buyer { get; set; }
        public Offer(IBuyer buyer, double price)
        {
            Time = DateTime.Now;
            Buyer = buyer;
            Price = price;
        }
    }
}
