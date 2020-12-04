<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VideoGameStoreWeb.Home" %>
<%@ Register Src="~/UserControls/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>
<%@ Register Src="~/UserControls/ucSessionInfo.ascx" TagPrefix="uc1" TagName="ucSessionInfo" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
        
        <div runat="server" class="card" id="divFilters">
            <div class="row">
                <div class="col" style="padding:5px 25px">
                    <b>Genre: </b>
                    <asp:DropDownList onchange="filter(this.value, $('#ddlPlatformFilter').val())" ID="ddlGenreFilter" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="All">All</asp:ListItem>
                    </asp:DropDownList>
         
                    <b style="margin-left:10px">Platform: </b>
                    <asp:DropDownList onchange="filter($('#ddlGenreFilter').val(), this.value)" ID="ddlPlatformFilter" runat="server" AppendDataBoundItems="true" DataValueField="Platform" DataTextField="Platform">
                        <asp:ListItem Value="All">All</asp:ListItem>
                    </asp:DropDownList>
                </div>                               
            </div>
        </div>

             <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference path="~/Scripts/main.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="accordion" id="divAddGame" runat="server" visible="false">
            <div class="card">
                <div class="card-header" id="headingOne">
                    <h2 class="mb-0">
                        <button id="btnCollapse" runat="server" class="btn btn-link collapsed font-weight-bold" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                            + Add Game
                        </button>
                    </h2>
                </div>

                <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#divAddGame" style="">
                <div class="card-body row" style="padding-bottom:0">
                <div class="col">
                    <b>Title</b>
                    <asp:RequiredFieldValidator ID="rfvGameTitle" runat="server" Text="(Required)"
                        ControlToValidate="txtNewTitle" ValidationGroup="vgAdd"
                        ForeColor="Red" />
                    <asp:TextBox ID="txtNewTitle" runat="server" />
                    
                </div>
                <div class="col">
                    <b>Image Url</b>
                    <asp:RequiredFieldValidator ID="rfvImageUrl" runat="server" Text="(Required)"
                        ControlToValidate="txtNewImageUrl" ValidationGroup="vgAdd"
                        ForeColor="Red" />
                    <asp:TextBox ID="txtNewImageUrl" runat="server" />
                    
                </div>                
                <div class="col">
                    <b>Type/Genre</b>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ForeColor="Red"
                        ControlToValidate="ddlNewType" Text="(Required)" ValidationGroup="vgAdd" />
                    <asp:DropDownList ID="ddlNewType" runat="server"  >
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="Action">Action</asp:ListItem>
                        <asp:ListItem Value="Adventure">Adventure</asp:ListItem>
                        <asp:ListItem Value="First Person">First Person</asp:ListItem>
                        <asp:ListItem Value="Sports">Sports</asp:ListItem>
                    </asp:DropDownList>
                    
                </div>
                <div class="col">
                    <b >Platform</b>                    
                        <asp:RequiredFieldValidator ID="rfvPlatform" runat="server" ForeColor="Red"
                        ControlToValidate="ddlNewPlatform" Text="(Required)" ValidationGroup="vgAdd" />                    
                    <asp:DropDownList ID="ddlNewPlatform" runat="server"  >
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="Nintendo Wii">Nintendo Wii</asp:ListItem>
                        <asp:ListItem Value="PS4">PS4</asp:ListItem>
                        <asp:ListItem Value="Xbox One">Xbox One</asp:ListItem>
                    </asp:DropDownList>
                    
                </div>
                <div class="col">
                    <b>Retail Price</b>
                    <asp:RequiredFieldValidator ID="rfvRetailPrice" runat="server" Text="(Required)"
                        ControlToValidate="txtNewRetailPrice" ValidationGroup="vgAdd"
                        ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revPrice" ValidationExpression="^\d{0,8}(\.\d{1,4})?$" runat="server" ErrorMessage="Please enter a valid price"
                        ControlToValidate="txtNewRetailPrice" ValidationGroup="vgAdd" Display="Dynamic" ForeColor="Red" SetFocusOnError="True">
                    </asp:RegularExpressionValidator>                   

                    <asp:TextBox ID="txtNewRetailPrice" runat="server"  />
                    
                </div>
                <div class="col">
                    <b>Release Date</b>
                    <asp:RequiredFieldValidator ID="rfvReleasedDate" runat="server" Text="(Required)"
                        ControlToValidate="txtNewReleaseDate" ValidationGroup="vgAdd"
                        ForeColor="Red" />
                    <asp:TextBox ID="txtNewReleaseDate" runat="server" TextMode="Date" />
                    
                </div>
                <div class="col" style="text-align:right">
                    <asp:Button OnClick="btnAddGame_Click" ID="btnAddGame" CssClass="btn btn-primary" runat="server" Text="Add" ValidationGroup="vgAdd" />
                </div>
            </div> 
             <div style="padding: 0 1.25rem">
                    <b>Description</b>
                  <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Text="(Required)"
                        ControlToValidate="txtNewDescription" ForeColor="Red" ValidationGroup="vgAdd" />
                    <asp:TextBox ID="txtNewDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="2" />
                   
             </div>
                </div>
            </div>
        </div>

                          
        <div class="row">
            <div class="col-md-12" style="padding:0 5px">
                <asp:GridView ID="gvGames" runat="server" DataKeyNames="ID" GridLines="None" OnRowDeleting="gvGames_RowDeleting"
                    OnRowEditing="gvGames_RowEditing" OnRowCommand="gvGames_RowCommand" OnRowUpdating="gvGames_RowUpdating"
                    OnRowCancelingEdit="gvGames_RowCancelingEdit" AutoGenerateColumns="false" >
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
                                </div>
                            <div class="row">                                
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
                                    <asp:TextBox Width="155" ID="txtImageUrl" Text='<%#Eval("ImageUrl") %>' runat="server" />
                                </div>
                            </div>
                            <div class="row">                                 
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
                                    <asp:TextBox Width="350" Rows="2" ID="txtDescription" runat="server" Text='<%#Eval("Description") %>' TextMode="MultiLine" />
                                </div>                                 
                            </div>
                            </div>
                            <div runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                <div class="col">
                                    <b>Discount by:</b>
                                    <asp:TextBox Width="100" ID="txtDiscount" Text='<%#Eval("CurrentDiscount") %>' runat="server" TextMode="Number" />                                    
                                    <b>%</b>
                                    <asp:rangevalidator CssClass="displayBlock" ID="rval1" errormessage="Please enter value between 0-100." forecolor="Red" ControlToValidate="txtDiscount" MinimumValue="0" MaximumValue="100" runat="server" Type="Double">
                                    </asp:rangevalidator>
                                </div>    
                            </div>
                         <div style="text-align:right">
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
                                <div class="col" style="max-width:145px;padding-left:0">
                                    <asp:Image Height="140" Width="140" ImageUrl='<%#string.Concat("Images/", Eval("ImageUrl")) %>' ID="imgGame" runat="server" />
                                </div>
                                <div class="col" style="padding-left:0">
                                     <h3 id="lblTitle" class="ellipsis" runat="server" title='<%#Eval("Title")%>' style="display:inline-block;max-width:220px"><%#Eval("Title") %></h3> 
                                     <span visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? false : true %>' runat="server" class='<%#(bool)Eval("ListedForSale") == true ? "badge badge-pill badge-success" : "badge badge-pill badge-warning" %>'>
                                         <asp:Label ID="lblListedForSale" runat="server" Text='<%#(bool)Eval("ListedForSale") == true ? "Approved" : "Pending" %>' />
                                     </span>
                                    
                                     <div>
                                         <b>Type: </b><asp:Label CssClass="gameCategory" ID="lblCategory" runat="server" Text='<%#Eval("Type") %>' />
                                     </div>
                                     <div>
                                         <b>Platform: </b><asp:Label CssClass="gamePlatform" ID="lblPlatform" runat="server" Text='<%#Eval("Platform") %>' />
                                     </div>
                                     <div>
                                         <b>Released: </b><asp:Label ID="lblReleaseDate" runat="server" Text='<%#Eval("ReleaseDate", "{0:d}") %>' />
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
                            <div class="row" style="margin-top:5px">
                                <div class="col-sm-12">
                                     <p id="lblDescription" style="height:85px;overflow:auto;margin-bottom:5px;font-size:14px" runat="server"><%#Eval("Description") %></p>
                                    <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Producer" ? true : false %>'>
                                        <div class="col" style="text-align:right">
                                            <asp:LinkButton CssClass="btn btn-info" ID="lbtnDetailsProducer" runat="server" CommandName="Details" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Details" CausesValidation="false">
                                                 <i class="fa fa-info-circle"></i>
                                             </asp:LinkButton> 
                                            <asp:LinkButton CssClass="btn btn-primary"  ID="lbtnEditByProducer" runat="server" CommandName="Edit" title="Edit">
                                                 <i class="fa fa-edit"></i>
                                             </asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDeleteProducer" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                OnClientClick="return confirm('Are you sure you want to delete this game?')" title="Delete" CausesValidation="false">
                                                <i class="fa fa-trash-o"></i>
                                            </asp:LinkButton>                                             
                                        </div>
                                    </div>
                                    <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Store Manager" ? true : false %>'>
                                        <div class="col" style="text-align:right">
                                            <asp:LinkButton CssClass="btn btn-info" ID="lbtnDetailsStoreManager" runat="server" CommandName="Details" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Details" CausesValidation="false">
                                                 <i class="fa fa-info-circle"></i>
                                             </asp:LinkButton>
                                             <asp:LinkButton CssClass="btn btn-primary"  ID="lbtnAddDiscount" runat="server" CommandName="Edit" title="Set Discount">
                                                 <i class="fa fa-dollar"></i>
                                             </asp:LinkButton>                                       
                                            <asp:LinkButton CssClass="btn btn-danger" ID="lbtnDeleteStoreManager" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                OnClientClick="return confirm('Are you sure you want to delete this game?')" title="Delete" CausesValidation="false">
                                                <i class="fa fa-trash-o"></i>
                                            </asp:LinkButton>                                        
                                        </div>
                                    </div>
                                    <div class="row" runat="server" visible='<%#Application["LoggedOnUserType"].ToString() == "Customer" ? true : false %>'>
                                        <div class="col" style="text-align:right">
                                        <asp:LinkButton CssClass="btn btn-info" ID="lbtnDetailsCustomer" runat="server" CommandName="Details" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Details" CausesValidation="false">
                                                 <i class="fa fa-info-circle"></i>
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
                       
    </main>    
        <br />
			<!-- Footer section -->
			<footer>
				<p>CIS 3342 Term Project</p>				
			</footer>
          
    </form>
   
</body>
</html>

