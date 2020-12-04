<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountConfirmation.aspx.cs" Inherits="VideoGameStoreWeb.AccountConfirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Game Store - Account Verification</title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Styles/main.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
</head>
<body>
    <form id="form1" runat="server">    
    <header>
		<h2 class="main">My Video Game Store</h2>				
	</header>          
    <main>
        <div visible="false" runat="server" id="divUserVerified" class="alert alert-success"><span>Your Account has been verified and you may now</span><a style="margin-top: 15px;margin-left:5px" href="Login.aspx">Log in</a></div>
    </main>
        <br />
			<footer>
				<p>My Video Game Store &copy;2020 | Contact info: Ethan: <a href="mailto:email@gmail.com">email@gmail.com</a></p>				
			</footer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>       
    </form>
</body>
</html>
