import React from 'react';
import {Modal, Grid, Row, Col} from 'react-bootstrap';
import LoginForm from 'lib/forms/LoginForm.react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as authActions from 'actions/authActions';
import * as modalActions from 'actions/modalActions';
import { Link, withRouter } from 'react-router-dom';

class LoginModal extends React.Component {

    constructor(props) {
        super(props);
    }

    render() { 
        const { loginModal, modalActions } = this.props;
        return  <Modal show={loginModal.show} onHide={modalActions.hideLoginModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>Newcomers Network Login</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <LoginForm  />
                    </Modal.Body>
                    <Modal.Footer style={{"padding": "0px !important"}}>
                        <Grid fluid>
                            <Row>
                                <Col xs={6} className="lg-footitm">
                                    <Link to="/forgot" onClick={modalActions.hideLoginModal}>Forgot Password</Link>
                                </Col>
                                <Col xs={6} className="lg-footitm">
                                    <Link to="/signup" onClick={modalActions.hideLoginModal}> Sign Up</Link>
                                </Col>
                            </Row>
                        </Grid>
                    </Modal.Footer>
                </Modal>;
    }
};

LoginModal.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return {    auth: state.auth,
                loginModal: state.modal.login };
}

function mapDispatchToProps(dispatch) {
    return {    actions: bindActionCreators(authActions, dispatch),
                modalActions: bindActionCreators(modalActions, dispatch) };
}

LoginModal = withRouter(LoginModal);
export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);