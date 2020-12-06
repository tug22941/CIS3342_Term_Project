<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNav.ascx.cs" Inherits="VideoGameStoreWeb.UserControls.ucNav" %>

<nav runat="server" id="nav">
    <ul>
        <li runat="server" id="liHome">            
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Home.aspx">Home</asp:HyperLink>
        </li>
        <li runat="server" id="liPendingApproval" visible="false">            
            <asp:HyperLink ID="lnkPendingApproval" runat="server" NavigateUrl="~/PendingApproval.aspx">Pending Approval</asp:HyperLink>
        </li>    
        <li runat="server" id="liOrders" visible="false">            
            <asp:HyperLink ID="lnkOrders" runat="server" NavigateUrl="~/Orders.aspx">Orders</asp:HyperLink>
        </li>

    </ul>
</nav>
