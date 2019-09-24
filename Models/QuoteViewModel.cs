using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Stocks.Models
{
    public class QuoteViewModel
    {
        public JObject Quote { get; set; }

        public string Symbol = string.Empty;

        public QuoteViewModel() {}
    }
}
