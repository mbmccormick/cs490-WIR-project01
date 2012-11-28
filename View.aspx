<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="TuneRank.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>TuneRank</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <link rel="stylesheet" href="Content/css/bootstrap.css" />
    <link rel="stylesheet" href="Content/css/bootstrap-responsive.css" />
    <style type="text/css">
        body {
            padding-top: 20px;
            padding-bottom: 40px;
        }

        .jumbotron {
            margin: 40px 0 60px 0;
            text-align: left;
        }

            .jumbotron h1 {
                font-size: 72px;
                line-height: 1;
            }

            .jumbotron .btn {
                font-size: 21px;
                padding: 14px 24px;
            }

        .highlight {
            background-color: yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="jumbotron">
                <a href="/">
                    <h1>TuneRank</h1>
                </a>
                <br />
                <p class="lead">Lyric analysis of the Billboard Top 100 chart</p>
            </div>

            <hr />

            <div class="row">
                <div class="span6">
                    <h2 style="margin-left: 0px !important;">
                        <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                    <h3 style="margin-left: 0px !important;">by
                    <asp:Label ID="lblArtist" runat="server"></asp:Label></h3>
                    <br />
                    <p>
                        <asp:Label ID="lblLyrics" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="span6" style="text-align: right;">
                    <br />
                    <asp:Literal ID="litPlayButton" runat="server"></asp:Literal>
                </div>
            </div>
            <br />


            <hr />

            <footer>
                <p>Built by <a href="http://twitter.com/mbmccormick" target="_blank">@mbmccormick</a> and <a href="http://twitter.com/martellaj" target="_blank">@martellaj</a></p>
            </footer>

        </div>

        <script src="/Content/js/jquery.js"></script>
        <script src="/Content/js/bootstrap.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                <% string[] category = null; %>
                <% if (Request.QueryString["category"] == "1")
                   { %>                
                <% category = TuneRank.Index.LoveCategory; %>
                <% }
                   else if (Request.QueryString["category"] == "2")
                   { %>
                <% category = TuneRank.Index.HappyCategory; %>
                <% }
                   else if (Request.QueryString["category"] == "3")
                   { %>
                <% category = TuneRank.Index.SadCategory; %>
                <% }
                   else if (Request.QueryString["category"] == "4")
                   { %>
                <% category = TuneRank.Index.ProfanityCategory; %>
                <% } %>
                <% if (category != null)
                   { %>
                <% foreach (string term in category)
                   { %>
                $("p").highlight("<%= term %>");
                <% } %>
                <% } %>
            });
        </script>
    </form>
</body>
</html>
