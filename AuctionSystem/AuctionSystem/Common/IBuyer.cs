using AuctionSystem.SellsManeger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Common
{
    public interface IBuyer
    {
        string Name { get; set; }
        void IsIntresting(ISellManeger sellManeger);
        void IsWantToRaise(ISellManeger sellManeger); //ToDo: check if need to add lastCall

    }
}
