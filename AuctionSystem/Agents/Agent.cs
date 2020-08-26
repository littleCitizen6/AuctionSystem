using System;
using AuctionSystem.Common;
using AuctionSystem.SellsManeger;

namespace Agents
{
    public abstract class Agent : IBuyer
    {
        public string Name { get; set; }
        public Agent(string name)
        {
            Name = name;
        }

        public abstract void IsIntresting(ISellManeger sellManeger);
        public abstract void IsWantToRaise(ISellManeger sellManeger);
    }
}
