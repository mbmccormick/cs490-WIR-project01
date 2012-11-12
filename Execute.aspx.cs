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

namespace BillboardAnalyzer
{
    public partial class Execute : System.Web.UI.Page
    {
        private const string apiKey = "ae4a5799bcf796a4d4d300dee30ecfcc";

        protected void Page_Load(object sender, EventArgs e)
        {
            // initialize web client
            WebClient client = new WebClient();

            // download billboard top 100 songs
            string response1 = client.DownloadString("http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=1&page_size=100&country=us&f_has_lyrics=1&apikey=" + apiKey);
            
            // parse songs from chart response
            var chart = Json.Decode(response1).message.body.track_list;
            foreach (var song in chart)
            {
                // download lyrics for this song
                string response2 = client.DownloadString("http://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id=" + song.track.track_id + "&apikey=" + apiKey);

                // parse lyrics for track response
                var lyrics = Json.Decode(response2).message.body.lyrics.lyrics_body;

                // save lyrics to disk
                File.WriteAllText(Server.MapPath("~/Data") + "\\" + song.track.track_id + ".txt", lyrics);
            }

            // dispose web client
            client.Dispose();
        }
    }
}