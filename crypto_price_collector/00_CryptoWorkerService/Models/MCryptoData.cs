using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Models
{
    public class MCryptoData
    {
        public string id { get; set; } = "";
        public string symbol { get; set; } = "";
        public string name { get; set; } = "";

        public _market_data market_data {get;set;}

        public class _market_data
        {
            public _current_price current_price { get; set; }
        }
        public class _current_price
        {
            public double myr { get; set; }
            public double usd { get; set; }
        }
    }
}
