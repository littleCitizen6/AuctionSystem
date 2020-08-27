using Agents;
using AuctionSystem.Common;
using AuctionSystem.Products;
using AuctionSystem.SellsInfo;
using AuctionSystem.SellsManeger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MAS.TEST.Agents
{
    [TestClass]
    public class PercentageAgentTest
    {
        private IDisplayer _displayer;
        private ISellManeger _sell;
        [ClassInitialize]
       /* public void ClassInit()
        {
            IDisplayer displayer = new ConsoleDisplayer();
        }*/
        [TestInitialize]
        public void TestInit()
        {
            _displayer = new ConsoleDisplayer();
            List<double> rooms60 = new List<double>();
            rooms60.Add(34.5);
            rooms60.Add(24);
            rooms60.Add(30.8);
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("name", "havered 60");
            House house = new House(rooms60, prop);
            ISellInfo info = new BasicSellInfo(house, 300, 25, DateTime.Now.AddSeconds(12), 5000, 1);
            _sell = new MasSellManeger(_displayer, info);
        }
        [TestMethod]
        public void CheckIfIntresting()
        {
            //arrange
            PercentageAgent agent = new PercentageAgent("carmel", 100, _displayer);

            //act
            agent.IsIntresting(_sell);

            //assert
            Assert.AreEqual(agent, _sell.SellInfo.Participates.First());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _sell.SellOver(); // dispose all event of sell
        }
    }
}
