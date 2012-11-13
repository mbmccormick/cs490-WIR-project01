using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillboardAnalyzer
{
    public partial class Default : System.Web.UI.Page
    {
        private const string apiKey = "ae4a5799bcf796a4d4d300dee30ecfcc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.litLoveSongs.Text = LoadSongs(Server.MapPath("~/Data/Output/love.txt"));
            this.litHappy.Text = LoadSongs(Server.MapPath("~/Data/Output/happy.txt"));
            this.litSad.Text = LoadSongs(Server.MapPath("~/Data/Output/sad.txt"));
            this.litProfaneSongs.Text = LoadSongs(Server.MapPath("~/Data/Output/profanity.txt"));
            this.litRepetitiveSongs.Text = LoadSongs(Server.MapPath("~/Data/Output/repetitive.txt"));
        }

        private string LoadSongs(string path)
        {
            StreamReader sr = new StreamReader(path);
            string love = sr.ReadToEnd();

            Dictionary<int, double> loveSongs = new Dictionary<int, double>();
            foreach (string line in love.Split('\n'))
            {
                try
                {
                    loveSongs.Add(Convert.ToInt32(line.Split(' ')[0]), Convert.ToDouble(line.Split(' ')[1]));
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            // initialize web client
            WebClient client = new WebClient();

            string output = "";
            foreach (KeyValuePair<int, double> song in loveSongs.Take(5))
            {
                string response = client.DownloadString("http://api.musixmatch.com/ws/1.1/track.get?track_id=" + song.Key + "&apikey=" + apiKey);

                var track = Json.Decode(response).message.body.track;

                string albumCover = track.album_coverart_350x350 == "" ? track.album_coverart_100x100 : track.album_coverart_350x350;

                output += "<div class='span2'><img class='img-polaroid' width='150' height='150' src='" + albumCover + "'></a><h4><a href='View.aspx?id=" + track.track_id + "'>" + track.track_name + "</a></h4><p>by " + track.artist_name + "</p></div>\n";
            }

            return output;
        }
    }
}