using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.Common.Extensions
{
    public static class StructExtension
    {
        public static int OnlyPositiveSun(this int num1, int num2)
        {
            if (num1 - num2 < 0)
            {
                return 0;
            }
            return num1 - num2;
        }
        public static int ZeroIfNegative(this int num1) 
        {
            if (num1 < 0) 
            {
                return 0;
            }
            return num1;
        }
    }
}
