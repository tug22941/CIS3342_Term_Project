<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VideoGameStoreWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Video Game Store - Login</title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Styles/main.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
</head>
<body class="authContainer">
    <form id="form1" runat="server">   
        <div id="divLogin" class="loginForm" runat="server">		
			<div class="alignCenter">
                <h2>Login</h2>
                <div id="divAlertLogin" visible="false" runat="server" class="alert alert-danger"></div>
                <asp:TextBox ID="txtLoginUsername" CssClass="loginInputs" runat="server" placeholder="Username"></asp:TextBox>
                <asp:TextBox ID="txtLoginPassword" CssClass="loginInputs" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>                
			</div>		
            <asp:CheckBox ForeColor="White" ID ="chkRememberMe" runat="server"  Text ="Remember Me"></asp:CheckBox>
			<div>                
				<div class="row">
                    <div class="col-sm-12">
                         <asp:Button OnClick="btnLogin_Click" ID="btnLogin" Font-Bold="true" CssClass="button button-blue" runat="server" Text="LOGIN" style="width:100%"/>
                    </div>                   
				</div>
			</div>            
			<hr style="margin:15px 0"/>
            <span style="color:#fff; margin-right:7px">New user?</span>
			<a style="margin-top:15px;color:#ffc251" href="Register.aspx">Register</a>
		</div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
</body>
</html>

