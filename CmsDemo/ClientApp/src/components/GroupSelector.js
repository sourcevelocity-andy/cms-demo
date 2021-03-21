import React, { useState } from 'react';
import { Dropdown, DropdownToggle, DropdownItem, DropdownMenu } from 'reactstrap';


export const GroupSelector = ({ right = true, groupId = 0, onGroupSelected = null }) => {

	const [isOpen, setIsOpen] = useState(false);
	const groups = [{ id: 0, name: 'All' }, { id: 1, name: 'Personal' }, { id: 2, name: 'Work' }];

	function toggle() {
		setIsOpen(!isOpen);
	}

	function getName(id) {
		return groups[id].name;
	}

	return (
		<div>
			<Dropdown isOpen={isOpen} toggle={toggle} size="sm" className="group-selector">
				<DropdownToggle caret>
					Group: { getName(groupId) }
			</DropdownToggle>
				<DropdownMenu right={right}>
					{
						groups.map(group => (
							<DropdownItem key={group.id} onClick={() => { onGroupSelected(group.id); } }>{ group.name }</DropdownItem>
						))
					}
				</DropdownMenu>
			</Dropdown>
		</div>
	);
}
