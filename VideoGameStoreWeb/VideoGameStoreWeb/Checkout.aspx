<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="VideoGameStoreWeb.Checkout" %>
<%@ Register Src="~/UserControls/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>
<%@ Register Src="~/UserControls/ucSessionInfo.ascx" TagPrefix="uc1" TagName="ucSessionInfo" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Game Store - Checkout</title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
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
                <asp:ScriptReference Path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>

        <div id="divOrderCompleted" runat="server" class="alert alert-success" visible="false">
            Your order has been submitted. Please await shipping confirmation. Visit the <a href="Orders.aspx">Orders</a> page for order history.
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvCart" runat="server" GridLines="None" OnRowDeleting="gvCart_RowDeleting" AutoGenerateColumns="false" >
                    <HeaderStyle Font-Bold="true" Height="30" />              
                    <Columns>
                    <asp:TemplateField>                         
                        <ItemTemplate>                             
                            <div class="row">
                                <div class="col" style="max-width: 105px; padding-left: 0">
                                    <asp:Image Height="100" Width="100" ImageUrl='<%#string.Concat("Images/", Eval("Game.ImageUrl")) %>' ID="imgGame" runat="server" />
                                    <br />
                                    <br />
                                </div>
                                <div class="col">                                   
                                    <asp:Label ID="lblCategory" Font-Bold="true" runat="server" Text='<%#Eval("Game.Title") %>' />
                                </div>
                                <div class="col">
                                    <b>Quantity: </b>
                                    <asp:HiddenField runat="server" ID="hdnGameId" Value='<%#Eval("Game.ID") %>'/>
                                    <asp:TextBox ID="txtQuantity" Width="70" AutoPostBack="true" runat="server" min="1" Text='<%#Eval("Quantity") %>' TextMode="Number" OnTextChanged="txtQuantity_TextChanged"/>                                    
                                </div>
                                <div class="col">
                                    <b>Price: </b>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Game.DiscountedPrice", "{0:c}") %>' />
                                </div>  
                                <div class="col">
                                    <b>Subtotal: </b>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ItemTotalCost", "{0:c}")%>' />
                                </div>  
                                <div class="col">
                                    <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                           OnClientClick="return confirm('Are you sure you want to delete this item from your cart?')" title="Delete" CausesValidation="false">                                                
                                           <i class="fa fa-trash-o"></i>
                                     </asp:LinkButton>
                                </div>
                            </div>                            
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <hr />
        <div class="row">
            <div class="col">
                <h3>Ship to:</h3>
                <asp:Label ID="lblShipTo" runat="server" />
            </div>
            <div class="col" style="text-align:right">
                <h3>Order Total:</h3>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label Font-Size="26" ID="lblTotal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col" style="text-align: right">
                <asp:LinkButton OnClick="lbtnAddOrder_Click" CssClass="btn btn-success" ID="lbtnAddOrder" runat="server"  title="Confirm Order" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to complete this order?')">                                                
                     <i class="fa fa-check"></i><span class="marginLeft7">Confirm Order</span>
                </asp:LinkButton>
            </div>
        </div>
        
    </main>
        <br />
			<footer>
				<p>CIS 3342 Term Project</p>					
			</footer>         
    </form>
</body>
</html>
