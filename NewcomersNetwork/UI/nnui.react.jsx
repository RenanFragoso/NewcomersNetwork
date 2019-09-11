import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import configureStore from './store/configureStore';
import initialState from './reducers/initialState';
import NewcomersNetworkUI from './lib/NewcomersNetworkUI.react';

import $ from 'jquery';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';
import './resources/css/newcomers-network.less';

window.React = React;

const appContainer = document.querySelector('#nnui');
const store = configureStore(initialState);

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <NewcomersNetworkUI />
        </BrowserRouter>
    </Provider>,
    appContainer
);

