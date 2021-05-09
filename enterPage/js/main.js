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