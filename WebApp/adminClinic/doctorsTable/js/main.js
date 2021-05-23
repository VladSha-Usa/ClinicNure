function getElByClass (className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

// invite form
getElByClass('btn-add').addEventListener('click', showInviteForm);
function showInviteForm () {
	getElByClass('popup-form-wrapper-invite').style.display = 'block';
	getElByClass('overlay-popup-form-invite').style.display = 'block';
}

getElByClass('overlay-popup-form-invite').addEventListener('click', hideInviteForm);
getElByClass('invite-form').addEventListener('submit', hideInviteForm);
function hideInviteForm () {
	getElByClass('popup-form-wrapper-invite').style.display = 'none';
	getElByClass('overlay-popup-form-invite').style.display = 'none';
}


// change form
function showChangeDoctorForm (editEl) {
	let mail = editEl.parentNode.getElementsByClassName('row-mail')[0].innerHTML
	console.log(mail);

	getElByClass('popup-form-wrapper-change-doctor').style.display = 'block';
	getElByClass('overlay-popup-form-change-doctor').style.display = 'block';
}

getElByClass('overlay-popup-form-change-doctor').addEventListener('click', hideChangeDoctorForm);
getElByClass('change-doctor-form').addEventListener('submit', hideChangeDoctorForm);
function hideChangeDoctorForm () {
	getElByClass('popup-form-wrapper-change-doctor').style.display = 'none';
	getElByClass('overlay-popup-form-change-doctor').style.display = 'none';
}