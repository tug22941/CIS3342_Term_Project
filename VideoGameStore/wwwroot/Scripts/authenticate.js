
function loadRegister(e) {
    if (e && e != null) {
        e.preventDefault();
    }	
    $('#divLogin').hide();
    $('#divRegister').show();
}

function loadLogin(e) {
    if (e && e != null) {
        e.preventDefault();
    }
    $('#divLogin').show();
    $('#divRegister').hide();
}

function login() {
    var username = $('#inpUsername').val();
    var password = $('#inpPassword').val();
    if (username == '' || password == '') {
        alert('Both username and password are required.');
    }
    else {
        var form = new FormData();
        form.append("username", username);
        form.append("password", password);

        var settings = {
            "url": window.location.origin + "/v1/Login",
            "method": "POST",
            "timeout": 0,
            "processData": false,
            "mimeType": "multipart/form-data",
            "contentType": false,
            "data": form
        };

        $.ajax(settings).done(function (response) {
            if (response) {
                localStorage.setItem("loggedOnUser", response);
                window.location.replace(window.location.href.replace('Login.html', 'Home.html'));
            }
            else {
                alert('Wrong credentials. Please try again.');
            }
        });
    }    
}

function register() {    
    var name = $('#inpNameRegister').val();
    var username = $('#inpUsernameRegister').val();
    var password = $('#inpPasswordRegister').val();
    var email = $('#inpEmailRegister').val();
    var phone = $('#inpPhoneRegister').val();
    var address = $('#inpAddressRegister').val();
    var city = $('#inpCityRegister').val();
    var state = $('#selStateRegister').val();

    if (name == '') {
        alert('name is required.');
    }
    else if (username == '') {
        alert('username is required.');
    }
    else if (password == '') {
        alert('password is required.');
    }
    else if (email == '') {
        alert('email is required.');
    }
    else if (phone == '') {
        alert('phone is required.');
    }
    else if (address == '') {
        alert('address is required.');
    }
    else if (city == '') {
        alert('city is required.');
    }
    else if (state == '') {
        alert('state is required.');
    }
    else {
        var settings = {
            "url": window.location.origin + "/v1/Register",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({ "FirstName": firstName, "LastName": lastName, "Username": username, "Password": password, "Email": email, "Phone": phone, "Address": address, "City": city, "State": state, "Zip": zip }),
        };

 
        $.ajax(settings).done(function (response) {
            if (response) {
                if (response < 0) {
                    alert('A user with that username or email already exists. If you already registered, please try loggin in.');
                }
                else {
                    loadLogin();
                    alert('You are now registered. Please log in.')
                }
            }
            else {
                alert('A user with that username or email already exists. If you already registered, please try loggin in.');
            }
        });
    }
}