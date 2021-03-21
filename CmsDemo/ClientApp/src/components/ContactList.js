import React, { useEffect, useState } from 'react';
import { ContactCard } from './ContactCard';
import Auth from '../Auth';

export const ContactList = ({ getRefresh = null, onEdit = null, onDelete = null, groupId = 0 }) => {

	const [contacts, setContacts] = useState([]);
	const [isLoading, setIsLoading] = useState(true);

	// This method is called when the component is first added to the document
	useEffect(refresh, []);

	function refresh() {
		populateContactData();
	}

	if (getRefresh)
		getRefresh(refresh);

	function renderContactTable() {
		return (
			<div>
				{contacts.map(renderContact)}
			</div>
		);
	}

	function renderContact(c) {
		if (groupId !== 0 && c.groupId !== groupId)
			return null;

		return (
			<ContactCard key={c.id} contact={c} onEdit={onEdit} onDelete={onDelete}></ContactCard>
		);
	}

	async function populateContactData() {
		setIsLoading(true);

		const response = await fetch('contacts', {
			headers: Auth.getHeaders()
		});
		if (response.ok) {
			const data = await response.json();
			setContacts(data);
			setIsLoading(false);
		}
		else {

		}
	}

	return (
		<div>
			{renderContactTable()}
		</div>
	);
}
