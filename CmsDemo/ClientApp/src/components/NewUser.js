import React, { useState, useRef } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Alert, FormGroup, Label, Input, Form } from 'reactstrap';
import Auth from '../Auth';

//{ onCreate = null, contact = null }
export const NewUser = (props) => {

	const inputRef = useRef(null);
	const [modal, setModal] = useState(false);
	const handleOpen = () => inputRef.current.focus();
	const [error, setError] = useState(null);

	function show() {
		setModal(true);
	}

	props.getShow(show);

	function close() {
		setModal(false);
	}

	function submit(event) {
		if (event | typeof event.preventDefault != 'undefined') {
			event.preventDefault();
		}

		setError(null);

		const data = new FormData(event.target);
		var password = data.get('password');
		var confirm = data.get('confirm');

		if (password !== confirm) {
			setError("Password and confirm password must match");
			return;
		}

		const request = {
			userName: data.get('userName'),
			password: password
		};

		sendRequest(request);
	}

	async function sendRequest(request) {
		const response = await fetch('users', {
			method: 'POST',
			body: JSON.stringify(request),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		if (response.ok) {
			var data = await response.json();

			Auth.set(data.loginId);

			setModal(false);

			props.onLogin(data);
		}
		else {
			var json = await response.json();
			var msg = json?.message ?? response.statusText;
			setError(msg);
		}
	}

	return (
		<div>
			<Modal isOpen={modal} toggle={close} onOpened={handleOpen} >
				<Form onSubmit={(evt) => { submit(evt, this); }}>
					<ModalHeader toggle={close}>Create a New Account</ModalHeader>
					<ModalBody>

						<FormGroup>
							<Label for="userName">User Name</Label>
							<Input innerRef={inputRef} id="userName" name="userName" />
						</FormGroup>

						<FormGroup>
							<Label for="description">Password</Label>
							<Input name="password" type="password" id="password" />
						</FormGroup>

						<FormGroup>
							<Label for="description">Confirm Password</Label>
							<Input name="confirm" type="password" id="confirm" />
						</FormGroup>

						<Alert isOpen={error != null} color="danger">{error}</Alert>
					</ModalBody>
					<ModalFooter>
						<Button type="submit" color="primary" >Create</Button>{' '}
						<Button color="secondary" onClick={close}>Cancel</Button>
					</ModalFooter>
				</Form>
			</Modal>
		</div >
	);
}



