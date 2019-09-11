import React from 'react';
import { Form, Field, reduxForm } from 'redux-form';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import InputTextArea from 'lib/components/InputTextArea.react';
import { required } from './FormValidations';
import * as justShareActions from '../../actions/justShareActions';

class JustShareMessageForm extends React.Component {

    constructor(props) {
        super(props);

        this.captchaSelect = this._captchaSelect.bind(this);
    }

    componentDidMount() {
        this.props.change('ClassifiedId', this.props.jsId);
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    render() {
        const { jsMsgForm, handleSubmit, valid, pristine, submitting } = this.props;
        const modeDesc = (this.props.mode === 'n' ? 'Need' : 'Support');
        const btnSend = (submitting ? <div className="loader-btn2"></div> : <span>Send Message</span>); 
        const formContent = this.props.submitSucceeded ? 
            <Grid fluid style={{"marginTop": "20px"}}>
                <Row>
                    <Col sm={12} md={6} mdOffset={3}>
                        <Alert bsStyle="success">
                            <center><strong>Message sent.</strong></center>
                        </Alert>
                    </Col>
                </Row> 
            </Grid> :
            <Form onSubmit={handleSubmit} className="msgjs-form">
                <Grid fluid style={{"marginTop": "20px"}}>
                    <Row>
                        <Col sm={6} smOffset={3}>
                            <Field  
                                name="Message"
                                id="Message"
                                component={InputTextArea}
                                label="Message"
                                placeholder="Type here your message"
                                validate={[required]} />
                        </Col>
                        <Col sm={6} smOffset={3}>
                            <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white form-btn" disabled={submitting || !valid || pristine}>
                                {btnSend}
                            </button>                            
                        </Col>
                    </Row>
                </Grid>
            </Form>;

        return formContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return {    
        jsMsgForm: state.forms.jsMsgForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(justShareActions, dispatch),
        onSubmit: bindActionCreators(justShareActions, dispatch).jsMsgFormSubmit
    };
}

JustShareMessageForm = reduxForm({
    form: "jsMsgForm",
    enableReinitialize: true
})(JustShareMessageForm);

export default connect(mapStateToProps, mapDispatchToProps)(JustShareMessageForm);