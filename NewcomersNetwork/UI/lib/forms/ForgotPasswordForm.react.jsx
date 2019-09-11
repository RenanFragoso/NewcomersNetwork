import React from 'react';
import { Form, Field, reduxForm } from 'redux-form';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import InputText from 'lib/components/InputText.react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
//import ReCAPTCHA from 'react-google-recaptcha';
import {apiConfig} from 'api/';
import { required, email, formValid } from './FormValidations';
import * as usrAdmActions from 'actions/usrAdmActions';

class ForgotPasswordForm extends React.Component {

    constructor(props) {
        super(props);
        
        this.captchaSelect = this._captchaSelect.bind(this);
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    render() {
        debugger;
        const { error, handleSubmit, valid, pristine, submitting, submitSucceeded } = this.props;
        const btnLoading = (submitting ? <div className="loader-btn2"></div> : <span>Recover Password</span>);
        const formContent = 
            (submitSucceeded ?
                <Grid fluid>
                    <Row style={{"marginTop": "20px"}}> 
                        <Col sm={12} md={6} mdOffset={3}>
                            <Alert bsStyle="success">
                                <center><strong>Password recovery message sent.</strong></center>
                            </Alert>
                        </Col>
                    </Row>
                </Grid>:
                <Grid fluid>
                    <Row>
                        <Col xs={12} style={{"alignText": "center"}}>
                            <span style={{"fontWeight": "bold", "fontSize": "1.2em"}}>Provide your Newcomers Network registered e-mail in order recover your password</span>
                        </Col>
                    </Row>

                    <Row style={{"marginTop": "20px"}}>
                        <Form onSubmit={handleSubmit} model="ForgotPasswordForm">
                            <Col xs={12}>
                                <Field  
                                    name="Email"
                                    type="text"
                                    id="Email"
                                    component={InputText}
                                    validate={[email, required]}
                                    label="Email"
                                />
                            </Col>
                            <Col xs={12}>
                                <button type="submit" className="btn nn-style-btn-2 bg-tpc-blue txt-tpc-white login-btn center" disabled={submitting || !valid || pristine}>
                                    {btnLoading}
                                </button>
                            </Col>
                        </Form>
                    </Row>
                </Grid>);

        return formContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return {    
        forgotPasswordForm: state.forms.forgotPasswordForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(usrAdmActions, dispatch),
        onSubmit: bindActionCreators(usrAdmActions, dispatch).forgotPasswordSubmit
    };
}

ForgotPasswordForm = reduxForm({
    form: "forgotPasswordForm",
    enableReinitialize: true
})(ForgotPasswordForm);

export default connect(mapStateToProps, mapDispatchToProps)(ForgotPasswordForm);