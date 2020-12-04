<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSessionInfo.ascx.cs" Inherits="VideoGameStoreWeb.UserControls.ucSessionInfo" %>

<span runat="server" id="spanCart" style="margin-right: 20px" visible="false">
    <asp:LinkButton ToolTip="Checkout" ID="lbtnCart" runat="server" PostBackUrl="~/Checkout.aspx">
        <i class="fa fa-cart-arrow-down" style="font-size: 20px"></i>
        <asp:Label Font-Size="15" Font-Bold="true" runat="server" ID="lblCart"></asp:Label>
    </asp:LinkButton>
</span>
<span id="spanLoggedInUserName">
    <asp:Label Font-Bold="true" runat="server" ID="lblUsername"></asp:Label>
</span>
<asp:LinkButton OnClick="lbtnLogout_Click" ID="lbtnLogout" runat="server" Text="Log out" CausesValidation="false" />
