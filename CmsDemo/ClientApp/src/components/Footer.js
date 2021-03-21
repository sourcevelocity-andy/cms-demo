import React from 'react';

export const Footer = () => {

	function getYear() {
		var d = new Date();
		return d.getFullYear();
	}

	return (
		<div className="footer">
			<div className="text-center">&#169; {getYear()} Andy Bennett</div>
		</div>);
}
