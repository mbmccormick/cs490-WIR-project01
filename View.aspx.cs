using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TuneRank
{
    public partial class View : System.Web.UI.Page
    {
        // define MusixMatch API key
        private const string apiKey = "ae4a5799bcf796a4d4d300dee30ecfcc";

        protected void Page_Load(object sender, EventArgs e)
        {
            // extract the MusixMatch song ID from the query string
            int trackId = Convert.ToInt32(Request.QueryString["id"]);

            // initialize web client
            WebClient client = new WebClient();
            string response1 = client.DownloadString("http://api.musixmatch.com/ws/1.1/track.get?track_id=" + trackId + "&apikey=" + apiKey);

            // fetch the track information from MusixMatch
            var track = Json.Decode(response1).message.body.track;

            // output the track and artist name
            this.lblTitle.Text = track.track_name;
            this.lblArtist.Text = track.artist_name;

            // read the previously downloaded lyrics from disk
            StreamReader sr = new StreamReader(Server.MapPath("~/Data/" + track.track_id + ".txt"));
            this.lblLyrics.Text = sr.ReadToEnd().Replace("\n", "<br />\n");

            // download the Spotify embed component
            string response2 = client.DownloadString("http://ws.spotify.com/search/1/track.json?q=" + track.track_name);

            if (Json.Decode(response2).tracks[0] != null)
            {
                // parse the Spotify URI for this song
                var spotifyUri = Json.Decode(response2).tracks[0].href;

                // output the Spotify embed component
                this.litPlayButton.Text = "<iframe src='https://embed.spotify.com/?uri=" + spotifyUri + "' width='400' height='480' frameborder='0' allowtransparency='true'></iframe>";
            }
        }
    }
}