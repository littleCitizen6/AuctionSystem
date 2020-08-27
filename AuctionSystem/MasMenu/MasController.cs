using AuctionSystem.AuctionManeger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
