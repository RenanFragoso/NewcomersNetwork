import React from 'react';
import { Form, Field, reduxForm } from 'redux-form';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import { required, formValid } from './FormValidations';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import InputText from 'lib/components/InputText.react';
import FontAwesome from 'react-fontawesome';
//import ReCAPTCHA from 'react-google-recaptcha';
import PropTypes from 'prop-types';
import * as usrAdmActions from 'actions/usrAdmActions';

class ChangePasswordForm extends React.Component {

    constructor(props) {
        super(props);
        this.captchaSelect = this._captchaSelect.bind(this);
        this.formResetOk = this._formResetOk.bind(this);
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    _formResetOk() {
        this.props.reset();
    }

    render() {
        debugger;
        const { handleSubmit, formPanelId, valid, pristine, submitting, submitSucceeded } = this.props;
        const btnLoading = (submitting ? <div className="loader-btn2"></div> : <span>Change Password</span>);
        const formContents = ( submitSucceeded ?
            <Row>
                <Col xs={12} style={{"textAlign": "center", "fontWeight":"bold", "marginTop": "20px"}}>
                    <Alert bsStyle="success">Password Updated  
                        <div role="button" data-toggle="collapse" href={formPanelId} onClick={this.formResetOk} style={{"display": "inline-block", "float":"right"}}>
                            <FontAwesome name="window-close"/>
                        </div>
                    </Alert>
                </Col>
            </Row> :
            <Row>
                <Form onSubmit={handleSubmit} model="changePasswordForm">
                    <Col sm={12}>
                        <Field  
                            name="OldPassword"
                            type="password"
                            id="OldPassword"
                            component={InputText}
                            label="Old Password" 
                            validate={[required]} />
                    </Col>
                    <Col sm={12}>
                        <Field  name="Password" 
                                type="password"
                                id="Password"
                                component={InputText} 
                                label="New Password" 
                                validate={[required, formValid.password]} />
                    </Col>
                    <Col sm={12}>
                        <Field  name="ConfirmPassword" 
                                type="password"
                                id="ConfirmPassword"
                                component={InputText} 
                                label="Confirm Password" 
                                validate={[required, formValid.password]} />
                    </Col>
                    <Col sm={6} smOffset={3}>
                        <div>
                            <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white login-btn center" disabled={submitting || !valid || pristine || submitSucceeded}>
                                {btnLoading}
                            </button>
                        </div>
                    </Col>
                </Form>
            </Row>);
        
        const chngPwdFrmContents = 
            <Grid fluid>
                {formContents}
            </Grid>;

        return chngPwdFrmContents;
    }
};

ChangePasswordForm.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return {    
        changePasswordStatus: state.userAdm.changePasswordStatus,
        changePasswordForm: state.forms.changePasswordForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(usrAdmActions, dispatch),
        onSubmit: bindActionCreators(usrAdmActions, dispatch).passwdChangeSubmit
    };
}

ChangePasswordForm = reduxForm({
    form: "changePasswordForm"
})(ChangePasswordForm);

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);