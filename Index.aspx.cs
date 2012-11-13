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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Building index.<br />\n");

            Processing.Index index = new Processing.Index(Directory.GetFiles(Server.MapPath("~/Data/Output"), "*.txt"));

            Response.Write("Calculating weight.<br />\n");

            string results = WeightHandler.Process(index, new string[] { "fuck", "shit", "ass", "bitch", "damn" });
            File.WriteAllText(Server.MapPath("~/Data/Output") + "\\index.txt", results);

            Response.Write("Done.<br />\n");
        }
    }
}