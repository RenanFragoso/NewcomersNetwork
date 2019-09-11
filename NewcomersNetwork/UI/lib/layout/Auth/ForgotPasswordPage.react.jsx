import React from 'react';
import ForgotPasswordForm from 'lib/forms/ForgotPasswordForm.react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Grid, Row, Col } from 'react-bootstrap';
import * as usrAdmActions from 'actions/usrAdmActions';
import { withRouter } from 'react-router-dom';

class ForgotPasswordPage extends React.Component {

    constructor(props) {
        super(props);

        this.forgotPasswordFormSubmit = this._forgotPasswordFormSubmit.bind(this);
    }

    componentWillMount() {
        const storedToken = sessionStorage.getItem('nntoken');
        if(!this.props.auth.userInfo.logged && storedToken) {
            this.props.actions.getUserInfo();
        }
    }

    _forgotPasswordFormSubmit(values) { 
        this.props.actions.forgotPasswordSubmit(values);
    }

    render() {
        const headSection = <section id="page-title" className="bg-tpc-blue txt-tpc-white sub-header" >
                                <div className="container clearfix">
                                    <h1 className="txt-tpc-white">Recover Password</h1>
                                    <span className="txt-tpc-white">Recover Password</span>
                                    <ol className="breadcrumb">
                                        <li><a href="/">Home</a></li>
                                        <li>Recover Password</li>
                                    </ol>
                                </div>
                            </section>;




        const contentSection =  <section id="content" className="sub-content">
                                    <Grid>
                                        <Row>
                                            <Col xs={8} sm={8} lg={8} lgOffset={2} smOffset={2} xsOffset={2} className="login-headpanel bg-tpc-blue">
                                                <h3>Recover Password</h3>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={8} sm={8} lg={8} lgOffset={2} smOffset={2} xsOffset={2} className="login-panel">
                                                <ForgotPasswordForm 
                                                    onSubmit={this.forgotPasswordFormSubmit}/>
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
        return(contentSection);
    }
};

ForgotPasswordPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { auth: state.auth };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(ForgotPasswordPage));