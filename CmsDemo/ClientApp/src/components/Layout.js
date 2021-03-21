import React from 'react';
import { Container } from 'reactstrap';

export const Layout = ({ children = null }) => {

	return (
		<div>
			<Container>
				<div className="main-border">
					{children}
				</div>
			</Container>
		</div>
	);

}
