import React, { useState } from 'react';
import { ContactManager } from './ContactManager'
import { Login } from './Login'
import Auth from '../Auth';

export const Home = () => {

	const [loggedIn, setLoggedIn] = useState(Auth.isLoggedIn());

	function onLogin() {
		setLoggedIn(true);
	}

	function onLogout() {
		setLoggedIn(false);
	}

	function renderMain() {
		if (loggedIn) {
			return (
				<div>
					<ContactManager onLogout={onLogout}></ContactManager>
				</div>
			);
		}
		else {
			return (
				<Login onLogin={onLogin}></Login>
			);
		}
	}

	return (
		<div className="inner-app">
			{renderMain()}
		</div>
	);
}
