using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Exceptions
{
    public class MarketException : Exception
    {
        public int Code { get; set; }

        public MarketException(int code, string message) : base(message)
        {
            this.Code = code;
        }   
    }
}
