import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Tabs, Tab } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom';

import * as usrAdmActions from 'actions/usrAdmActions';
import ChangePasswordForm from 'lib/forms/ChangePasswordForm.react';

class ChangePasswordPage extends React.Component {

    constructor(props) {
        super(props);

        this.changePasswordFormSubmit = this._passwordFormSubmit.bind(this);
    }

    componentWillMount() {
        this.props.actions.getValuesLists();
        this.props.actions.getUserDetailsForm();
    }

    _passwordFormSubmit(formData) {
        this.props.actions.passwdFormSubmit(formData);
    }

    render() {
        var { userInfo } = this.props;
        const userProfileFormContent =  <div className="content-wrap">
                                            <ChangePasswordForm 
                                                onSubmit={this.changePasswordFormSubmit} />
                                        </div>;
        return userProfileFormContent;
    }
}

ChangePasswordPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        //userInfo: state.auth.userInfo,
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

ChangePasswordPage = withRouter(ChangePasswordPage);
export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordPage);
