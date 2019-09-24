using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Stocks.Models
{
    public class StockViewModel
    {
        public JObject Stats { get; set; }

        public JObject Quote { get; set; }

        public JObject Logo { get; set; }

        public string Symbol = string.Empty;

        public StockViewModel() {}
    }
}
