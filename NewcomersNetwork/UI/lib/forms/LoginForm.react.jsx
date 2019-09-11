import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Field, reduxForm } from 'redux-form';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import InputText from 'lib/components/InputText.react';
import * as authActions from 'actions/authActions';
import { required } from './FormValidations';

class LoginForm extends React.Component {

    constructor(props) {
        super(props);

        this.clearErrors = this._clearErrors.bind(this);
    }

    _clearErrors() {
        this.props.reset();
        if(this.props.cleanErrors) {
            this.props.cleanErrors();
        }
    }

    render() {
        const { submitting } = this.props.formState;
        const hasError = !this.props.formState.result && this.props.formState.status;

        const { handleSubmit } = this.props;
        const formClass = (hasError ? 'hidden' : '');
        const errorClass = (hasError ? '' : 'hidden');
        const btnLoading = (submitting ? <div className="loader-btn2"></div> : <span>Login</span>);
        const gridContent = 
            <Grid fluid>
                <Row className={errorClass}>
                    <Col sm={12}>
                        <Alert bsStyle="danger">
                            <center><strong>Authentication Error</strong></center>
                        </Alert>
                    </Col>
                    <Col sm={12}>
                        <div className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white login-btn" onClick={this.clearErrors}><span>&lsaquo;&lsaquo; Try Again</span></div>
                    </Col>
                </Row>
                <Row className={formClass}>
                    <form onSubmit={handleSubmit}>
                        <Col sm={12}>
                            <Field  
                                name="username"
                                type="text"
                                id="username"
                                component={InputText}
                                label="Username" 
                                validate={[required]} />
                        </Col>
                        <Col sm={12}>
                            <Field  name="password" 
                                    type="password"
                                    id="password"
                                    component={InputText} 
                                    label="Password" 
                                    validate={[required]} />
                        </Col>

                        <Col sm={12}>
                            <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white login-btn" disabled={submitting || !this.props.valid}>
                                {btnLoading}
                            </button>
                        </Col>
                    </form>
                </Row>
            </Grid>
        
        return gridContent;
    }
};

LoginForm.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state) {  
    return {    
        auth: state.auth,
        formState: state.forms.loginForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(authActions, dispatch),
        onSubmit: bindActionCreators(authActions, dispatch).tryLogin,
        cleanErrors:  bindActionCreators(authActions, dispatch).cleanErrors
    };
}

LoginForm = reduxForm({
    form: "loginForm"
})(LoginForm);

export default connect(mapStateToProps, mapDispatchToProps)(LoginForm);