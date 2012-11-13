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
    public partial class Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Running text pre-processing.<br />\n");

            // loop through all of the lyrics
            foreach (string path in Directory.GetFiles(Server.MapPath("~/Data"), "*.txt"))
            {
                // skip the index file
                if (path.EndsWith("index.txt") == true)
                    continue;
                
                // initialize stream reader
                StreamReader sr = new StreamReader(path);

                // read contents of file
                string contents = sr.ReadToEnd();

                Response.Write("Stemming and removing stopwords for " + Path.GetFileName(path) + ".<br />\n");

                // stem and remove stopwords
                contents = StemmingHandler.Process(contents);
                contents = StopwordsHandler.Process(contents);

                // close stream reader
                sr.Dispose();

                // replace file on disk
                File.Delete(path);
                File.WriteAllText(path, contents);
            }

            Response.Write("Done.<br />\n");
        }
    }
}