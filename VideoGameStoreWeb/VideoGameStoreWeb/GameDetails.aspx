<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameDetails.aspx.cs" Inherits="VideoGameStoreWeb.GameDetails" %>
<%@ Register Src="~/UserControls/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>
<%@ Register Src="~/UserControls/ucSessionInfo.ascx" TagPrefix="uc1" TagName="ucSessionInfo" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Game Details</title>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <uc1:ucSessionInfo runat="server" id="ucSessionInfo" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
				
	</header> 
         <uc1:ucnav runat="server" id="ucNav" />
    <main>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvGameDetails" runat="server" DataKeyNames="ID" GridLines="None" OnRowDeleting="gvGameDetails_RowDeleting"
                            OnRowEditing="gvGameDetails_RowEditing" OnRowCommand="gvGameDetails_RowCommand" OnRowUpdating="gvGameDetails_RowUpdating"
                            OnRowCancelingEdit="gvGameDetails_RowCancelingEdit" AutoGenerateColumns="false">
                            <HeaderStyle Font-Bold="true" Height="30" />
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <div runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Producer" ? true : false %>'>

                                            <div class="row">
                                                <div class="col">
                                                    <b>Title:</b>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="(Required)"
                                                        ControlToValidate="txtTitle" ValidationGroup="vgUpdate" ForeColor="Red" />
                                                    <asp:TextBox Width="300" ID="txtTitle" Text='<%#Eval("Title") %>' runat="server" />
                                                </div>
                                                <div class="col">
                                                    <b>Released:</b>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="(Required)"
                                                        ControlToValidate="txtReleaseDate" ValidationGroup="vgUpdate" ForeColor="Red" />
                                                    <asp:TextBox Width="155" ID="txtReleaseDate" Text='<%#Eval("ReleaseDate", "{0:d}") %>' runat="server" />
                                                </div>
                                                <div class="col">
                                                    <b>Image:</b>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="(Required)"
                                                        ControlToValidate="txtImageUrl" ValidationGroup="vgUpdate" ForeColor="Red" />
                                                    <asp:TextBox  ID="txtImageUrl" Text='<%#Eval("ImageUrl") %>' runat="server" />
                                                </div>
                                                <div class="col">
                                                    <b class="displayBlock">Category:</b>
                                                    <asp:DropDownList ID="ddlGameType" runat="server" SelectedValue='<%# Eval("Type") %>'>
                                                        <asp:ListItem Value="Action">Action</asp:ListItem>
                                                        <asp:ListItem Value="Adventure">Adventure</asp:ListItem>
                                                        <asp:ListItem Value="First Person">First Person</asp:ListItem>
                                                        <asp:ListItem Value="Sports">Sports</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col">
                                                    <b class="displayBlock">Platform:</b>
                                                    <asp:DropDownList ID="ddlPlatform" runat="server" SelectedValue='<%# Eval("Platform") %>'>
                                                        <asp:ListItem Value="Nintendo Wii">Nintendo Wii</asp:ListItem>
                                                        <asp:ListItem Value="PS4">PS4</asp:ListItem>
                                                        <asp:ListItem Value="Xbox One">Xbox One</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col">
                                                    <b>Description:</b>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="(Required)"
                                                        ControlToValidate="txtDescription" ValidationGroup="vgUpdate" ForeColor="Red" />
                                                    <asp:TextBox Width="100%" Rows="3" ID="txtDescription" runat="server" Text='<%#Eval("Description") %>' TextMode="MultiLine" />
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                            <div class="col">
                                                <b>Discount by:</b>
                                                <asp:TextBox Width="100" ID="txtDiscount" Text='<%#Eval("CurrentDiscount") %>' runat="server" TextMode="Number" />
                                                <b>%</b>
                                                <asp:RangeValidator CssClass="displayBlock" ID="rval1" ErrorMessage="Please enter value between 0-100." ForeColor="Red" ControlToValidate="txtDiscount" MinimumValue="0" MaximumValue="100" runat="server" Type="Double">
                                                </asp:RangeValidator>
                                            </div>
                                        </div>
                                        <div style="text-align: right">
                                            <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-danger" runat="server" CommandName="Cancel" title="Cancel">
                                    <i class="fa fa-times"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnUpdate" CssClass="btn btn-success" runat="server" CommandName="Update" title="Update" ValidationGroup="vgUpdate">
                                    <i class="fa fa-check"></i>
                                            </asp:LinkButton>
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
                                                <p id="lblDescription" runat="server"><%#Eval("Description") %></p>
                                                <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Producer" ? true : false %>'>
                                                    <div class="col" style="text-align: right">
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="lbtnEditByProducer" runat="server" CommandName="Edit" title="Edit">
                                                 <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDeleteProducer" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this game?')" title="Delete" CausesValidation="false">
                                                <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                                    <div class="col" style="text-align: right">
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="lbtnAddDiscount" runat="server" CommandName="Edit" title="Set Discount">
                                                 <i class="fa fa-dollar"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDeleteStoreManager" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this game?')" title="Delete" CausesValidation="false">
                                                <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton Visible='<%# (bool)Eval("ListedForSale") == false ? true : false %>' CssClass="btn btn-success" ID="lbtnApprove" runat="server" CommandName="Approve" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnClientClick="return confirm('Are you sure you want to approve this game?')" title="Approve Game" CausesValidation="false">                                                
                                                <i class="fa fa-thumbs-up"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? true : false %>'>
                                                    <div class="col" style="text-align: right">
                                                        <asp:LinkButton CssClass="btn btn-success" ID="lbtnAddToCart" runat="server" CommandName="AddToCart" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            title="Add to cart" CausesValidation="false">                                                
                                                <i class="fa fa-cart-plus"></i><span class="marginLeft7">Add to cart</span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <h3 style="margin:20px 0;font-weight:bold;font-size:18px">Customer Reviews</h3>
                <div class="accordion" id="divAddReview" runat="server" visible="false">
                    <div class="card">
                        <div class="card-header" id="headingOne">
                            <h2 class="mb-0">
                                <button class="btn btn-link collapsed font-weight-bold" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    + Add Review
                                </button>
                            </h2>
                        </div>

                        <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#divAddReview" style="">
                            <div class="card-body row"> 
                                <div class="col">
                                    <b class="displayBlock">Gameplay</b>
                                    <asp:RadioButtonList ID="rblNewGamePlay" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" Selected="True" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col">
                                    <b class="displayBlock">Graphics</b>
                                    <asp:RadioButtonList ID="rblNewGraphics" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" Selected="True" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col">
                                    <b class="displayBlock">Replay Value</b>
                                    <asp:RadioButtonList ID="rblNewReplayValue" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" Selected="True" />
                                    </asp:RadioButtonList>
                                </div>                               
                                <div class="col">
                                    <b class="displayBlock">Comments</b>
                                    <asp:TextBox ID="txtNewComments" runat="server" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="rfvComments" runat="server" Text="*"
                                        ControlToValidate="txtNewComments" ValidationGroup="vgAdd"
                                        ForeColor="Red" />
                                </div>
                                <div class="col" style="text-align: right">
                                    <asp:Button ID="lbtnAddReview" CssClass="btn btn-primary" runat="server" OnClick="lbtnAddReview_Click" Visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? true : false %>'
                                        Text="Add Review" ValidationGroup="vgAdd" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvReviews" runat="server" DataKeyNames="ID" GridLines="None" OnRowDeleting="gvReviews_RowDeleting"
                    OnRowEditing="gvReviews_RowEditing"
                    OnRowUpdating="gvReviews_RowUpdating" OnRowCancelingEdit="gvReviews_RowCancelingEdit" AutoGenerateColumns="false">
                    <HeaderStyle Font-Bold="true" Height="30" />
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <div class="row">
                                    <div class="col">
                                        <b class="displayBlock">Gameplay</b>
                                        <asp:RadioButtonList ID="rblGamePlay" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField ID="hdnGamePlay" runat="server" Value='<%#Eval("GamePlay") %>' />
                                    </div>
                                    <div class="col">
                                        <asp:RadioButtonList ID="rblGraphics" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField ID="hdnGraphics" runat="server" Value='<%#Eval("Graphics") %>' />
                                    </div>
                                    <div class="col">
                                        <asp:RadioButtonList ID="rblReplayValue" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField ID="hdnReplayValue" runat="server" Value='<%#Eval("ReplayValue") %>' />
                                    </div>
                                    
                                    <div class="col">
                                        <asp:LinkButton ID="lbtnUpdate" CssClass="btn btn-danger" runat="server" CommandName="Cancel" title="Cancel">
                                    <i class="fa fa-times"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-success" runat="server" CommandName="Update" title="Update">
                                    <i class="fa fa-check"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div>
                                        <asp:TextBox ID="txtComments" Text='<%#Eval("Comments") %>' TextMode="MultiLine"
                                            runat="server" />
                                    </div>


                            </EditItemTemplate>
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col">
                                        <b>Gameplay: </b>
                                        <asp:Label ID="lblGamePlay" runat="server" Text='<%#Eval("GamePlay") %>' />
                                    </div>
                                    <div class="col">
                                        <b>Graphics: </b>
                                        <asp:Label ID="lblGraphics" runat="server" Text='<%#Eval("Graphics") %>' />
                                    </div>
                                    <div class="col">
                                        <b>Replay Value: </b>
                                        <asp:Label ID="lblReplayValue" runat="server" Text='<%#Eval("ReplayValue") %>' />
                                    </div>                                                                      
                                </div>
                                <div>
                                    <asp:Label ID="lblComments" Text='<%#Eval("Comments") %>' runat="server" />
                                 </div>  
                                <div style="text-align:right">
                                      <%--  <asp:LinkButton ID="lbtnEdit" CssClass="btn btn-primary" runat="server" CommandName="Edit" Text="Edit" Visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? true : false %>'>
                                            <i class="fa fa-edit"></i>
                                        </asp:LinkButton>--%>
                                        <asp:LinkButton ID="lbtnDelete" CssClass="btn btn-danger" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this review?')"
                                            Text="Delete" Visible='<%#Application["LoggedOnUserId"].ToString() == Eval("Author.Id").ToString() ? true : false %>' CausesValidation="false">
                                            <i class="fa fa-trash-o"></i>
                                        </asp:LinkButton>
                                    </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
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
