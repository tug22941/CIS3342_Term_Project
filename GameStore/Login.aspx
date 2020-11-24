<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GameStore.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="Theme/Login_Theme.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous"/>

    <title>Login</title>
    
</head>
<body>
    <form id="form1" runat="server">
       
        <div class="container mt-4">
            <div class="row">
                <div class="col-6 offset-3 border border-dark border-ra formTopSpace">

                <div>

                <div class="imageBox text-center">
                    <img src="Image/Crash_Bandicoot.png" alt="Profile Picture"/>
                </div>

                <div class="inputBox d-block">
                    <div class="inputSection">
                      <div class="font-weight-bold"><label>Username</label></div>
                      <div><input type="text" placeholder="Enter Username"/></div>
                    </div>

                    <div class="inputSection mt-3 mb-5">
                      <div class="font-weight-bold"><label>Password</label></div>
                      <div><input type="text" placeholder="Enter Password"/></div>
                    </div>

                    <div class="logInButton mb-3">
                      <div class="d-flex"><button type="button">Login</button></div>
                      <div class="d-flex"><input class="align-self-center" type="checkbox"/>Remember Me</div>
                    </div>
                
                    <div class="newUserButton mb-3 mr-1 ">
                      <div><button type="button">Create New Account</button></div>
                    </div>

                </div>

                </div>

            </div>
        </div>
    </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
</body>
</html>
