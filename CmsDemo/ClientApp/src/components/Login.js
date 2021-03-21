import React, { useState } from 'react';

import { Form, Button, Alert, Input, FormGroup } from 'reactstrap';
import { NewUser } from './NewUser';
import { TitleBar } from './TitleBar';

import Auth from '../Auth';

export const Login = ({ onLogin = null }) => {

	var createNewAccount;

	const [error, setError] = useState(null);

	function submit(event) {
		event.preventDefault();

		setError(null);

		const data = new FormData(event.target);

		const request = {
			userName: data.get('userName'),
			password: data.get('password')
		};

		sendRequest(request);
	}

	async function sendRequest(request) {
		const response = await fetch('logins', {
			method: 'POST',
			body: JSON.stringify(request),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		if (response.ok) {
			var data = await response.json();

			Auth.set(data.loginId);

			onLogin();
		}
		else {
			var json = await response.json();
			var msg = json?.message ?? response.statusText;
			setError(msg);
		}
	}

	function create() {
		createNewAccount();
	}

	return (
		<div>
			<div className="topBar">
				<div className="float-right">
					<button onClick={create} className="create">Create a New Account</button>
				</div>
				<div className="clearfix"></div>
			</div>
			<NewUser onLogin={onLogin} getShow={(cb) => { createNewAccount = cb; }}></NewUser>

			<TitleBar></TitleBar>
			<div className="login-background">
				<div className="row justify-content-center align-middle">
					<div className="col-6 align-middle">
						<Form onSubmit={submit}>
							<FormGroup>
								<Input autoFocus name="userName" id="userName" placeholder="User Name" />
							</FormGroup>
							<FormGroup>
								<Input type="password" name="password" id="examplePassword" placeholder="Password" />
							</FormGroup>
							<Button type="submit" className="d-none">Submit</Button>
						</Form>
						<Alert isOpen={error != null} color="danger">{error}</Alert>
					</div>
				</div>
			</div>
		</div>
	);
}


