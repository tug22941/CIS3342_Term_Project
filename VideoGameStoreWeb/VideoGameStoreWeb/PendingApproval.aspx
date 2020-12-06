<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingApproval.aspx.cs" Inherits="VideoGameStoreWeb.PendingApproval" %>
<%@ Register Src="~/UserControls/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>
<%@ Register Src="~/UserControls/ucSessionInfo.ascx" TagPrefix="uc1" TagName="ucSessionInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Approval</title>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvGames" runat="server" DataKeyNames="ID" GridLines="None" OnRowDeleting="gvGames_RowDeleting"
                            OnRowEditing="gvGames_RowEditing" OnRowCommand="gvGames_RowCommand" OnRowUpdating="gvGames_RowUpdating"
                            OnRowCancelingEdit="gvGames_RowCancelingEdit" AutoGenerateColumns="false">
                            <HeaderStyle Font-Bold="true" Height="30" />
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <div runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                            <div class="col">
                                                <b>Discount by:</b>
                                                <asp:TextBox Width="100" ID="txtDiscount" Text='<%#Eval("CurrentDiscount") %>' runat="server" TextMode="Number" />
                                                <b>%</b>
                                                <asp:RangeValidator CssClass="displayBlock" ID="rval1" ErrorMessage="Please enter value between 0-100." ForeColor="Red" ControlToValidate="txtDiscount" MinimumValue="0" MaximumValue="100" runat="server" Type="Double">
                                                </asp:RangeValidator>
                                            </div>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col" style="max-width: 145px; padding-left: 0">
                                                <asp:Image Height="140" Width="140" ImageUrl='<%#string.Concat("Images/", Eval("ImageUrl")) %>' ID="imgGame" runat="server" />
                                            </div>
                                            <div class="col" style="padding-left: 0">
                                                <h3 id="lblTitle" runat="server" style="display: inline-block"><%#Eval("Title") %></h3>
                                                <span visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? false : true %>' runat="server" class='<%#(bool)Eval("ListedForSale") == true ? "badge badge-pill badge-success" : "badge badge-pill badge-warning" %>'>
                                                    <asp:Label ID="lblListedForSale" runat="server" Text='<%#(bool)Eval("ListedForSale") == true ? "Approved" : "Pending" %>' />
                                                </span>

                                                <div>
                                                    <b>Type: </b>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Type") %>' />
                                                </div>
                                                <div>
                                                    <b>Platform: </b>
                                                    <asp:Label ID="lblPlatform" runat="server" Text='<%#Eval("Platform") %>' />
                                                </div>
                                                <div>
                                                    <b>Released: </b>
                                                    <asp:Label ID="lblReleaseDate" runat="server" Text='<%#Eval("ReleaseDate", "{0:d}") %>' />
                                                </div>
                                                <div runat="server" visible='<%#Eval("RetailPrice").ToString() == Eval("DiscountedPrice").ToString() ? true : false %>'>
                                                    <asp:Label CssClass="price" runat="server" Text='<%#Eval("RetailPrice", "{0:c}") %>' />
                                                </div>
                                                <div runat="server" visible='<%#Eval("RetailPrice").ToString() != Eval("DiscountedPrice").ToString() ? true : false %>'>
                                                    <asp:Label ForeColor="#e2190b" CssClass="price crossedout" runat="server" Text='<%#Eval("RetailPrice", "{0:c}") %>' />
                                                    <asp:Label ForeColor="#28a745" CssClass="price marginLeft7" runat="server" Text='<%#Eval("DiscountedPrice", "{0:c}") %>' />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-sm-12">
                                                <p id="lblDescription" style="height: 125px; overflow: auto; margin-bottom: 5px; font-size: 14px" runat="server"><%#Eval("Description") %></p>
                                                <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                                    <div class="col-sm-12" style="text-align: right">
                                                        <asp:LinkButton CssClass="btn btn-info" ID="lbtnDetailsStoreManager" runat="server" CommandName="Details" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Details" CausesValidation="false">
                                                 <i class="fa fa-info-circle"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="lbtnAddDiscount" runat="server" CommandName="Edit" title="Set Discount">
                                                 <i class="fa fa-dollar"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this game?')" title="Delete" CausesValidation="false">                                                
                                                <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-success" ID="lbtnApprove" runat="server" CommandName="Approve" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnClientClick="return confirm('Are you sure you want to approve this game?')" title="Approve Game" CausesValidation="false">                                                
                                                <i class="fa fa-thumbs-up"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <div style="text-align: right">
                                            <asp:LinkButton CssClass="btn btn-danger" ID="lbtnCancel" runat="server" CommandName="Cancel" Text="Cancel">
                                     <i class="fa fa-times"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-success" ID="lbtnUpdate" runat="server" CommandName="Update" Text="Update">
                                    <i class="fa fa-check"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
               
    </main>   
        <br />
			<footer>
				<p>CIS 3342 Term Project</p>				
			</footer>
              
    </form>
   
</body>
</html>