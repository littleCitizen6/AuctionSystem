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
        Task<bool> IsIntresting(ISellManeger sellManeger);
        Task<bool> IsWantToRaise(ISellManeger sellManeger, out double price); //ToDo: check if need to add lastCall

    }
}
