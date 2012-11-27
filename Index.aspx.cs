using TuneRank.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TuneRank
{
    public partial class Index : System.Web.UI.Page
    {
        public static string[] LoveCategory = new string[] { "love", "sweet", "heart" };
        public static string[] HappyCategory = new string[] { "smile", "shine", "bright", "sunlight", "joy", "happy" };
        public static string[] SadCategory = new string[] { "sad", "dark", "cry", "tear", "storm", "scream", "torn", "pain" };
        public static string[] ProfanityCategory = new string[] { "fuck", "bitch", "slut", "ass", "damn", "shit" };

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Building index.<br />\n");

            // build the index
            Processing.Index index = new Processing.Index(Directory.GetFiles(Server.MapPath("~/Data/Output"), "*.txt"));

            // stem the queries
            string[] stemmedQuery1 = StemQueryArray(LoveCategory);
            string[] stemmedQuery2 = StemQueryArray(HappyCategory);
            string[] stemmedQuery3 = StemQueryArray(SadCategory);
            string[] stemmedQuery4 = StemQueryArray(ProfanityCategory);

            // calculate weight for love songs
            Response.Write("Calculating weight for love songs.<br />\n");

            string results1 = WeightHandler.Process(index, stemmedQuery1);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\love.txt", results1);

            // calculate weight for happy songs
            Response.Write("Calculating weight for happiest songs.<br />\n");

            string results2 = WeightHandler.Process(index, stemmedQuery2);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\happy.txt", results2);

            // calculate weight for sad songs
            Response.Write("Calculating weight for saddest songs.<br />\n");

            string results3 = WeightHandler.Process(index, stemmedQuery3);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\sad.txt", results3);

            // calculate weight for profane songs
            Response.Write("Calculating weight for profanity songs.<br />\n");

            string results4 = WeightHandler.Process(index, stemmedQuery4);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\profanity.txt", results4);

            // calculate weight for repetitive songs
            Response.Write("Calculating weight for repetitive songs.<br />\n");

            string results5 = WeightHandler.ProcessUniqueTerms(index);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\repetitive.txt", results5);

            Response.Write("Done.<br />\n");
        }

        private string[] StemQueryArray(string[] query)
        {
            string[] result = new string[query.Count()];

            for (int i = 0; i < query.Count(); i++)
                result[i] = StemmingHandler.Process(query[i]);

            return result;
        }
    }
}