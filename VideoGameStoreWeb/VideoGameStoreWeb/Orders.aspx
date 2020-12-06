<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="VideoGameStoreWeb.Orders" %>
<%@ Register Src="~/UserControls/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>
<%@ Register Src="~/UserControls/ucSessionInfo.ascx" TagPrefix="uc1" TagName="ucSessionInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Orders</title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Styles/main.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <header>
		        <div class="row" style="margin:0">
                    <div class="col-md-4">
                        <h2 class="main">Video Game Store</h2>                
                    </div>
                    <div class="col-md-4" style="padding:30px">
                    </div>
                    <div class="col-md-4" style="text-align:right;padding:30px">                
                        <uc1:ucSessionInfo runat="server" id="ucSessionInfo" />
                    </div>
                </div>	
	        </header> 
        <uc1:ucnav runat="server" id="ucNav" />
        <main>
            
            

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>     

           <div class="row" style="margin:0">
               <div class="col">

                   <asp:Repeater ID="rptrOrders" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <tr>
                                <th scope="col" style="width: 120px">
                                    Game
                                </th>                                
                                <th scope="col" style="width: 80px">
                                    Purchase Price
                                </th>
                                <th scope="col" style="width: 120px">
                                    Quantity
                                </th>
                                <th scope="col" style="width: 100px">
                                    Purchase Date
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblGameTitle" runat="server" Text='<%# Eval("GameTitle") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("PurchasePrice") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblPucrhaseDate" runat="server" Text='<%# Eval("PurchaseDate", "{0:d}") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

               </div>                    
           </div>
                	
        </main>
    </form>
</body>
</html>
