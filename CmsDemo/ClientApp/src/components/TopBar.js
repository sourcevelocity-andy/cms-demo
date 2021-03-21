import React from 'react';
import Auth from '../Auth';

export const TopBar = ({ onLogout = null, onNew = null }) => {

	function logout(evt) {
		evt.preventDefault();
		Auth.clear();
		onLogout();
	}

	function create(evt) {
		evt.preventDefault();
		onNew();
	}

	return (
		<div className="topBar">
			&nbsp;
			<div className="float-right">
				<button onClick={create}>Create a Contact</button>
				<button onClick={logout}>Logout</button>
			</div>
			<div className="clearfix"></div>
		</div>
	);
}
