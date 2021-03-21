import Cookies from 'js-cookie';

export function setLogin(data) {
	Cookies.set('login-key', data.id);
}

export function getLogin(data) {
	return Cookies.get('login-key');
}

export function clearLogin() {
	Cookies.remove('login-key');
}

