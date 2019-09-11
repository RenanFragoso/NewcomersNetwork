import React from 'react';
import { bindActionCreators } from 'redux';
import { Form, Field, reduxForm } from 'redux-form';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Grid, Row, Col } from 'react-bootstrap';
import CheckBox from 'lib/components/CheckBox.react';
import Select from 'lib/components/Select.react';
import ImgCanvasRender from 'lib/components/ImgCanvasRender.react';
import {apiConfig} from 'api/'
import { required, email, formValid } from './FormValidations';
import InputText from 'lib/components/InputText.react';
import InputTextArea from 'lib/components/InputTextArea.react';
import * as justShareActions from 'actions/justShareActions';

class JustShareForm extends React.Component {

    constructor(props) {
        super(props);

        this.imageClick = this._imageClick.bind(this);
        this.captchaSelect = this._captchaSelect.bind(this);
        this.updateFormImageFile = this._updateFormImageFile.bind(this);
    }

    componentDidMount(props) {
        this.props.change('Id', "-1");
        this.props.change('Type', this.props.mode);
        this.props.actions.getValuesLists();
    }

    _captchaSelect(captchaValue) {
        this.props.change('Captcha', captchaValue);
    }

    _imageClick() {
        window.dispatchEvent(new Event('avatarclick', { bubbles: true}));
    }

    _updateFormImageFile(file) {
        this.props.change('Image', file.files[0]);
    }

    render() {
        const { mode, addJustShareForm, valuesLists, pristine, valid, error, handleSubmit } = this.props;
        const modeDesc = (mode === 'n' ? 'Need' : 'Support');
        const justShareGroups = valuesLists.find((x) => x.ListName === "classifiedgroups") || [];
        var btnLoading = ( !addJustShareForm.status ? <div className="loader-btn2"></div> : <span>Add Your {modeDesc}</span>);

        const imgCanvas =   ( mode === 'n' ?
                            '':
                            <div className="profile-pic">
                                <ImgCanvasRender 
                                    image={""} 
                                    updateFormImageFile={this.updateFormImageFile} />
                                <div className="overlay">
                                    <div className="text">
                                        <div className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white" onClick={this.imageClick} style={{"padding": "10px"}}>
                                            Add Picture
                                        </div>
                                    </div>
                                </div>
                            </div>);
                
        var formContent = 
                    <Form onSubmit={handleSubmit} model="addJustShareForm">        

                        <Col lg={8} lgOffset={2}>
                            {imgCanvas}

                            <Field  
                                name="Category"
                                type="text"
                                id="Category"
                                component={Select}
                                label="Select a JustShare Category"
                                options={(justShareGroups?justShareGroups.Itens:[])} 
                                validate={[required]} />

                            <Field  
                                name="Title"
                                type="text"
                                id="Title"
                                component={InputText}
                                label="Write the Title"
                                placeholder="Type here your JustShare title" 
                                validate={[required]} />

                            <Field  
                                name="Text"
                                id="Text"
                                componentClass="textarea"
                                component={InputTextArea}
                                label="Write the Text"
                                placeholder="Type here your JustShare text" 
                                validate={[required]} />

                            <div>
                                <button type="submit" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white form-btn" disabled={!addJustShareForm.status || !valid || pristine}>
                                    {btnLoading}
                                </button>
                            </div>
                        </Col>
                    </Form>;

        return( formContent );
    }
};

JustShareForm.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        valuesLists: state.forms.valuesLists,
        addJustShareForm: state.forms.addJustShareForm
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(justShareActions, dispatch),
        onSubmit: bindActionCreators(justShareActions, dispatch).justShareFormSubmit
    };
}

JustShareForm = reduxForm({
    form: "addJustShareForm",
    enableReinitialize: true
})(JustShareForm);

export default connect(mapStateToProps, mapDispatchToProps)(JustShareForm);