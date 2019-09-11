import React from 'react';
import { Form, Field, reduxForm } from 'redux-form';
import { Grid, Row, Col, Alert } from 'react-bootstrap';
import { required, formValid } from './FormValidations';
import FontAwesome from 'react-fontawesome';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
//import ReCAPTCHA from 'react-google-recaptcha';
import PropTypes from 'prop-types';
import * as usrAdmActions from 'actions/usrAdmActions';
import ImgCanvasRender from 'lib/components/ImgCanvasRender.react';

class UpdateAvatarForm extends React.Component {

    constructor(props) {
        super(props);

        this.avatarClick = this._avatarClick.bind(this);
        this.updateFormImageFile = this._updateFormImageFile.bind(this);
    }

    componentDidMount() {
        window.addEventListener("clickFile", this.clickFile);
    }

    componentWillUnmount(newProps) {
        window.removeEventListener("clickFile", this.clickFile);
    }

    _avatarClick() {
        window.dispatchEvent(new Event('avatarclick', { bubbles: true }));
    }

    _updateFormImageFile(file) {
        this.props.change('AvatarFile', file.files[0]);
    }

    render() {
        const { handleSubmit, updateAvatarForm, valid, pristine } = this.props;
        const btnHidClass = (!updateAvatarForm.status || !valid || pristine ? "hidden" : "");
        const btnLoading = (!updateAvatarForm.status ? <div className="loader-btn2"></div> : <span><FontAwesome name="arrow-up" /> Update Avatar</span>);
        //const hasError = updateAvatarForm.status && updateAvatarForm.result;
        const updateAvatarFrmContents = 
                <Grid fluid>
                    <Form onSubmit={handleSubmit} model="updateAvatarForm" className="updAvatar-form">
                        <Row style={{ "marginBottom": "10px" }}>
                            <Col xs={10} md={10} lg={10} xsOffset={1} mdOffset={1} lgOffset={1}>
                                <div className="profile-pic">
                                    <ImgCanvasRender 
                                        image={this.props.userInfo.Picture} 
                                        updateFormImageFile={this.updateFormImageFile} 
                                        errors={this.props.errors} 
                                        centered />
                                    
                                    <div className="overlay">
                                        <div className="text">
                                            <div className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white" onClick={this.avatarClick} style={{"padding": "10px"}}>
                                                Change Picture
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </Col>
                        </Row>
                        <Row style={{ "marginBottom": "20px" }}>
                            <Col xs={10} md={6} lg={6} xsOffset={1} mdOffset={3} lgOffset={3}>
                                <button type="submit" className={"btn nn-style-btn-2 bg-tpc-red txt-tpc-white login-btn center " + btnHidClass} disabled={!updateAvatarForm.status || !valid || pristine}>
                                    {btnLoading}
                                </button>
                            </Col>
                        </Row>
                    </Form>
                </Grid>;
                
        return updateAvatarFrmContents;
    }
};

UpdateAvatarForm.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return {    
        updateAvatarForm: state.forms.updateAvatarForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(usrAdmActions, dispatch),
        onSubmit: bindActionCreators(usrAdmActions, dispatch).userAvatarUpdate
    };
}

UpdateAvatarForm = reduxForm({
    form: "updateAvatarForm"
})(UpdateAvatarForm);

export default connect(mapStateToProps, mapDispatchToProps)(UpdateAvatarForm);