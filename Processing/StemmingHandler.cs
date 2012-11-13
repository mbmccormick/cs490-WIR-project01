using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;

namespace BillboardAnalyzer.Processing
{
    public class StemmingHandler
    {
        private const string apiKey = "anJzcmJybXdtdjVpaWw5YXRlZGt6aThjdGdka3ZpOjE1ZjQ3OTRmMzM5Yjc2M2I0NzgyYjQ0NmEwZDEzMGVmMDk4MzY4M2Q";

        public static string Process(string input)
        {
            WebClient client = new WebClient();

            string parameters = "language=english&" +
                                "stemmer=porter&" +
                                "text=" + input;

            client.Headers.Add("X-Mashape-Authorization", apiKey);
            string response = client.UploadString("https://japerk-text-processing.p.mashape.com/stem/", parameters);

            return Json.Decode(response).text;
        }
    }
}