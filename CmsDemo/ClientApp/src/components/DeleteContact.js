import React, { useState, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Alert, Form } from 'reactstrap';
import Auth from '../Auth';

//{ onCreate = null, contact = null }
export const DeleteContact = (props) => {

	const [modal, setModal] = useState(false);
	const [error, setError] = useState(null);

	function close() {
		setModal(false);
	}

	function submit(event) {
		if (event | typeof event.preventDefault != 'undefined') {
			event.preventDefault();
		}

		setError(null);

		var id = props.contact?.id;

		if (!id) {
			setError('Contact was not found');
		}

		const request = {
			id: id
		};

		sendRequest(request);
	}

	useEffect(() => {
		setModal(props.contact !== null);
	}, [props.contact]);

	async function sendRequest(request) {
		const response = await fetch('contacts', {
			method: 'DELETE',
			body: JSON.stringify(request),
			headers: {
				'Authorization': Auth.getHeader(),
				'Content-Type': 'application/json'
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
			<Modal isOpen={modal} toggle={close} >
				<Form onSubmit={(evt) => { submit(evt, this); }}>
					<ModalHeader toggle={close}>Edit Contact</ModalHeader>
					<ModalBody>
						<h4>Are you sure that you want to delete this contact?</h4>
						<div>
							{props.contact?.name}
						</div>
						<Alert isOpen={error != null} color="danger">{error}</Alert>
					</ModalBody>
					<ModalFooter>
						<Button type="submit" color="primary" >Delete</Button>{' '}
						<Button color="secondary" onClick={close}>Cancel</Button>
					</ModalFooter>
				</Form>
			</Modal>
		</div >
	);
}

