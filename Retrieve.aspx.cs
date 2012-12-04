using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using System.Data;
using System.IO;
using System.Text;

namespace TuneRank
{
    public partial class Retrieve : System.Web.UI.Page
    {
        // define MusixMatch API key
        private const string apiKey = "cbd3340887455ec7c422dd1564e02ec6";

        protected void Page_Load(object sender, EventArgs e)
        {
            // delete previous Data directory and recreate
            if (Directory.Exists(Server.MapPath("~/Data")) == false)
                Directory.CreateDirectory(Server.MapPath("~/Data"));

            // delete previous Data directory and recreate
            if (Directory.Exists(Server.MapPath("~/Data/Output")) == false)
                Directory.CreateDirectory(Server.MapPath("~/Data/Output"));

            Response.Write("Downloading Billboard Top 100 chart.<br />\n");
            
            // clean data directory
            foreach (string path in Directory.GetFiles(Server.MapPath("~/Data"), "*.txt"))
            {
                File.Delete(path);
            }

            // initialize web client
            WebClient client = new WebClient();

            // download billboard top 100 songs
            string response1 = client.DownloadString("http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=1&page_size=100&country=us&f_has_lyrics=1&apikey=" + apiKey);
            
            // parse songs from chart response
            var chart = Json.Decode(response1).message.body.track_list;

            StringBuilder sb = new StringBuilder();

            // loop through songs in chart
            int i = 1;
            foreach (var song in chart)
            {
                sb.AppendLine(song.track.track_id + ";" + song.track.track_name + ";" + song.track.artist_name);
                
                // download lyrics for this song
                Response.Write("Downloading lyrics for #" + i + " " + song.track.track_name + " by " + song.track.artist_name + ".<br />\n");
                string response2 = client.DownloadString("http://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id=" + song.track.track_id + "&apikey=" + apiKey);

                if (song.track.has_lyrics == 1)
                {
                    // parse lyrics for track response
                    var lyrics = Json.Decode(response2).message.body.lyrics.lyrics_body;

                    // save lyrics to disk
                    File.WriteAllText(Server.MapPath("~/Data") + "\\" + song.track.track_id + ".txt", lyrics);
                }

                i++;
            }

            // save index to disk
            File.WriteAllText(Server.MapPath("~/Data") + "\\index.txt", sb.ToString());

            // dispose web client
            client.Dispose();

            Response.Write("Done.<br />\n");
        }
    }
}