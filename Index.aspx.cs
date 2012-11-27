using BillboardAnalyzer.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillboardAnalyzer
{
    public partial class Index : System.Web.UI.Page
    {
        public static string[] LoveCategory = new string[] { "love", "love", "sweet", "heart" };
        public static string[] HappyCategory = new string[] { "smile", "shine", "bright", "sunlight", "joy", "happy" };
        public static string[] SadCategory = new string[] { "sad", "dark", "cry", "tear", "storm", "scream", "torn", "pain" };
        public static string[] ProfanityCategory = new string[] { "fuck", "bitch", "slut", "ass", "damn" };

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Building index.<br />\n");

            Processing.Index index = new Processing.Index(Directory.GetFiles(Server.MapPath("~/Data/Output"), "*.txt"));

            Response.Write("Calculating weight for love songs.<br />\n");

            string results1 = WeightHandler.Process(index, LoveCategory);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\love.txt", results1);

            Response.Write("Calculating weight for happiest songs.<br />\n");

            string results2 = WeightHandler.Process(index, HappyCategory);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\happy.txt", results2);

            Response.Write("Calculating weight for saddest songs.<br />\n");

            string results3 = WeightHandler.Process(index, SadCategory);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\sad.txt", results3);

            Response.Write("Calculating weight for profanity songs.<br />\n");

            string results4 = WeightHandler.Process(index, ProfanityCategory);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\profanity.txt", results4);

            Response.Write("Calculating weight for repetitive songs.<br />\n");

            string results5 = WeightHandler.ProcessUniqueTerms(index);
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\repetitive.txt", results5);

            Response.Write("Done.<br />\n");
        }
    }
}