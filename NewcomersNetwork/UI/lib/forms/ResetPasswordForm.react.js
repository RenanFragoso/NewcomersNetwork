import React from 'react';
import { Field, reduxForm } from 'redux-form';
import { Grid, Row, Col } from 'react-bootstrap';
import InputText from 'lib/components/InputText.react';
import {apiConfig} from 'api/';

const validate = values => {
    const errors = {};
    if (!values.Email) {
      errors.Email = "Required";
    }
    if (!values.NewPassword) {
        errors.NewPassword = "Required";
    };
    if (!values.ConfirmPassword) {
        errors.ConfirmPassword = "Required";
    };
    return errors;
};
const warn = values => {
    const warnings = {};
    return warnings;
};

class ResetPasswordForm extends React.Component {

    constructor(props) {
        super(props);
        
        this.captchaSelect = this._captchaSelect.bind(this);
    }

    componentDidMount() {
        this.props.change('Code', this.props.code||'');
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    render() {
        const { error, handleSubmit, submitting } = this.props;
        return( 
            <Grid>
                <Row>
                    <form onSubmit={handleSubmit}>

                        <input  type="hidden"
                                id="Code"
                                value=""
                                name="Code" />

                        <Field  name="Email" 
                                type="text"
                                id="Email"
                                component={InputText}
                                label="Email" />

                        <Field  name="Password" 
                                type="password"
                                id="Password"
                                component={InputText} 
                                label="New Password" />

                        <Field  name="ConfirmPassword" 
                                type="password"
                                id="ConfirmPassword"
                                component={InputText} 
                                label="Confirm Password" />

                        <button type="submit" className="btn nn-style-btn-2 bg-tpc-blue txt-tpc-white" disabled={submitting}>Reset Password</button>
                        {error && <strong>{error}</strong>}
                    </form>
                </Row>
            </Grid>
        );
    }
};

export default reduxForm({
    form: "resetPasswordForm",
    validate,
    warn
})(ResetPasswordForm);