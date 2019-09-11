import React from 'react';
import UserDetailsForm from 'lib/forms/UserDetailsForm.react';
import PropTypes from 'prop-types';
import { Row, Col, Tabs, Tab } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom';
import * as usrAdmActions from 'actions/usrAdmActions';

class UpdateProfilePage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            key: 1
        },

        this.userDetailsFormSubmit = this._detailsFormSubmit.bind(this);
        this.handleSelect = this._handleSelect.bind(this);
    }

    componentWillMount() {
        this.props.actions.getValuesLists();
        this.props.actions.getUserDetailsForm();
    }

    _handleSelect(key) {
        this.setState({ key });
    }

    _detailsFormSubmit(formData) {
        var imageData = new FormData();
        imageData.append('AvatarFile', formData.AvatarFile);
        imageData.append('Captcha', formData.Captcha);
        this.props.actions.userAvatarUpdate(imageData);
        this.props.actions.detailsFormSubmit(formData);
    }

    render() {
        var { userInfo, userDetailsForm } = this.props;
        const userProfileFormContent =  <div className="content-wrap">
                                            <UserDetailsForm 
                                                initialValues={userDetailsForm} 
                                                onSubmit={this.userDetailsFormSubmit}
                                                valuesLists={this.props.valuesLists} 
                                                userInfo={userInfo} />
                                        </div>;
        return userProfileFormContent;
    }
}

UpdateProfilePage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        userInfo: state.auth.userInfo,
        userDetailsForm: state.forms.userDetailsForm,
        valuesLists: state.forms.valuesLists
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

UpdateProfilePage = withRouter(UpdateProfilePage);
export default connect(mapStateToProps, mapDispatchToProps)(UpdateProfilePage);
