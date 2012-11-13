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
    public partial class View : System.Web.UI.Page
    {
        private const string apiKey = "ae4a5799bcf796a4d4d300dee30ecfcc";

        protected void Page_Load(object sender, EventArgs e)
        {
            int trackId = Convert.ToInt32(Request.QueryString["id"]);

            WebClient client = new WebClient();
            string response = client.DownloadString("http://api.musixmatch.com/ws/1.1/track.get?track_id=" + trackId + "&apikey=" + apiKey);

            var track = Json.Decode(response).message.body.track;

            this.lblTitle.Text = track.track_name;
            this.lblArtist.Text = track.artist_name;

            this.imgAlbum.ImageUrl = track.album_coverart_350x350 == "" ? track.album_coverart_100x100 : track.album_coverart_350x350;

            StreamReader sr = new StreamReader(Server.MapPath("~/Data/" + track.track_id + ".txt"));
            this.lblLyrics.Text = sr.ReadToEnd().Replace("\n", "<br />\n");
        }
    }
}