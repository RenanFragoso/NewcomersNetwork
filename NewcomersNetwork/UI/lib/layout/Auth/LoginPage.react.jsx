import React from 'react';
import LoginForm from 'lib/forms/LoginForm.react';
import PropTypes from 'prop-types';
import { Grid, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as authActions from 'actions/authActions';
import { Link, withRouter } from 'react-router-dom';

class LoginPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            formError: false,
            formSuccess: false,
            submitting: false
        }

        this.loginFormSubmit = this._loginFormSubmit.bind(this);
        this.clearErrors = this._clearErrors.bind(this);
    }

    componentWillMount() {
        const storedToken = sessionStorage.getItem('nntoken');
        if(!this.props.auth.userInfo.logged && storedToken) {
            this.props.actions.getUserInfo();
        }
    }

    componentWillReceiveProps(newProps) {
        if(!newProps.auth.result)  {
            this.setState({ formError: true });
        }
        else {
            this.setState({ formError: false });
        }

        if(newProps.auth.status && newProps.auth.result) {
            this.setState({formSuccess: true});
            if(this.props.handleClose)
            {
                this.props.handleClose();
            }
        }
    }

    componentWillUpdate(newProps) {
        if(newProps.auth.userInfo.logged) {
            this.props.history.replace('/userprofile');
        }
    }

    _loginFormSubmit(values) { 
        this.setState({ submitting: true });
        this.props.actions.tryLogin(values);
    }

    _clearErrors() {
        this.setState({ formError: false });
    }

    render() {
        const headSection = <section id="page-title" className="bg-tpc-red txt-tpc-white sub-header" >
                                <div className="container clearfix">
                                    <h1 className="txt-tpc-white">Login</h1>
                                    <span className="txt-tpc-white">Login</span>
                                    <ol className="breadcrumb">
                                        <li><a href="/">Home</a></li>
                                        <li>Login</li>
                                    </ol>
                                </div>
                            </section>;

        const contentSection =  <section id="content" className="sub-content">
                                    <Grid>
                                        <Row>
                                            <Col xs={6} sm={4} lg={4} lgOffset={4} smOffset={4} xsOffset={3} className="login-headpanel bg-tpc-red">
                                                <h3>Newcomers Network Login</h3>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} sm={4} lg={4} lgOffset={4} smOffset={4} xsOffset={3} className="login-panel">
                                                <LoginForm 
                                                        onSubmit={this.loginFormSubmit}
                                                        error={this.props.auth.errorMessage}
                                                        hasError={this.state.formError} 
                                                        clearErrors={this.clearErrors} 
                                                        submitting={this.state.submitting} />
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} sm={4} lg={4} lgOffset={4} smOffset={4} xsOffset={3} className="login-footpanel">
                                                <Grid fluid>
                                                    <Row>
                                                        <Col xs={6} className="lg-footitm">
                                                            <Link to="/forgot">Forgot Password</Link>
                                                        </Col>
                                                        <Col xs={6} className="lg-footitm">
                                                            <Link to="/signup"> Sign Up</Link>
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
};

LoginPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { auth: state.auth };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(authActions, dispatch) };
}

LoginPage = connect(mapStateToProps, mapDispatchToProps)(LoginPage);

export default withRouter(LoginPage);