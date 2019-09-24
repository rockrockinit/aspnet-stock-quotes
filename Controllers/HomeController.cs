using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stocks.Models;

namespace Stocks.Controllers
{
    public class HomeController : Controller
    {
        protected IConfiguration config;
        protected string baseUrl = "";
        protected string token = "";

        public HomeController(IConfiguration configuration)
        {
            config = configuration;
            baseUrl = config.GetValue<string>("api:iex:url");
            token = config.GetValue<string>("api:iex:token");
        }

        /// <summary>
        ///     Handles stock quote requests
        /// </summary>
        /// <see href="http://unirest.io/net.html">Lightweight HTTP library</see>
        /// <see href="https://iexcloud.io/docs/api/#quote">API Documentation</see>
        /// <see href="https://code-maze.com/different-ways-consume-restful-api-csharp/">Consume Rest Requests</see>
        /// <returns>View</returns>
        public IActionResult Index(string symbol)
        {
            var quote = JObject.Parse("{}");

            if (!String.IsNullOrEmpty(symbol))
            {
                var url = $"{baseUrl}stable/stock/{symbol}/quote?token={token}";

                System.Diagnostics.Debug.WriteLine(url);

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    request.Method = "GET";

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var sr = new StreamReader(stream))
                            {
                                var content = sr.ReadToEnd();
                                quote = JObject.Parse(content);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var model = new QuoteViewModel {
                Quote = quote,
                Symbol = symbol
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
