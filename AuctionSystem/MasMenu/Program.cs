using Agents;
using AuctionSystem.AuctionManeger;
using AuctionSystem.Products;
using AuctionSystem.SellsInfo;
using AuctionSystem.SellsManeger;
using MasMenu.MenuHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            NullDisplayer displayer = new NullDisplayer();
            MasManeger mas = new MasManeger(displayer);

            List<double> rooms60 = new List<double>();
            rooms60.Add(34.5);
            rooms60.Add(24);
            rooms60.Add(30.8);
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("name", "havered 60");
            House house = new House(rooms60, prop);
            ISellInfo info = new BasicSellInfo(house, 300, 25, DateTime.Now.AddSeconds(20), 5000, 1);
            ISellManeger sellManeger = new MasSellManeger(displayer, info);
            mas.AddSell(sellManeger);

            Agent lior = new PercentageAgent("lior", 100, displayer);
            Agent dor = new PercentageAgent("dor", 50, displayer);
            Agent ron = new PercentageAgent("ron", 50, displayer);


            mas.Subscribe(dor);
            mas.Subscribe(lior);
            mas.Subscribe(ron);

            MasController controller = new MasController(mas);
            Runner runner = new Runner(controller);
            Task t = new Task(mas.Run);
            t.Start();
            runner.Run();
            
        }
    }
}
