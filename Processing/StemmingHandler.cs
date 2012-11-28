using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;

namespace TuneRank.Processing
{
    public class StemmingHandler
    {
        // define Mashape API key
        private const string apiKey = "anJzcmJybXdtdjVpaWw5YXRlZGt6aThjdGdka3ZpOjE1ZjQ3OTRmMzM5Yjc2M2I0NzgyYjQ0NmEwZDEzMGVmMDk4MzY4M2Q";

        public static string Process(string input)
        {
            // initialize web client
            WebClient client = new WebClient();

            // define parameters for stemming
            string parameters = "language=english&" +
                                "stemmer=porter&" +
                                "text=" + input;

            // submit document for stemming
            client.Headers.Add("X-Mashape-Authorization", apiKey);
            string response = client.UploadString("https://japerk-text-processing.p.mashape.com/stem/", parameters);

            // extract result from HTTP response
            return Json.Decode(response).text;
        }
    }
}