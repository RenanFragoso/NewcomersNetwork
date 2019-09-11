import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import configureStore from 'store/AdminUi/configureStore';
import initialState from 'reducers/AdminUi/initialState';
import NewcomersNetworkAdminUI from 'lib/NewcomersNetworkAdminUI.react';

import $ from 'jquery';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';
import './resources/css/nnadmin.less';

window.React = React;

const appContainer = document.querySelector('#nn_adminui');
const store = configureStore(initialState);

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <NewcomersNetworkAdminUI />
        </BrowserRouter>
    </Provider>,
    appContainer
);

