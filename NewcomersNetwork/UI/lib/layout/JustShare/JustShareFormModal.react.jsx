import React from 'react';
import {Modal} from 'react-bootstrap';
import JustShareForm from 'lib/forms/JustShareForm.react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as justShareActions from 'actions/justShareActions';
import { withRouter } from 'react-router-dom';

class JustShareFormModal extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            formError: false,
            formSuccess: false
        }

        this.justShareFormSubmit = this._justShareFormSubmit.bind(this);
        this.clearErrors = this._clearErrors.bind(this);
    }

    componentDidMount() {
        this.props.actions.getNewJustShareId();
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
            this.props.handleClose();
        }
    }

    _justShareFormSubmit(formData) {
        this.setState({ formError: false });
        var multiPartFormData = new FormData();
        multiPartFormData.append('Id', this.props.newJustShareId);
        multiPartFormData.append('Category', formData.Category);
        multiPartFormData.append('Title', formData.Title);
        multiPartFormData.append('Text', formData.Text);
        multiPartFormData.append('ImageFile', formData.Image);
        multiPartFormData.append('Captcha', formData.Captcha);
        this.props.actions.justShareFormSubmit(multiPartFormData);
        this.props.actions.getNewJustShareId();
    }

    _clearErrors() {
        this.setState({ formError: false });
    }

    render() { 
        const showForm = this.props.show && !this.state.formSuccess;
        const typeName = this.props.mode === 'n' ? 'Need' : 'Support';
        return  <Modal show={showForm} onHide={this.props.handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Newcomers Network - Justshare Add {typeName}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <JustShareForm 
                            onSubmit={this.justShareFormSubmit} 
                            error={this.props.auth.errorMessage}
                            hasError={this.state.formError} 
                            clearErrors={this.clearErrors} 
                            valuesLists={this.props.valuesLists} 
                            mode={this.props.mode}
                            newJustShareId={this.props.newJustShareId} />
                    </Modal.Body>
                    <Modal.Footer>
                    </Modal.Footer>
                </Modal>;
    }
};

JustShareFormModal.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return {    auth: state.auth,
                newJustShareId: state.justShare.newJustShareId };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(JustShareFormModal));