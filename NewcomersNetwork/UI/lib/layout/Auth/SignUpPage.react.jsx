'use strict';

import React from 'react';
import SignUpForm from 'lib/forms/SignUpForm.react';
import { Grid, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

class SignUpPage extends React.Component {

    constructor(props) {
        super(props);

    }

    componentDidUpdate() {
        if(this.props.signUpForm.status && this.props.signUpForm.result) {
            this.props.history.replace('/accountcreated');
        }
    }

    render() {
        //const sending = <div className={"loader"} style={{ "margin": "0px auto" }}></div>;
        const formComponent = <SignUpForm />;
        //const formContent = (this.state.submitting ? sending : formComponent);
        const contentSection =  <section id="content" className="sub-content">
                                    <Grid>
                                        <Row>
                                            <Col xs={8} sm={8} lg={8} lgOffset={2} smOffset={2} xsOffset={2} className="login-headpanel bg-tpc-red">
                                                <h3>SignUp for a Newcomers Network Account</h3>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={8} sm={8} lg={8} lgOffset={2} smOffset={2} xsOffset={2} className="login-panel">
                                                {formComponent}                                                
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={8} sm={8} lg={8} lgOffset={2} smOffset={2} xsOffset={2} className="login-footpanel"> 
                                                <Grid fluid>
                                                    <Row>
                                                        <Col xs={12} className="lg-footitm">
                                                            &nbsp;
                                                        </Col>
                                                    </Row>
                                                </Grid>
                                            </Col>
                                        </Row>
                                    </Grid>
                                </section>;
        
        return( contentSection );
    }
};

function mapStateToProps(state, ownProps) {  
    return {    signUpStatus: state.userAdm.signUpStatus,
                signUpForm: state.forms.signUpForm  };
}

function mapDispatchToProps(dispatch) {
    return {};
}

SignUpPage = connect(mapStateToProps, mapDispatchToProps)(SignUpPage)

export default withRouter(SignUpPage);