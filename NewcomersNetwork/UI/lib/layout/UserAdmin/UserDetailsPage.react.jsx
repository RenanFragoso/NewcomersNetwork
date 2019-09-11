import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Tabs, Tab, PanelGroup, Panel, Accordion } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom';
import * as usrAdmActions from 'actions/usrAdmActions';
import ChangePasswordForm from 'lib/forms/ChangePasswordForm.react';
import UserDetailsForm from 'lib/forms/UserDetailsForm.react';
import UpdateAvatarForm from 'lib/forms/UpdateAvatarForm.react';

class UserDetailsPage extends React.Component {

    constructor(props, context) {
        super(props, context);
        
        this.state = {
            userInfoactiveKey: "1"
        }

        this.handlePanelSelect = this.handlePanelSelect.bind(this);
        this.detailsFormSubmit = this._detailsFormSubmit.bind(this);
    }

    componentWillMount() {
        this.props.actions.getUserDetails();
    }

    handlePanelSelect(activeKey) {
        this.setState({ userInfoactiveKey: activeKey });
    }

    _detailsFormSubmit(formData) {
        var imageData = new FormData();
        imageData.append('AvatarFile', formData.AvatarFile);
        imageData.append('Captcha', formData.Captcha);
        this.props.actions.userAvatarUpdate(imageData);
        this.props.actions.detailsFormSubmit(formData);
    }

    render() {
        var { userDetails, userDetailsForm, userInfo, changePasswordForm } = this.props;
        const userDetailsContent =  
            <div className="content-wrap">
                <PanelGroup
                    accordion 
                    id="user-details-panels"
                    activeKey={this.state.userInfoactiveKey}
                    onSelect={this.handlePanelSelect} >

                    <Panel eventKey="1">
                        <Panel.Heading>
                            <Panel.Title toggle>Personal Information</Panel.Title>
                        </Panel.Heading>
                        <Panel.Body collapsible>
                            <div>First Name: {userDetails.FirstName}</div>
                            <div>Last Name: {userDetails.LastName}</div>
                            <div>E-Mail: {userDetails.Email}</div>
                            <div>Gender: {userDetails.Gender}</div>
                            <div>Marital Status: {userDetails.MaritalStatus}</div>
                            <div>Age Range: {userDetails.AgeRange}</div>
                            <div>Education: {userDetails.Education}</div>
                            <div>Nearest Intersection: {userDetails.NearestIntersection}</div>
                        </Panel.Body>
                    </Panel>

                    <Panel eventKey="2">
                        <Panel.Heading>
                            <Panel.Title toggle>Change Details</Panel.Title>
                        </Panel.Heading>
                        <Panel.Body collapsible>                                            
                            <UpdateAvatarForm 
                                userInfo={userInfo} />
                            <hr />
                            <UserDetailsForm 
                                userInfo={userInfo} />
                        </Panel.Body>
                    </Panel>
                </PanelGroup>
                <hr />
                <PanelGroup id="change-password-panel">
                
                    <Panel eventKey="1">
                        <Panel.Heading>
                            <Panel.Title toggle>Change Password</Panel.Title>
                        </Panel.Heading>
                        <Panel.Body collapsible>
                            <ChangePasswordForm />
                        </Panel.Body>
                    </Panel>
                </PanelGroup>
            </div>;
        return userDetailsContent;
    }
}

UserDetailsPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        userDetails: state.userAdm.userDetails,
        userInfo: state.auth.userInfo
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

UserDetailsPage = withRouter(UserDetailsPage);
export default connect(mapStateToProps, mapDispatchToProps)(UserDetailsPage);
