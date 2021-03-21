import React, { useState, useRef, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Alert, FormGroup, Label, Input, Form } from 'reactstrap';
import Auth from '../Auth';
import { GroupSelector } from './GroupSelector';

//{ onCreate = null, contact = null }
export const EditContact = (props) => {

	const inputRef = useRef(null);
	const [modal, setModal] = useState(false);
	const handleOpen = () => inputRef.current.focus();
	const [error, setError] = useState(null);
	const [isNew, setIsNew] = useState(true);
	const [isFavorite, setIsFavorite] = useState(false);
	const [groupId, setGroupId] = useState(0);

	function close() {
		props.onCancel();
		//setModal(false);
	}

	function submit(event) {
		if (event | typeof event.preventDefault != 'undefined') {
			event.preventDefault();
		}

		setError(null);

		const data = new FormData(event.target);

		const request = {
			name: data.get('name'),
			birthdate: data.get('birthdate'),
			description: data.get('description'),
			favorite: isFavorite,
			groupId: groupId
		};

		if (!isNew)
			request.id = props.contact.id;

		sendRequest(request, isNew);
	}

	useEffect(() => {

		if (props.contact) {
			if (typeof props.contact.id != 'undefined') {
				setIsNew(false);
			} else {
				setIsNew(true);
			}

			if (props.contact?.favorite) {
				setIsFavorite(true);
			}
			else {
				setIsFavorite(false);
			}

			if (props.contact?.groupId) {
				setGroupId(props.contact.groupId);
			}
			else {
				setGroupId(0);
			}

			setModal(true);

		} else {
			setModal(false);
		}

	}, [props.contact]);

	async function sendRequest(request) {
		const response = await fetch('contacts', {
			method: isNew ? 'POST' : 'PUT',
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
			<Modal isOpen={modal} toggle={close} onOpened={handleOpen} >
				<Form onSubmit={(evt) => { submit(evt, this); }}>
					<ModalHeader toggle={close}>{isNew ? "Create a Contact" : "Edit a Contact"}</ModalHeader>
					<ModalBody>

						<FormGroup>
							<Label for="name">Contact Name</Label>
							<Input innerRef={inputRef} id="name" name="name" defaultValue={props.contact?.name} />
						</FormGroup>

						<FormGroup>
							<Label for="birthdate">Birthdate</Label>
							<Input type="date" name="birthdate" id="birthdate" defaultValue={props.contact?.birthdate} />
						</FormGroup>

						<FormGroup>
							<Label for="description">Description</Label>
							<Input type="textarea" name="description" id="description" defaultValue={props.contact?.description} />
						</FormGroup>

						<FormGroup>
							<GroupSelector right={false} groupId={groupId} onGroupSelected={id => { setGroupId(id); }}></GroupSelector>
						</FormGroup>

						<div className="clickable" onClick={() => { setIsFavorite(!isFavorite); }}><i className={isFavorite ? "bi-star" : "bi-app"}></i>&nbsp;Favorite</div>

						<Alert isOpen={error != null} color="danger">{error}</Alert>
					</ModalBody>
					<ModalFooter>
						<Button type="submit" color="primary">{isNew ? "Create" : "Modify"}</Button>{' '}
						<Button color="secondary" onClick={close}>Cancel</Button>
					</ModalFooter>
				</Form>
			</Modal>
		</div >
	);
}

