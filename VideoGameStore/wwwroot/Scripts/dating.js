
//declare all profile objects to be used

var profiles = [];
var myProfile = {};
var loggedInMember = JSON.parse(localStorage.getItem("loggedInMember"));
$('#spanLoggedInUserName').html(loggedInMember.name);

const urlAddMember = 'https://localhost:44386/v1/AddMember';
const urlLogin = 'https://localhost:44386/v1/Login';
const urlAddProfile = 'https://localhost:44386/v1/AddProfile';
const urlGetProfilesByGender = 'https://localhost:44386/v1/GetProfilesByGender';
const urlGetProfilesByCityAndState = 'https://localhost:44386/v1/GetProfilesByCityAndState';
const urlGetProfileById = 'https://localhost:44386/v1/GetProfileById';
const urlUpdateProfile = 'https://localhost:44386/v1/UpdateProfile';
const urlDisableProfile = 'https://localhost:44386/v1/DisableProfile';
const urlDeleteProfile = 'https://localhost:44386/v1/DeleteProfile';
const urlEnableProfile = 'https://localhost:44386/v1/EnableProfile';



class Profile {
	//constructor
	constructor(title, name, age, gender, desc, city, state, status, hasKids, wantsKids, religion, relationshipType, imgUrl, enabled) {
    this.title = title;
	this.name = name;
	this.age = age;
	this.gender = gender;
	this.desc = desc;
	this.city = city;
	this.state = state;
	this.status = status;
	this.hasKids = hasKids;
	this.wantsKids = wantsKids;
	this.religion = religion;
	this.relationshipType = relationshipType;
	this.imgUrl = imgUrl;
	this.enabled = enabled;
  }
}

function logout() {
	loggedInMember = null;
	localStorage.removeItem("loggedInMember");	
	window.location.replace(window.location.href.replace('Dating.html', 'Login.html'));
}

function initControls(){
	try{
	//on change, filter profiles list
	document.getElementById('selGenderFilter').addEventListener('change', function() {	
		loadProfiles();
});

	//on change, filter profiles list
	document.getElementById('selStateFilter').addEventListener('change', function() {	
		loadProfiles();
});

}
catch(ex){	
}
}


function loadProfiles(){
	//try {	

		var gender = $('#selGenderFilter').val();		

		//first get by gender
		$.getJSON(urlGetProfilesByGender + '?gender=' + gender , function (data) {
			profiles = data;
			var state = $('#selStateFilter').val();
			//now check if filtering further by state
			if (state && state != '' && state != 'A') {
				$.getJSON(urlGetProfilesByCityAndState + '?city=A&state=' + state, function (data) {
					//find intersection of profiles
					var newProfiles = [];
					for (var i = 0; i < data.length; i++) {
						if (JSON.stringify(profiles).includes(JSON.stringify(data[i]))) {
							newProfiles.push(data[i]);
                        }
                    }
					profiles = newProfiles;
					populateProfiles();
				});
			}
			else {
				populateProfiles();
            }

				
		});


	//declare all profile objects to be used and store in array
	//localProfiles = JSON.parse(localStorage.getItem("profiles"));
	
	//if(!profiles || profiles === null){
	//	initApp()
	//}	
		
	//}
	//catch(ex){
	//	console.log(ex);
	//}
}

function populateProfiles() {

	try {
		var passed = JSON.parse(localStorage.getItem("passed"));
		$('#divProfiles').html('');

		for (var i = 0; i < profiles.length; i++) {
			profile = profiles[i];
			//exclude passed on and disabled profiles
			if (!JSON.stringify(passed).includes(JSON.stringify(profiles[i])) && profiles[i].enabled) {
				//outer card elem to hold the visual profile representation
				card = document.createElement('div');
				card.className = 'card profileCard';
				card.id = 'profile-' + i;

				//profile image
				img = document.createElement('img');
				img.src = 'images/' + profile.imgUrl;

				//container 1 for profile attributes
				container1 = document.createElement('div');
				container1.className = 'container';

				//container 2 for like/pass buttons
				container2 = document.createElement('div');
				container2.className = 'container';

				//like button
				btnLike = document.createElement('button');
				btnLike.className = 'button';
				btnLike.textContent = 'Like';
				btnLike.onclick = (function (i) {
					return function () {
						//will store liked profiles
						var likes = JSON.parse(localStorage.getItem("likes"));
						if (!likes || likes === null || likes.length === 0) {
							likes = [];
						}
						if (!JSON.stringify(likes).includes(JSON.stringify(profiles[i]))) {
							//add clicked item to likes array
							likes.push(profiles[i]);
							localStorage.setItem("likes", JSON.stringify(likes));
						}
						else {
							alert('You already liked this profile.');
						}

					};
				})(i);

				//pass button
				btnPass = document.createElement('button');
				btnPass.className = 'button button-red';
				btnPass.textContent = 'Pass';
				btnPass.onclick = (function (i) {
					return function () {
						//delete profile from main profiles array
						var passed = JSON.parse(localStorage.getItem("passed"));
						if (!passed || passed === null || passed.length === 0) {
							passed = [];
						}
						if (!JSON.stringify(passed).includes(JSON.stringify(profiles[i]))) {
							//add clicked item to passed array
							passed.push(profiles[i]);
							localStorage.setItem("passed", JSON.stringify(passed));
						}
						loadProfiles();

					};
				})(i);

				//create dom elements to render all other profile attributes
				pName = document.createElement('h4');
				pName.textContent = profile.name;

				elem1 = document.createElement('p');
				elem1.innerHTML = '<b>Age: </b>' + profile.age + '<b style="margin-left:15px">Gender: </b>' + profile.gender;

				elem2 = document.createElement('p');
				elem2.innerHTML = '<b>Location: </b>' + profile.city + ', ' + profile.state;

				desc = document.createElement('p');
				desc.textContent = profile.desc;

				elem3 = document.createElement('p');
				elem3.innerHTML = '<b>Status: </b>' + profile.status;

				elem4 = document.createElement('p');
				elem4.innerHTML = '<b>Has kids: </b>' + profile.hasKids + '<b style="margin-left:15px">Wants kids: </b>' + profile.wantsKids;

				elem5 = document.createElement('p');
				elem5.innerHTML = '<b>Religion: </b>' + profile.religion;

				elem6 = document.createElement('p');
				elem6.innerHTML = '<b>Relationship Type: </b>' + profile.relationshipType;

				//build dom card elem with created elements/nodes
				card.appendChild(img);
				card.appendChild(container1);
				card.appendChild(container2);
				container1.appendChild(pName);
				container1.appendChild(desc);
				container1.appendChild(elem1);
				container1.appendChild(elem2);
				container1.appendChild(elem3);
				container1.appendChild(elem4);
				container1.appendChild(elem5);
				container1.appendChild(elem6);
				container2.appendChild(btnLike);
				container2.appendChild(btnPass);

				//finally attach card to main div (center)
				$('#divProfiles').append(card);
			}

		}	
	}
	catch (ex) {

    }
}

function loadLikes(e) {	
	try {
		if (e && e != null) {
			e.preventDefault();
		}	
		$('nav a').removeClass('active');
		$('#aLiked').addClass('active');
	$('.view').hide();	
	$('#divLiked').show().html('');
	var likes = JSON.parse(localStorage.getItem("likes"));
	
	if(!likes || likes === null || likes.length === 0){		
		return false;
	}		
	
		//loop thru all profiles array
		for(var i = 0; i < likes.length; i++){
			profile = likes[i];	
			
			//outer card elem to hold the visual profile representation
			card = document.createElement('div');
			card.className = 'card profileCard';
			card.id = 'profile-' + i;
			
			//profile image
			img = document.createElement('img');
			img.src = 'images/' + profile.imgUrl;
			
			//container 1 for profile attributes
			container1 = document.createElement('div');
			container1.className = 'container';
			
			//container 2 for like/pass buttons
			container2 = document.createElement('div');
			container2.className = 'container';
			
			
			//pass button
			btnDelete = document.createElement('button');
			btnDelete.className = 'button button-red';
			btnDelete.textContent = 'Remove';
			btnDelete.onclick = (function(i){
				return function(){	
						//delete liked profile from main likes array
					var newLikes = [];
						var likes = JSON.parse(localStorage.getItem("likes"));
						delete likes[i];
						for(var j=0; j<likes.length; j++){
							if(likes[j]){
								newLikes.push(likes[j]);
							}
						}
					localStorage.setItem("likes", JSON.stringify(newLikes));
					loadLikes();
			};})(i);
			
			//create dom elements to render all other profile attributes
			pName = document.createElement('h4');
			pName.textContent = profile.name;
			
			elem1 = document.createElement('p');		
			elem1.innerHTML = '<b>Age: </b>' + profile.age + '<b style="margin-left:15px">Gender: </b>' + profile.gender;
							
			elem2 = document.createElement('p');		
			elem2.innerHTML = '<b>Location: </b>' + profile.city + ', ' + profile.state;
			
			desc = document.createElement('p');
			desc.textContent = profile.desc;		
					
			elem3 = document.createElement('p');
			elem3.innerHTML = '<b>Status: </b>' + profile.status;
			
			elem4 = document.createElement('p');		
			elem4.innerHTML = '<b>Has kids: </b>' + profile.hasKids + '<b style="margin-left:15px">Wants kids: </b>' + profile.wantsKids;
					
			elem5 = document.createElement('p');
			elem5.innerHTML = '<b>Religion: </b>' + profile.religion;
			
			elem6 = document.createElement('p');
			elem6.innerHTML = '<b>Relationship Type: </b>' + profile.relationshipType;
			
			//build dom card elem with created elements/nodes
			card.appendChild(img);
			card.appendChild(container1);
			card.appendChild(container2);
			container1.appendChild(pName);
			container1.appendChild(desc);		
			container1.appendChild(elem1);		
			container1.appendChild(elem2);		
			container1.appendChild(elem3);
			container1.appendChild(elem4);		
			container1.appendChild(elem5);
			container1.appendChild(elem6);			
			container2.appendChild(btnDelete);
			
			//finally attach card to main div (center)
			$('#divLiked').append(card);
		}
	}
	catch(ex){
		console.log(ex);
	}
}

function loadPassed(e){
	try {
	if (e && e != null) {
		e.preventDefault();
		}
		$('nav a').removeClass('active');
		$('#aPassed').addClass('active');
	$('.view').hide();
	$('#divPassed').html('').show();
	var passed = JSON.parse(localStorage.getItem("passed"));
	
	if(!passed || passed === null || passed.length === 0){		
		return false;
	}		
	
		//loop thru all profiles array
		for(var i = 0; i < passed.length; i++){
			profile = passed[i];	
			
			//outer card elem to hold the visual profile representation
			card = document.createElement('div');
			card.className = 'card profileCard';
			card.id = 'profile-' + i;
			
			//profile image
			img = document.createElement('img');
			img.src = 'images/' + profile.imgUrl;
			
			//container 1 for profile attributes
			container1 = document.createElement('div');
			container1.className = 'container';
			
			//container 2 for like/pass buttons
			container2 = document.createElement('div');
			container2.className = 'container';
			
			
			//pass button
			btnLike = document.createElement('button');
			btnLike.className = 'button button-green';
			btnLike.textContent = 'Like';
			
			btnLike.onclick = (function(i){
				return function(){	
						//delete profile from passed array
						
						var likes = JSON.parse(localStorage.getItem("likes"));
						if(!likes || likes === null || likes.length === 0){
							likes = [];
						}
						if(!JSON.stringify(likes).includes(JSON.stringify(passed[i]))){
							//put back to likes
							likes.push(passed[i]);
							//then remove from passed list
							delete passed[i];
							var newPassed = [];
							for(var j=0; j<passed.length; j++){
								if(passed[j]){
									newPassed.push(passed[j]);
								}
							}
							localStorage.setItem("passed", JSON.stringify(newPassed));
							localStorage.setItem("likes", JSON.stringify(likes));
						}
						//loadPassed();			
			};})(i);
			
			//pass button
			btnDelete = document.createElement('button');
			btnDelete.className = 'button button-red';
			btnDelete.textContent = 'Remove';
			
			btnDelete.onclick = (function(i){
				return function(){	
						//delete profile from passed array
						
					var passed = JSON.parse(localStorage.getItem("passed"));
					if (!passed || passed === null || passed.length === 0) {
						passed = [];
					}
					if (!JSON.stringify(profiles).includes(JSON.stringify(passed[i]))) {
						//put back to likes
						profiles.push(passed[i]);
						//then remove from passed list						
					}
					delete passed[i];
					var newPassed = [];
					for (var j = 0; j < passed.length; j++) {
						if (passed[j]) {
							newPassed.push(passed[j]);
						}
					}
					localStorage.setItem("passed", JSON.stringify(newPassed));
					localStorage.setItem("profiles", JSON.stringify(profiles));
					loadPassed();
			};})(i);
			
			//create dom elements to render all other profile attributes
			pName = document.createElement('h4');
			pName.textContent = profile.name;
			
			elem1 = document.createElement('p');		
			elem1.innerHTML = '<b>Age: </b>' + profile.age + '<b style="margin-left:15px">Gender: </b>' + profile.gender;
							
			elem2 = document.createElement('p');		
			elem2.innerHTML = '<b>Location: </b>' + profile.city + ', ' + profile.state;
			
			desc = document.createElement('p');
			desc.textContent = profile.desc;		
					
			elem3 = document.createElement('p');
			elem3.innerHTML = '<b>Status: </b>' + profile.status;
			
			elem4 = document.createElement('p');		
			elem4.innerHTML = '<b>Has kids: </b>' + profile.hasKids + '<b style="margin-left:15px">Wants kids: </b>' + profile.wantsKids;
					
			elem5 = document.createElement('p');
			elem5.innerHTML = '<b>Religion: </b>' + profile.religion;
			
			elem6 = document.createElement('p');
			elem6.innerHTML = '<b>Relationship Type: </b>' + profile.relationshipType;
			
			//build dom card elem with created elements/nodes
			card.appendChild(img);
			card.appendChild(container1);
			card.appendChild(container2);
			container1.appendChild(pName);
			container1.appendChild(desc);		
			container1.appendChild(elem1);		
			container1.appendChild(elem2);		
			container1.appendChild(elem3);
			container1.appendChild(elem4);		
			container1.appendChild(elem5);
			container1.appendChild(elem6);
			container2.appendChild(btnLike);			
			container2.appendChild(btnDelete);
			
			//finally attach card to main div (center)
			$('#divPassed').append(card);
		}
	}
	catch(ex){
		console.log(ex);
	}
}

function loadAddProfile(e) {
	if (e && e != null) {
		e.preventDefault();
	}
	$('nav a').removeClass('active');
	$('#aAddProfile').addClass('active');

	$('.view').hide();
	$('#divAddProfile').show();
}


function loadMyProfile(e){ 
	try {		
		if (e && e != null) {
			e.preventDefault();
		}
		$('nav a').removeClass('active');
		$('#aMyProfile').addClass('active');

		$('.view').hide();
		$('#divMyProfile').show();

		if(!profiles || profiles === null || profiles.length === 0){
			alert('No profiles were found.');
			return false;
		}

		//populate
		 populateMyProfile(loggedInMember.profileId);	
		
	}
	catch(ex){		
	}	
}


function populateMyProfile(profileId) {
	
	$.getJSON(urlGetProfileById + '?profileId=' + profileId, function (data) {
		if (data && data != null) {
			myProfile = data;	
			document.getElementById('inpTitle').value = myProfile.title;
			document.getElementById('inpFullName').value = myProfile.name;
			document.getElementById('inpAge').value = myProfile.age;
			document.getElementById('selGender').value = myProfile.gender;
			document.getElementById('txaDescription').value = myProfile.description;
			document.getElementById('inpCity').value = myProfile.city;
			document.getElementById('selState').value = myProfile.state;

			//status
			if (myProfile.status === 'Single') {
				document.getElementById('single').checked = true;
			}
			else if (myProfile.status === 'Taken') {
				document.getElementById('taken').checked = true;
			}

			//has kids
			if (myProfile.hasKids === 'Yes') {
				document.getElementById('yesKids').checked = true;
			}
			else if (myProfile.hasKids === 'No') {
				document.getElementById('noKids').checked = true;
			}

			//wants kids
			if (myProfile.wantsKids === 'Yes') {
				document.getElementById('yesWantKids').checked = true;
			}
			else if (myProfile.wantsKids === 'No') {
				document.getElementById('noWantKids').checked = true;
			}

			document.getElementById('inpTitle').value = myProfile.title;
			document.getElementById('inpTitle').value = myProfile.title;
			document.getElementById('inpTitle').value = myProfile.title;

			document.getElementById('selReligion').value = myProfile.religion;
			document.getElementById('selRelationshipType').value = myProfile.relationshipType;
			document.getElementById('inpImgUrl').value = myProfile.imgUrl;

			var btnDisableProfile = document.getElementById('btnDisableProfile');
			btnDisableProfile.onclick = (function (profileId) {
				return function () {
					disableProfile(myProfile);
				};
			})(myProfile);

			var btnDeleteProfile = document.getElementById('btnDeleteProfile');
			btnDeleteProfile.onclick = (function (profileId) {
				return function () {
					var r = confirm("Your proile will be deleted together with your member account. This action cannot be reversed. Do you want to proceed?");
					if (r == true) {
						deleteProfile(myProfile);
					} 
				};
			})(myProfile);

			var btnEnableProfile = document.getElementById('btnEnableProfile');
			btnEnableProfile.onclick = (function (profileId) {
				return function () {
					enableProfile(myProfile);
				};
			})(myProfile);

			if (myProfile.enabled) {
				$('#btnDisableProfile').show();
				$('#btnEnableProfile').hide();
			}
			else {
				$('#btnEnableProfile').show();
				$('#btnDisableProfile').hide();
			}
		}
		else {
			alert('unable to load your profile');
		}
	});
	return myProfile;
}


function deleteProfile(myProfile) {
	var settings = {
		"url": urlDeleteProfile,
		"method": "DELETE",
		"timeout": 0,
		"headers": {
			"Content-Type": "application/json"
		},
		"data": JSON.stringify(myProfile.id),
	};

	$.ajax(settings).done(function (response) {		
		logout();
		alert('profile deleted');
	});
}

function disableProfile(myProfile) {
	var settings = {
		"url": urlDisableProfile,
		"method": "PUT",
		"timeout": 0,
		"headers": {
			"Content-Type": "application/json"
		},
		"data": JSON.stringify(myProfile.id),
	};

	$.ajax(settings).done(function (response) {
		myProfile.enabled = false;
		loadMyProfile();
		alert('profile disabled');
	});	
}

function enableProfile(myProfile) {
	var settings = {
		"url": urlEnableProfile,
		"method": "PUT",
		"timeout": 0,
		"headers": {
			"Content-Type": "application/json"
		},
		"data": JSON.stringify(myProfile.id),
	};

	$.ajax(settings).done(function (response) {
		myProfile.enabled = true;
		loadMyProfile();
		alert('profile enabled');
	});
}

//render likes list on the side from liked array
function renderLiked(profile)
{
	likeCard = document.createElement('div');
	likeCard.className = 'card';					
	likeImg = document.createElement('img');
	likeImg.src = 'images/' + profile.imgUrl;
					
	table = document.createElement('table');
	tr = document.createElement('tr');
	td1 = document.createElement('td');
	td2 = document.createElement('td');
	table.appendChild(tr);
	tr.appendChild(td1);
	tr.appendChild(td2);
					
	n = document.createElement('h4');
	n.textContent = profile.name;
					
	a = document.createElement('p');		
	a.innerHTML = '<b>Age: </b>' + profile.age;
										
	d = document.createElement('p');
	d.textContent = profile.desc;
					
	likeCard.appendChild(table);					
	td1.appendChild(likeImg);
	td2.appendChild(n);
	td2.appendChild(a);
	td2.appendChild(d);
		
	document.getElementById('likesDiv').appendChild(likeCard);
}

//add profile
function addProfile() {
	var newProfile = {};
	var title = document.getElementById('inpTitleAdd').value;
	if(!title || title === ''){
		alert('Please enter a title.');
		return false;
	}
	else{
		newProfile.title = title;
	}
	var name = document.getElementById('inpFullNameAdd').value;
	if(!name || name === ''){
		alert('Please enter a name.');
		return false;
	}
	else{
		newProfile.name = name;
	}
	var age = document.getElementById('inpAgeAdd').value;
	if(!age || age === ''){
		alert('Please enter an age.');
		return false;
	}
	else{
		newProfile.age = age;
	}
	var gender = document.getElementById('selGenderAdd').value;
	if(!gender || gender === ''){
		alert('Please select a gender.');
		return false;
	}
	else{
		newProfile.gender = gender;
	}
	var description = document.getElementById('txaDescriptionAdd').value;
	if (!description || description === ''){
		alert('Please enter a profile description.');
		return false;
	}
	else{
		newProfile.description = description;
	}
	var city = document.getElementById('inpCityAdd').value;	
	if(!city || city === ''){
		alert('Please enter a city.');
		return false;
	}
	else{
		newProfile.city = city;
	}
	var state = document.getElementById('selStateAdd').value;
	if(!state || state === ''){
		alert('Please select a state.');
		return false;
	}
	else{
		newProfile.state = state;
	}
	var status = 'single';
	if(!status || status === ''){
		alert('Please select a status.');
		return false;
	}
	else{
		newProfile.status = status;
	}
	var hasKids = false;
	//if(!hasKids || hasKids === ''){
	//	alert('Please select if person has kids.');
	//	return false;
	//}
	//else{
		newProfile.hasKids = hasKids;
	//}
	var wantsKids = true
	//if(!wantsKids || wantsKids === ''){
	//	alert('Please select if person wants kids.');
	//	return false;
	//}
	//else{
		newProfile.wantsKids = wantsKids;
	//}
	var religion = document.getElementById('selReligionAdd').value;
	if(!religion || religion === ''){
		alert('Please select a religion.');
		return false;
	}
	else{
		newProfile.religion = religion;
	}
	var relationshipType = document.getElementById('selRelationshipTypeAdd').value;
	if (!relationshipType || relationshipType === ''){
		alert('Please select a relationship type.');
		return false;
	}
	else{
		newProfile.relationshipType = relationshipType;
	}
	var imgUrl = document.getElementById('inpImgUrlAdd').value;
	if(!imgUrl || imgUrl === ''){
		alert('Please enter an image name (including extension).');
		return false;
	}
	else{
		newProfile.imgUrl = imgUrl;
	}

	newProfile.enabled = true;

	$.ajax({
		type: "POST",		
		url: urlAddProfile,
		contentType: 'application/json',
		dataType: 'json',		
		data: JSON.stringify(newProfile),
		success: function () {
			alert("Profile added");
		}
	});

}


//save profile
function saveProfile(){

	var title = document.getElementById('inpTitle').value;
	if(!title || title === ''){
		alert('Please enter a title.');
		return false;
	}
	else{
		myProfile.title = title;
	}
	var name = document.getElementById('inpFullName').value;
	if(!name || name === ''){
		alert('Please enter a name.');
		return false;
	}
	else{
		myProfile.name = name;
	}
	var age = document.getElementById('inpAge').value;
	if(!age || age === ''){
		alert('Please enter an age.');
		return false;
	}
	else{
		myProfile.age = age;
	}
	var gender = document.getElementById('selGender').value;
	if(!gender || gender === ''){
		alert('Please select a gender.');
		return false;
	}
	else{
		myProfile.gender = gender;
	}
	var description = document.getElementById('txaDescription').value;
	if (!description || description === ''){
		alert('Please enter a profile description.');
		return false;
	}
	else{
		myProfile.description = description;
	}
	var city = document.getElementById('inpCity').value;	
	if(!city || city === ''){
		alert('Please enter a city.');
		return false;
	}
	else{
		myProfile.city = city;
	}
	var state = document.getElementById('selState').value;
	if(!state || state === ''){
		alert('Please select a state.');
		return false;
	}
	else{
		myProfile.state = state;
	}
	var status = 'single';
	if(!status || status === ''){
		alert('Please select a status.');
		return false;
	}
	else{
		myProfile.status = status;
	}
	var hasKids = true;
	//if(!hasKids || hasKids === ''){
	//	alert('Please select if person has kids.');
	//	return false;
	//}
	//else{
		myProfile.hasKids = hasKids;
	//}
	var wantsKids = false;
	//if(!wantsKids || wantsKids === ''){
	//	alert('Please select if person wants kids.');
	//	return false;
	//}
	//else{
		myProfile.wantsKids = wantsKids;
	//}
	var religion = document.getElementById('selReligion').value;
	if(!religion || religion === ''){
		alert('Please select a religion.');
		return false;
	}
	else{
		myProfile.religion = religion;
	}
	var relationshipType = document.getElementById('selRelationshipType').value;
	if (!relationshipType || relationshipType === ''){
		alert('Please select a relationship type.');
		return false;
	}
	else{
		myProfile.relationshipType = relationshipType;
	}
	var imgUrl = document.getElementById('inpImgUrl').value;
	if(!imgUrl || imgUrl === ''){
		alert('Please enter an image name (including extension).');
		return false;
	}
	else{
		myProfile.imgUrl = imgUrl;
	}

	$.ajax({
		type: "PUT",
		url: urlUpdateProfile,
		contentType: 'application/json',
		dataType: 'json',
		data: JSON.stringify(myProfile),
		success: function () {
			alert("Profile updated");
		},
		error: function () {
			alert("error");
		}
	});


}


