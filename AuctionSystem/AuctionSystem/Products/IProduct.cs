using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Products
{
    public interface IProduct
    {
        Dictionary<string,string> Properties { get; set; }
    }
}
