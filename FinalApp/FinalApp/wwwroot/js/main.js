function getElByClass (className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

getElByClass('button-enter').addEventListener('click', buttonEnterClick);
function buttonEnterClick () {
	getElByClass('popup-form-wrapper').style.display = 'block';
	getElByClass('overlay-popup-form').style.display = 'block';
}

getElByClass('overlay-popup-form').addEventListener('click', overlayPopupFormClick);
function overlayPopupFormClick () {
	getElByClass('popup-form-wrapper').style.display = 'none';
	getElByClass('overlay-popup-form').style.display = 'none';
}

$(".btn-open-info").click(function () {
	$(this).parent().parent().toggleClass("active");
	$(this).parent().parent().next().slideToggle();
});

$(".btn-more").click(function () {
	$(".popup-options-wrapper").show();
	$(".overlay-popup-options").show();
});

$(".overlay-popup-options").click(function () {
	$(".overlay-popup-options, .popup-options-wrapper").hide();
});

function getElByClass(className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

// invite form
getElByClass('btn-edit').addEventListener('click', showForm);
function showForm() {
	let name = getElByClass('clinic-name').innerHTML;
	let adress = getElByClass('clinic-adress').innerHTML;
	let phone = getElByClass('clinic-phone').innerHTML;
	let speciality = getElByClass('clinic-speciality').innerHTML;

	getElByClass('name-input').value = name;
	getElByClass('adress-input').value = adress;
	getElByClass('phone-input').value = phone;
	getElByClass('speciality-input').value = speciality;

	getElByClass('popup-form-wrapper').style.display = 'block';
	getElByClass('overlay-popup-form').style.display = 'block';
}

getElByClass('overlay-popup-form').addEventListener('click', hideForm);
getElByClass('form').addEventListener('submit', hideForm);
function hideForm() {
	getElByClass('popup-form-wrapper').style.display = 'none';
	getElByClass('overlay-popup-form').style.display = 'none';
}

function getElByClass(className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

// invite form
getElByClass('btn-add').addEventListener('click', showInviteForm);
function showInviteForm() {
	getElByClass('popup-form-wrapper-invite').style.display = 'block';
	getElByClass('overlay-popup-form-invite').style.display = 'block';
}

getElByClass('overlay-popup-form-invite').addEventListener('click', hideInviteForm);
getElByClass('invite-form').addEventListener('submit', hideInviteForm);
function hideInviteForm() {
	getElByClass('popup-form-wrapper-invite').style.display = 'none';
	getElByClass('overlay-popup-form-invite').style.display = 'none';
}


// change form
function showChangeDoctorForm(editEl) {
	let mail = editEl.parentNode.getElementsByClassName('row-mail')[0].innerHTML
	console.log(mail);

	getElByClass('popup-form-wrapper-change-doctor').style.display = 'block';
	getElByClass('overlay-popup-form-change-doctor').style.display = 'block';
}

getElByClass('overlay-popup-form-change-doctor').addEventListener('click', hideChangeDoctorForm);
getElByClass('change-doctor-form').addEventListener('submit', hideChangeDoctorForm);
function hideChangeDoctorForm() {
	getElByClass('popup-form-wrapper-change-doctor').style.display = 'none';
	getElByClass('overlay-popup-form-change-doctor').style.display = 'none';
}

$('.btn-open-info').click(function () {
	$(this).parent().toggleClass('active');
	$(this).parent().next().slideToggle();
});

$('.show_popup').click(function () {
	$('.popup-form-wrapper').show();
	$('.overlay-popup-form').show();
})

$('.overlay-popup-form').click(function () {
	$('.overlay-popup-form, .popup-form-wrapper').hide();
})