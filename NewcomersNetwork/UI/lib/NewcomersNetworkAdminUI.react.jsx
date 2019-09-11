import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import * as authActions from 'actions/AdminUi/authActions';
import { Main } from 'views/AdminUi/Main';

class NewcomersNetworkAdminUI extends React.Component { 
    
    constructor(props) {
        super(props);
    }
    
    componentDidMount() {
        this.props.authActions.getUserInfo();
    }
    
    render() {
        const nnAdminContent = <Main />;
        return nnAdminContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return { authActions: bindActionCreators(authActions, dispatch)};
}

NewcomersNetworkAdminUI = connect(mapStateToProps, mapDispatchToProps)(NewcomersNetworkAdminUI);

export default withRouter(NewcomersNetworkAdminUI);