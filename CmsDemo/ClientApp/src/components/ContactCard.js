import React, { useState } from 'react';
import { Collapse } from 'reactstrap';

export const ContactCard = ({ contact = null, onEdit = null, onDelete = null }) => {

	const [expanded, setExpanded] = useState(false);
	const [collapsed, setCollapsed] = useState(true);

	function toggleExpand() {
		setExpanded(!expanded);
		setCollapsed(false);
	}

	function modifyCss(css) {
		if (!collapsed)
			return css + " contact-expanded";
		return css;
	}

	function onExited() {
		setCollapsed(true);
	}

	function startEdit(evt) {

		evt.preventDefault();
		evt.stopPropagation();
		if (onEdit) {
			onEdit(contact);
		}
	}

	function startDelete(evt) {
		evt.preventDefault();
		evt.stopPropagation();
		if (onDelete) {
			onDelete(contact);
		}
	}

	function renderBirthDate() {
		if (!contact.prettyBirthdate) {
			return null;
		}

		return (
			<div className="row">
				<div className="col">
					<span className="label">Birthdate:</span>
					&nbsp;{contact.prettyBirthdate}
				</div>
			</div>
		);
	}

	return (
		<div>
			<div className="contact-card">

				<div className={modifyCss("contact-header")} onClick={toggleExpand}><i className={contact.favorite ? "bi-star favorite" : ""}></i> {contact.name}
					<div className="float-right">
						<button className="edit-button" aria-hidden onClick={startEdit}><i className="bi-pencil-square"></i></button>
						&nbsp;
						<button className="edit-button delete-button" aria-hidden onClick={startDelete}><i className="bi-person-x-fill"></i></button>
					</div>
				</div>
				<Collapse isOpen={expanded} onExited={onExited}>
					<div className={modifyCss("contact-body")}>
						{renderBirthDate()}
						<div className="keep-spacing">{contact.description}</div>
						<div className="text-right">
							<small>
								<span className="label">Created:</span>
							&nbsp;{contact.createdAt}
							</small>
						</div>
						<div className="text-right">
							<small>
								<span className="label">Last Updated:</span>
							&nbsp;{contact.updatedAt}
							</small>
						</div>

					</div>
				</Collapse>
			</div>
		</div>
	);


}
