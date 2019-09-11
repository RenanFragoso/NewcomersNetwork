import React from 'react';
import ResetPasswordPage from './layout/Auth/ResetPasswordPage.react';

class ResetPassword extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <ResetPasswordPage 
                confirmation={this.props.confirmation}/>
        );
    }
};

export default ResetPassword;
