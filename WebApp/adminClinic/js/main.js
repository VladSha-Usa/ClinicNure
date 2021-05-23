function getElByClass (className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

// invite form
getElByClass('btn-edit').addEventListener('click', showForm);
function showForm () {
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
function hideForm () {
	getElByClass('popup-form-wrapper').style.display = 'none';
	getElByClass('overlay-popup-form').style.display = 'none';
}