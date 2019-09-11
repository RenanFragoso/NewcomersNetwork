import React from 'react';
import PropTypes from 'prop-types';
import { Grid, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { withRouter, Link } from 'react-router-dom';

import * as usrAdmActions from 'actions/usrAdmActions';

class AccountCreatedPage extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
        if (!(this.props.signUpForm.result && this.props.signUpForm.status && this.props.signUpForm.confirmMail != '')) {
            this.props.history.push("/signup");
        }
    }
    
    render() {
        
        const confirmMail = this.props.signUpForm.confirmMail || "";
        const contentSection =  <section id="content" className="sub-content">
                                    <Grid>
                                        <Row>
                                            <Col xs={6} sm={6} lg={6} lgOffset={3} smOffset={3} xsOffset={3} className="login-headpanel bg-tpc-blue">
                                                <h3>Newcomers Network Account Created</h3>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} sm={6} lg={6} lgOffset={3} smOffset={3} xsOffset={3} className="login-panel">
                                                <span style={{ "display": "block" }}>An E-Mail was sent to: {confirmMail}</span>
                                                <span>Please confirm using the link in the message to enable your account.</span>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} sm={6} lg={6} lgOffset={3} smOffset={3} xsOffset={3} className="login-footpanel">
                                                <Grid fluid>
                                                    <Row>
                                                        <Col xs={12} className="lg-footitm" style={{"fontWeight": "bold"}}>
                                                            <Link to="/"> Re-send confirmation</Link>
                                                        </Col>
                                                    </Row>
                                                </Grid>
                                            </Col>
                                        </Row>
                                    </Grid>
                                </section>;
        return( 
            <div>
                {contentSection}
            </div>
        );
    }
}

AccountCreatedPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        signUpForm: state.forms.signUpForm
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

AccountCreatedPage = connect(mapStateToProps, mapDispatchToProps)(AccountCreatedPage);
export default withRouter(AccountCreatedPage);
