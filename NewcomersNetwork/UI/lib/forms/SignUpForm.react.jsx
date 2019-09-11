import React from 'react';
import { Form, Field, reduxForm, SubmissionError } from 'redux-form';
import { Grid, Row, Col } from 'react-bootstrap';
import { bindActionCreators } from 'redux';
import PropTypes from 'prop-types';
import InputText from 'lib/components/InputText.react';
import CheckBox from 'lib/components/CheckBox.react';
//import ReCAPTCHA from 'react-google-recaptcha';
import { connect } from 'react-redux';
import * as usrAdmActions from 'actions/usrAdmActions';
import { required, email, maxLength15, checked, formValid } from './FormValidations';

class SignUpForm extends React.Component {

    constructor(props) {
        super(props);

        this.captchaSelect = this._captchaSelect.bind(this);
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    render() {
        const { handleSubmit, signUpForm, valid, pristine, submitting, submitSucceeded } = this.props;
        var btnLoading = ( submitting ? <div className="loader-btn2"></div> : <span>Create Login</span>);
        var formContents = 
            <Grid fluid>
                <Row>
                    <Form onSubmit={handleSubmit} model="signupForm">
                        <Col xs={12}>
                            <input  
                                name="Id"
                                type="hidden"
                                id="Id" />
                            <Field  
                                name="FirstName"
                                type="text"
                                id="FirstName"
                                component={InputText}
                                label="First Name"
                                validate={[required]} />

                            <Field  
                                name="LastName"
                                type="text"
                                id="LastName"
                                component={InputText}
                                label="Last Name"
                                validate={[required]} />

                            <Field  
                                name="Email"
                                type="text"
                                id="Email"
                                component={InputText}
                                label="E-Mail"
                                validate={[required, email]} />

                            <Field  
                                name="Username"
                                type="text"
                                id="Username"
                                component={InputText}
                                label="Username"
                                validate={[required, maxLength15]} />

                            <Field  name="Password" 
                                    type="password"
                                    id="Password"
                                    component={InputText} 
                                    label="Password"
                                    model="Password"
                                    validate={[required, formValid.password]} />

                            <Field  name="ConfirmPassword" 
                                    type="password"
                                    id="ConfirmPassword"
                                    component={InputText} 
                                    model="ConfirmPassword"
                                    label="Confirm Password"
                                    validate={[required, formValid.password]} />

                            <Field  
                                name="ConsentToContact"
                                type="checkbox"
                                id="ConsentToContact"
                                component={CheckBox}
                                label=" I do consent to contact me" />

                            <Field  
                                name="IsNewcomer"
                                type="checkbox"
                                id="IsNewcomer"
                                component={CheckBox}
                                label=" I am a newcomer" />
                        
                            <Field  
                                name="TermsAccepted"
                                type="checkbox"
                                id="TermsAccepted"
                                component={CheckBox}
                                label=" I do accept the Newcomers Network terms of use" 
                                validate={[formValid.termsSelected]} />
                        </Col>
                        <Col xs={12}>
                            <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white login-btn center" disabled={submitting || !valid || pristine}>
                                {btnLoading}
                            </button>
                        </Col>
                    </Form>
                </Row>
            </Grid>;

            return formContents;
    }
};

function mapStateToProps(state, ownProps) {  
    return {    
        signUpStatus: state.userAdm.signUpStatus,
        signUpForm: state.forms.signUpForm 
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(usrAdmActions, dispatch),
        onSubmit: bindActionCreators(usrAdmActions, dispatch).signupFormSubmit
    };
}

SignUpForm = reduxForm({
    form: "signUpForm"
})(SignUpForm);

export default connect(mapStateToProps, mapDispatchToProps)(SignUpForm);