using System;
using System.Collections.Generic;
using Agents;
using AuctionSystem.AuctionManeger;
using AuctionSystem.Common;
using AuctionSystem.Products;
using AuctionSystem.SellsInfo;
using AuctionSystem.SellsManeger;

namespace RunChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDisplayer displayer = new ConsoleDisplayer();
            MasManeger mas = new MasManeger(displayer);
            Agent lior = new PercentageAgent("lior", 100, displayer);
            Agent dor = new PercentageAgent("dor", 50, displayer);
            Agent ron = new PercentageAgent("ron", 50, displayer);
            List<double> rooms60 = new List<double>();
            rooms60.Add(34.5);
            rooms60.Add(24);
            rooms60.Add(30.8);
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("name","havered 60");
            House house = new House(rooms60, prop);
            ISellInfo info = new BasicSellInfo(house, 300, 25, DateTime.Now.AddSeconds(12), 5000,1);
            ISellManeger sellManeger = new MasSellManeger(displayer, info);
            mas.AddSell(sellManeger);

            List<double> officeRooms = new List<double>();
            officeRooms.Add(32);
            officeRooms.Add(41.3);
            officeRooms.Add(33.3);
            Dictionary<string, string> prop2 = new Dictionary<string, string>();
            prop2.Add("name", "apple Office");
            Office office = new Office(officeRooms, prop2);
            ISellInfo info2 = new BasicSellInfo(office, 800, 30, DateTime.Now.AddSeconds(11), 5000, 2);
            ISellManeger sellManeger2 = new MasSellManeger(displayer, info2);
            mas.AddSell(sellManeger2);

            mas.Subscribe(dor);
            mas.Subscribe(lior);
            mas.Subscribe(ron);
            mas.Run();

            mas.Dispose();
            
        }
    }
}
