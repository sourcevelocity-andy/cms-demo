import React, { useState, useRef } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Alert, FormFeedback, InputGroup, InputGroupAddon, InputGroupText, FormGroup, Label, Input, Form } from 'reactstrap';
import Auth from '../Auth';
export const NewContact = (props) => {

	const inputRef = useRef(null);
	const [modal, setModal] = useState(false);
	const toggle = () => setModal(!modal);
	const handleOpen = () => inputRef.current.focus();
	const [error, setError] = useState(null);

	function create(event) {
		if (event | typeof event.preventDefault != 'undefined') {
			event.preventDefault();
		}

		setError(null);

		const data = new FormData(event.target);

		const request = {
			name: data.get('name'),
			birthdate: data.get('birthdate'),
			description: data.get('description')
		};

		sendRequest(request);
	}

	async function sendRequest(request) {
		const response = await fetch('contacts', {
			method: 'POST',
			body: JSON.stringify(request),
			headers: {
				'Content-Type': 'application/json',
				'Authorization': Auth.getHeader()
			}
		});

		if (response.ok) {
			setModal(false);
			if (props.onUpdate)
				props.onUpdate();
		}
		else {
			var json = await response.json();
			var msg = json?.message ?? response.statusText;
			setError(msg);
		}
	}

	return (
		<div>
			<Button color="primary" onClick={toggle}>Create New Contact</Button>

			<Modal isOpen={modal} toggle={toggle} onOpened={handleOpen} >
				<Form onSubmit={(evt) => { create(evt, this); }}>
					<ModalHeader toggle={toggle}>Create a New Contact</ModalHeader>
					<ModalBody>

						<FormGroup>
							<Label for="name">Contact Name</Label>
							<Input innerRef={inputRef} id="name" name="name" />
						</FormGroup>

						<FormGroup>
							<Label for="birthdate">Birthdate</Label>
							<Input type="date" name="birthdate" id="birthdate" />
						</FormGroup>

						<FormGroup>
							<Label for="description">Description</Label>
							<Input type="textarea" name="description" id="description" />
						</FormGroup>

						<Alert isOpen={error != null} color="danger">{error}</Alert>
					</ModalBody>
					<ModalFooter>
						<Button type="submit" color="primary" >Create</Button>{' '}
						<Button color="secondary" onClick={toggle}>Cancel</Button>
					</ModalFooter>
				</Form>
			</Modal>

		</div >
	);

}

