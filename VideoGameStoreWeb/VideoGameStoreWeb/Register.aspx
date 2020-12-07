<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RestaurantReviewSystem.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Game Store - Register</title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Styles/main.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
</head>
<body class="authContainer">
    <form id="form1" runat="server">


        <div id="divRegister" class="registerForm" runat="server">
            <div class="alignCenter">
                <h2>User Information</h2>
                <div class="row">
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="(Required)"
                        ControlToValidate="txtFirstName" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="(Required)"
                        ControlToValidate="txtLastName" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone" TextMode="Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="(Required)"
                        ControlToValidate="txtPhone" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="(Required)"
                        ControlToValidate="txtEmail" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="(Required)"
                        ControlToValidate="txtUsername" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="(Required)"
                        ControlToValidate="txtPassword" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                </div>

                <asp:TextBox ID="txtAddress" runat="server" placeholder="Address"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="(Required)"
                        ControlToValidate="txtAddress" ValidationGroup="vgUpdate" ForeColor="Red" />

                <div class="row">
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtCity" runat="server" placeholder="City"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Text="(Required)"
                        ControlToValidate="txtCity" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlState" runat="server">
                            <asp:ListItem Value="">State</asp:ListItem>
                            <asp:ListItem Value="AL">Alabama</asp:ListItem>
                            <asp:ListItem Value="AK">Alaska</asp:ListItem>
                            <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                            <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                            <asp:ListItem Value="CA">California</asp:ListItem>
                            <asp:ListItem Value="CO">Colorado</asp:ListItem>
                            <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                            <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                            <asp:ListItem Value="DE">Delaware</asp:ListItem>
                            <asp:ListItem Value="FL">Florida</asp:ListItem>
                            <asp:ListItem Value="GA">Georgia</asp:ListItem>
                            <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                            <asp:ListItem Value="ID">Idaho</asp:ListItem>
                            <asp:ListItem Value="IL">Illinois</asp:ListItem>
                            <asp:ListItem Value="IN">Indiana</asp:ListItem>
                            <asp:ListItem Value="IA">Iowa</asp:ListItem>
                            <asp:ListItem Value="KS">Kansas</asp:ListItem>
                            <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                            <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                            <asp:ListItem Value="ME">Maine</asp:ListItem>
                            <asp:ListItem Value="MD">Maryland</asp:ListItem>
                            <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                            <asp:ListItem Value="MI">Michigan</asp:ListItem>
                            <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                            <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                            <asp:ListItem Value="MO">Missouri</asp:ListItem>
                            <asp:ListItem Value="MT">Montana</asp:ListItem>
                            <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                            <asp:ListItem Value="NV">Nevada</asp:ListItem>
                            <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                            <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                            <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                            <asp:ListItem Value="NY">New York</asp:ListItem>
                            <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                            <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                            <asp:ListItem Value="OH">Ohio</asp:ListItem>
                            <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                            <asp:ListItem Value="OR">Oregon</asp:ListItem>
                            <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                            <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                            <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                            <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                            <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                            <asp:ListItem Value="TX">Texas</asp:ListItem>
                            <asp:ListItem Value="UT">Utah</asp:ListItem>
                            <asp:ListItem Value="VT">Vermont</asp:ListItem>
                            <asp:ListItem Value="VA">Virginia</asp:ListItem>
                            <asp:ListItem Value="WA">Washington</asp:ListItem>
                            <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                            <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                            <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Text="(Required)"
                        ControlToValidate="ddlState" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtZip" runat="server" placeholder="Zip"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Text="(Required)"
                        ControlToValidate="txtZip" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                         <span style="color:#fff;line-height:40px;font-weight:bold">User Type:</span>
                    </div>
                    <div class="col-sm-9">
                        <asp:RadioButtonList ForeColor="White" ID="rblUserType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Customer" Selected="True">Customer</asp:ListItem>
                    <asp:ListItem Value="Producer">Game Producer</asp:ListItem>
                    <asp:ListItem Value="Store Manager">Store Manager</asp:ListItem>
                </asp:RadioButtonList>
                    </div>
                </div>
                <hr style="border-top:1px solid #ddd"/>
                <h2>Security Questions</h2>
                <div class="row">
                    <div class="col-sm-4">
                        <span style="color:#fff">favorite video game?</span>
                         <asp:TextBox ID="txtFavoriteGame" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Text="(Required)"
                        ControlToValidate="txtFavoriteGame" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-4">
                        <span style="color:#fff">mother's maiden name?</span>
                        <asp:TextBox ID="txtMotherMaidenName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Text="(Required)"
                        ControlToValidate="txtMotherMaidenName" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                    <div class="col-sm-4">
                        <span style="color:#fff">favorite sport?</span>
                        <asp:TextBox ID="txtFavoriteSport" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Text="(Required)"
                        ControlToValidate="txtFavoriteSport" ValidationGroup="vgUpdate" ForeColor="Red" />
                    </div>
                </div>
                <hr style="margin: 15px 0" />
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Button Font-Bold="true" ValidationGroup="vgUpdate" ID="btnRegister" OnClick="btnRegister_Click" CssClass="button button-red" runat="server" Text="REGISTER" Style="width: 100%" />
                    </div>
                </div>               

            </div>
            <hr style="margin: 15px 0" />
                <span style="color:#fff; margin-right:7px">Existing user?</span><a style="margin-top: 15px; color: #ffc251" href="Login.aspx">Log in</a>

            <div class="row mt-3">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </div>
                <div>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                    <asp:Label ID="Label1" runat="server" Text="Label" Enabled="False"></asp:Label>
                </div>
        </div>

        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
</body>
</html>
