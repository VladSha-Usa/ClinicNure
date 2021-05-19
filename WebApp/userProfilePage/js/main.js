function getElByClass (className, index = 0) {
	return document.getElementsByClassName(className)[index];
}

getElByClass('btn-edit').addEventListener('click', showForm);
function showForm () {
	getElByClass('popup-form-wrapper').style.display = 'block';
	getElByClass('overlay-popup-form').style.display = 'block';
}

getElByClass('overlay-popup-form').addEventListener('click', hideForm);
getElByClass('changing-form').addEventListener('submit', hideForm);
function hideForm () {
	getElByClass('popup-form-wrapper').style.display = 'none';
	getElByClass('overlay-popup-form').style.display = 'none';
}