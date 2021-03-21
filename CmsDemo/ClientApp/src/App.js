import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Footer } from './components/Footer';

import './custom.css'

export const App = () => {

	return (
		<div>
			<Layout>
				<Route exact path='/' component={Home} />
				<Footer></Footer>
			</Layout>
		</div>
	);

}

export default App;