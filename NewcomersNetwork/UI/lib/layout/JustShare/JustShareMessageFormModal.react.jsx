import React from 'react';
import {Modal} from 'react-bootstrap';
import JustShareMessageForm from 'lib/forms/JustShareMessageForm.react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as justShareActions from 'actions/justShareActions';
import { withRouter } from 'react-router-dom';

class JustShareMessageFormModal extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            formError: false,
            formSuccess: false
        }

        this.justShareMessageFormSubmit = this._justShareMessageFormSubmit.bind(this);
        this.clearErrors = this._clearErrors.bind(this);
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

    _justShareMessageFormSubmit(values) {
        this.setState({ formError: false });
        this.props.actions.addJustShareMessage(values);
    }

    _clearErrors() {
        this.setState({ formError: false });
    }

    render() { 
        const showForm = this.props.show && !this.state.formSuccess;
        const typeName = this.props.mode === 'n' ? 'Need' : 'Support';
        return  <Modal show={showForm} onHide={this.props.handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Newcomers Network - Justshare Message</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <JustShareMessageForm 
                            onSubmit={this.justShareMessageFormSubmit} 
                            error={this.props.auth.errorMessage}
                            hasError={this.state.formError} 
                            clearErrors={this.clearErrors} 
                            valuesLists={this.props.valuesLists} 
                            mode={this.props.mode}
                            jsId={this.props.jsId} />
                    </Modal.Body>
                    <Modal.Footer>
                    </Modal.Footer>
                </Modal>;
    }
};

JustShareMessageFormModal.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { auth: state.auth };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(JustShareMessageFormModal));