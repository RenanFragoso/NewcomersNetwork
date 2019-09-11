import React from 'react';
import LoginPage from './layout/Auth/LoginPage.react';
import { withRouter } from 'react-router-dom';

class Login extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <LoginPage />
        );
    }
};

export default withRouter(Login);
