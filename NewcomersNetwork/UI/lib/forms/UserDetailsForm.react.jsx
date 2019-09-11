import React from 'react';
import { Form, Field, reduxForm, reset } from 'redux-form';
import { connect } from 'react-redux';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import InputText from 'lib/components/InputText.react';
import CheckBox from 'lib/components/CheckBox.react';
import Select from 'lib/components/Select.react';
import FontAwesome from 'react-fontawesome';
import { required, email, maxLength15, checked, formValid } from './FormValidations';
import { bindActionCreators } from 'redux';
import PropTypes from 'prop-types';
import * as usrAdmActions from 'actions/usrAdmActions';

class UserDetailsForm extends React.Component {

    constructor(props) {
        super(props);

        this.captchaSelect = this._captchaSelect.bind(this);
        this.formResetOk = this._formResetOk.bind(this);
        this.formResetError = this._formResetError.bind(this);
    }

    componentDidMount() {
        this.props.actions.getValuesLists();
        this.props.actions.getUserDetailsForm();
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    _formResetError() {
        this.props.clearSubmit();
    }

    _formResetOk() {
        this.props.actions.getUserDetailsForm();
        this.props.actions.getUserDetails();
        this.props.reset();
    }

    render() {
        debugger;
        const { error, status, pristine, valid, handleSubmit, submitting, submitSucceeded, userDetailsForm, formPanelId} = this.props;
        const genderList = this.props.valuesLists.find((item) => item.ListName === 'gender');
        const maritalStatusList = this.props.valuesLists.find((item) => item.ListName === 'maritalstatus');
        const ageRangeList = this.props.valuesLists.find((item) => item.ListName === 'agerange');
        const educationList = this.props.valuesLists.find((item) => item.ListName === 'education');
        const formResult = ( submitSucceeded ? 
            <Alert bsStyle="success">Personal Information updated  
                <div role="button" data-toggle="collapse" href={formPanelId} onClick={this.formResetOk} style={{"display": "inline-block", "float":"right"}}>
                    <FontAwesome name="window-close"/>
                </div>
            </Alert> :
            <Alert bsStyle="danger">Personal data update failed. Try Again later
                <div onClick={this.formResetError} style={{"display": "inline-block", "float":"right", "cursor": "pointer"}}>
                    <FontAwesome name="window-close"/>
                </div>
            </Alert> );
        
        /*
        <ReCAPTCHA
        ref="recaptcha"
        sitekey={apiConfig.recaptchaSiteKey} 
        onChange={this.captchaSelect} />
        */

        var formContents = ( submitSucceeded ? 
            <Row>
                <Col xs={12} style={{"textAlign": "center", "fontWeight":"bold", "marginTop": "20px"}}>
                    {formResult}
                </Col> 
            </Row> :
            <Row>
                <Form onSubmit={handleSubmit} model="userDetailsForm" className="usrdet-form" encType="multipart/form-data">
                    <input  type="hidden"
                            id="Id"
                            value="-1"
                            name="Id" />

                    <Col sm={12} md={6}>
                        <Field  
                            name="FirstName"
                            type="text"
                            id="FirstName"
                            component={InputText}
                            label="First Name" 
                            validate={[required]} />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="LastName"
                            type="text"
                            id="LastName"
                            component={InputText}
                            label="Last Name"
                            validate={[required]} />
                    </Col>
                    <Col sm={12}>
                        <Field  
                            name="Email"
                            type="text"
                            id="Email"
                            component={InputText}
                            label="E-Mail"
                            validate={[required, email]} />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="Gender"
                            id="Gender"
                            component={Select}
                            label="Gender"
                            options={(genderList?genderList.Itens:[])} />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="MaritalStatus"
                            id="MaritalStatus"
                            component={Select}
                            label="Marital Status" 
                            options={(maritalStatusList?maritalStatusList.Itens:[])} />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="AgeRange"
                            id="AgeRange"
                            component={Select}
                            label="Age Range" 
                            options={(ageRangeList?ageRangeList.Itens:[])} />
                    </Col>       
                    <Col sm={12} md={6}>
                        <Field  
                            name="Education"
                            type="text"
                            id="Education"
                            component={Select}
                            label="Education"
                            options={(educationList?educationList.Itens:[])} />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="NearestIntersection"
                            type="text"
                            id="NearestIntersection"
                            component={InputText}
                            label="Nearest Intersection" />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="PostalCode"
                            type="text"
                            id="PostalCode"
                            component={InputText}
                            label="Postal Code" />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="ConsentToContact"
                            type="checkbox"
                            id="ConsentToContact"
                            component={CheckBox}
                            label="I do consent to contact me" />
                    </Col>
                    <Col sm={12} md={6}>
                        <Field  
                            name="IsImmigrant"
                            type="checkbox"
                            id="IsImmigrant"
                            component={CheckBox}
                            label="I am a newcomer" />
                    </Col>
                    <Col sm={6} smOffset={3}>
                        <div>
                            <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white form-btn" disabled={submitting || !valid || pristine || submitSucceeded}>
                                <span>Update Details</span>
                            </button>
                        </div>
                    </Col>
                </Form>
            </Row>);

        const detailsFormContents = 
            <Grid fluid>
                {formContents}
            </Grid>;

        return detailsFormContents;
    }
};

UserDetailsForm.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return {    
        initialValues: state.forms.userDetailsForm,
        valuesLists: state.forms.valuesLists,
        userDetailsForm: state.forms.userDetailsForm,
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(usrAdmActions, dispatch),
        onSubmit: bindActionCreators(usrAdmActions, dispatch).userDetailsFormSubmit
    };
}

UserDetailsForm = reduxForm({
    form: "userDetailsForm",
    enableReinitialize: true
})(UserDetailsForm);

export default connect(mapStateToProps, mapDispatchToProps)(UserDetailsForm);