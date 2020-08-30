using AuctionSystem.AuctionManeger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionSystem.SellsInfo;

namespace MasMenu
{
    public class MasController
    {
        IAuctionManeger _maneger;
        public MasController(IAuctionManeger maneger)
        {
            _maneger = maneger;
        }

        public string GetFutureAuctions(string userInput)
        {
            StringBuilder builder = new StringBuilder();
            _maneger.AllSells.Where(sell => sell.SellInfo.StartTime > DateTime.Now).ToList().ForEach(sell => 
            {
                builder.AppendLine($"product {sell.SellInfo.Product.Properties["name"]}, have start price of {sell.SellInfo.StartPrice}, and start at {sell.SellInfo.StartTime}");
                sell.SellInfo.Product.Properties.Keys.ToList().ForEach(prop => 
                {
                    builder.AppendLine($"his prop {prop} is {sell.SellInfo.Product.Properties[prop]}");
                });
                builder.AppendLine();
            });
            return builder.ToString();
        }
        public string GetCurrentAuctions(string userInput)
        {
            StringBuilder builder = new StringBuilder();
            _maneger.AllSells.Where(sell => sell.SellInfo.State < SellState.finished && sell.SellInfo.State > SellState.WaitingForSubscriptions).ToList().ForEach(sell =>
            {
                var timeToEnd = sell.SellInfo.IntervalTime - (((DateTime)sell.SellInfo.LastChange) - DateTime.Now).TotalMilliseconds ;
                builder.AppendLine($"last price that offered is {sell.SellInfo.CurrentPrice}, these round end in {timeToEnd} miliseconds there is {sell.SellInfo.OfferHistory.Count}");
                sell.SellInfo.OfferHistory.ToList().ForEach(offer =>
                {
                    builder.AppendLine($"the offer is {offer.Price} by {offer.Buyer.Name} at {offer.Time}");
                });
                builder.AppendLine();
            });
            return builder.ToString();
        }
        public string GetFinishedAuctions
            (string userInput)
        {
            StringBuilder builder = new StringBuilder();
            _maneger.AllSells.Where(sell => sell.SellInfo.State == SellState.finished).ToList().ForEach(sell =>
            {
                builder.AppendLine($"auction of {sell.SellInfo.Product.Properties["name"]}, have finished in price of {sell.SellInfo.CurrentPrice}, by {sell.SellInfo.LeadingBuyer.Name}");
            });
            return builder.ToString();
        }
    }
}
