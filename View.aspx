<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="BillboardAnalyzer.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Tunerank</title>
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
            margin: 60px 0;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="jumbotron">
                <a href="/">
                    <h1>Tunerank</h1>
                </a>
                <br />
                <p class="lead">Lyric analysis of the Billboard Top 100 Chart.</p>
            </div>

            <hr />

            <div class="row">
                <div class="span2">
                    <asp:Image ID="imgAlbum" CssClass="img-polaroid" runat="server" Width="100" />
                </div>
                <div class="span7">
                    <h2>
                        <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                    <h4>by
                    <asp:Label ID="lblArtist" runat="server"></asp:Label></h4>
                </div>
            </div>
            <br />

            <div class="row">
                <p style="margin-left: 30px;">
                    <asp:Label ID="lblLyrics" runat="server"></asp:Label></p>
            </div>
            <br />


            <hr />

            <footer>
                <p>Built by <a href="http://twitter.com/mbmccormick" target="_blank">@mbmccormick</a> and <a href="http://twitter.com/martellaj" target="_blank">@martellaj</a></p>
            </footer>

        </div>

        <script src="/Content/js/jquery.js"></script>
        <script src="/Content/js/bootstrap.js"></script>
    </form>
</body>
</html>
