import React from 'react';
import PropTypes from 'prop-types';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Grid, Row, Col } from 'react-bootstrap';
import * as usrAdmActions from 'actions/usrAdmActions';
import * as authActions from 'actions/authActions';
import NNUserMessageWidget from 'lib/widgets/NNUserMessageWidget.react';
import ChangePasswordForm from 'lib/forms/ChangePasswordForm.react';
import UserDetailsForm from 'lib/forms/UserDetailsForm.react';
import { JustShareUserWidget } from 'lib/widgets';
import { apiUrlNoSufix } from 'api/nnApiConfig';

class UserProfilePage extends React.Component {

    constructor(props) {
        super(props);
        
        this.checkUserLogin = this._checkUserLogin.bind(this);
        this.logout = this._logout.bind(this);
        this.goToAdmin = this._goToAdmin.bind(this);
    }

    componentWillMount() {
        this.checkUserLogin();
        this.props.userAdmActions.getUserDetails();
    }
    
    _checkUserLogin() {
        if(!this.props.userInfo.logged) {
            this.props.history.push('/');
        }
        else {
            this.props.actions.getUserInfo();
        }
    }
    
    _logout() {
        this.props.actions.logout();
        this.props.history.replace('/');
    }

    _goToAdmin() {
        window.location = apiUrlNoSufix + '/nnadmin';
    }

    render() {
        const { userInfo, userDetails } = this.props;
        
        const profileHeader = 
                <Row className="txt-tpc-red usradm-head">
                    <Col xs={12}>
                        Your Account
                    </Col>
                </Row>;

        const adminBtn = (  userInfo.HasAdminAccess ? 
            <div className="btn nn-style-btn-1 btn-brd3-r" style={{"marginLeft": "10px"}} onClick={this.goToAdmin}>
                <span style={{"fontSize": "1em", "fontWeight": "bold"}}>Admin Area</span>
            </div> : 
            '');
        const userProfileCont = 
            <Grid fluid className="usradm-page">
                {profileHeader}
                <Row className="usradm-lncont">
                    <Grid>
                        <Col xs={12} sm={12} md={6}>
                            <Grid fluid>
                                <Row className="usradm-hditm">
                                    <Col xs={12}>
                                        <span className="usradm-namehead">Hello, {userInfo.Name}</span>
                                        <div className="btn nn-style-btn-1 btn-brd3-b" onClick={this.logout}>
                                            <span style={{"fontSize": "1em", "fontWeight": "bold"}}>Logout</span>
                                        </div>
                                        {adminBtn}
                                    </Col>
                                </Row>
                            </Grid>
                        </Col>
                        <Col xs={12} sm={12} md={6}>
                            <Grid fluid>
                                <Row className="usradm-hditm">
                                    <Col xs={12}>
                                        <span className="usradm-titlehead">Your Username</span>
                                        <span>{userInfo.UserName}</span>
                                    </Col>
                                </Row>
                            </Grid>
                        </Col>
                    </Grid>
                </Row>

                <Row className="usradm-rowdiv" />

                <Row>
                    <Grid>
                        <Col xs={12} sm={12} md={6}>

                            <Grid fluid className="usradm-panel">
                                <Row>
                                    <Col xs={12}>
                                        <span className="usradm-titlehead">Newcomers Network Messages</span>
                                        <NNUserMessageWidget />
                                    </Col>
                                </Row>
                            </Grid>

                            <Grid fluid className="usradm-panel">
                                <Row>
                                    <Col xs={12}>
                                        <span className="usradm-titlehead">Your Password</span>
                                        <span>**********</span>
                                        <div className="btn nn-style-btn-1 btn-brd3-b pull-right" role="button" data-toggle="collapse" href={"#pwdform"} aria-expanded="false" aria-controls="PwdForm">
                                            <span style={{"fontSize": "1em", "fontWeight": "bold"}}>Update</span>
                                        </div>
                                    </Col>
                                </Row>
                                <Row className="collapse panel-divider" id="pwdform" aria-expanded="false" style={{"marginTop": "20px", "padding": "20px"}}>
                                    <ChangePasswordForm formPanelId="#pwdform"/>
                                </Row>
                            </Grid>

                            <Grid fluid className="usradm-panel">
                                <Row>
                                    <Col xs={12}>
                                        <span className="usradm-titlehead">Your Personal Information</span>
                                        
                                        <span className="usradm-detline">First Name: {userDetails.FirstName}</span>
                                        <span className="usradm-detline">Last Name: {userDetails.LastName}</span>
                                        <span className="usradm-detline">E-Mail: {userDetails.Email}</span>
                                        <span className="usradm-detline">Gender: {userDetails.Gender}</span>
                                        <span className="usradm-detline">Marital Status: {userDetails.MaritalStatus}</span>
                                        <span className="usradm-detline">Age Range: {userDetails.AgeRange}</span>
                                        <span className="usradm-detline">Education: {userDetails.Education}</span>
                                        <span className="usradm-detline">Nearest Intersection: {userDetails.NearestIntersection}</span>

                                        <div className="btn nn-style-btn-1 btn-brd3-b pull-right" role="button" data-toggle="collapse" href={"#detailsform"} aria-expanded="false" aria-controls="DetForm">
                                            <span style={{"fontSize": "1em", "fontWeight": "bold"}}>Update</span>
                                        </div>
                                    </Col>
                                </Row>
                                <Row className="collapse panel-divider" id="detailsform" aria-expanded="false" style={{"marginTop": "20px", "padding": "20px"}}>
                                    <UserDetailsForm formPanelId="#detailsform" />
                                </Row>
                            </Grid>

                        </Col>
                        <Col xs={12} sm={12} md={6}>
                            
                            <Grid fluid className="usradm-panel">
                                <Row>
                                    <Col xs={12}>
                                        <span className="usradm-titlehead">Your Created JustShare</span>
                                        <JustShareUserWidget />
                                    </Col>
                                </Row>
                            </Grid>

                        </Col>
                    </Grid>

                </Row>

            </Grid>;

        return userProfileCont;
    }
}

UserProfilePage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        auth: state.auth,
        userInfo: state.auth.userInfo,
        userDetails: state.userAdm.userDetails
    };
}

function mapDispatchToProps(dispatch) {
    return {    actions: bindActionCreators(authActions, dispatch),
                userAdmActions: bindActionCreators(usrAdmActions, dispatch) };
}

UserProfilePage = withRouter(UserProfilePage);
export default connect(mapStateToProps, mapDispatchToProps)(UserProfilePage);