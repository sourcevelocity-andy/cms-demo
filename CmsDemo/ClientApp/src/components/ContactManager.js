import React, { useState } from 'react';
import { ContactList } from './ContactList';
import { EditContact } from './EditContact';
import { DeleteContact } from './DeleteContact';
import { TitleBar } from './TitleBar';
import { TopBar } from './TopBar';
import { GroupSelector } from './GroupSelector';

export const ContactManager = ({ onLogout = null }) => {

	const [contactToEdit, setContactToEdit] = useState(null);
	const [contactToDelete, setContactToDelete] = useState(null);
	const [groupId, setGroupId] = useState(0);

	var refresh = null;

	function onUpdate() {
		if (refresh)
			refresh();
	}

	function onNew() {
		setContactToEdit({});
	}

	function onEdit(target) {
		setContactToEdit(target);
	}

	function onCancel() {
		setContactToEdit(null);
	}

	function onDelete(target) {
		setContactToDelete(target);
	}

	function onGroupSelected(id) {
		setGroupId(id);
	}

	return (
		<div>
			<TopBar onLogout={onLogout} onNew={onNew}></TopBar>
			<TitleBar></TitleBar>
			<div className="d-flex justify-content-end">
				<div>
					<GroupSelector groupId={groupId} onGroupSelected={onGroupSelected}></GroupSelector>
				</div>
			</div>
			<EditContact contact={contactToEdit} onUpdate={onUpdate} onCancel={onCancel}></EditContact>
			<DeleteContact contact={contactToDelete} onUpdate={onUpdate}></DeleteContact>
			<ContactList groupId={groupId} getRefresh={(r) => { refresh = r; }} onEdit={onEdit} onDelete={onDelete}></ContactList>
		</div>
	);
}
