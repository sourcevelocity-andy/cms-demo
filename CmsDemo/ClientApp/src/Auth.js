import Cookies from 'js-cookie';

class AuthManager {
	static _instance;

	constructor() {
		this._auth = Cookies.get('login-key');
	}

	getHeader() {
		return 'BASIC ' + this._auth;
	}

	getHeaders() {
		if (this._auth) {
			return {
				'Authorization': this.getHeader()
			};
		}

		return {};
	}

	get() {
		return this._auth;
	}

	set(key) {
		if (key) {
			this._auth = key;
			Cookies.set('login-key', key, { expires: 7 });
		}
		else {
			this.clear();
		}
	}

	clear() {
		this._auth = null;
		Cookies.remove('login-key');
	}

	isLoggedIn() {
		return this._auth != null;
	}
};

// singleton

var Auth = AuthManager._instance || (AuthManager._instance = new AuthManager());

export default Auth;
