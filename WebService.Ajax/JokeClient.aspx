<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JokeClient.aspx.cs" Inherits="WebService.Ajax.JokeClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A simple javascript web service client</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="~/JokeService.svc" />
            </Services>
        </asp:ScriptManager>
        <script type="text/javascript">
            // instanciate service the is created after the 
            // inclusion of the ajax behaivior in web.config:
            // <behavior name="WebService.Ajax.JokeServiceAspNetAjaxBehavior">
            //   <enableWebScript />
            // </behavior>
            // the javascript methods createds can be observed at:
            // http://[localhost]/JokeService.svc/js
            var service = new JokeService();

            // call the add method 
            service.AddJoke({
                "Title": "Bar 31",
                "Question": "What do you call an alligator in vest?",
                "Answer": "An investgator !!!",
                "Grade": 3
            }, function() {
                alert("Joke Added");
            });

            // call the get method. They are two independent asynchronous calls 
            // that are started almost at same time.
            service.GetJokes(function (joke) {
                var table = document.getElementById("jokeList");
                var output = '';
                for (var i = 0; i < joke.length; i++) {
                    output += "<tr><td>"
                        + joke[i].Id
                        + "</td><td>"
                        + joke[i].Title
                        + "</td></tr>";
                }
                table.innerHTML = output;
            });
        </script>
        <div>
            <table id="jokeList"></table>
        </div>
    </form>
</body>
</html>
