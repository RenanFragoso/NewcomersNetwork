import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Link, IndexLink } from 'react-router';
import * as authActions from '../actions/authActions';

import HeaderPage from './layout/common/HeaderPage.react';

class Header extends React.Component {

    constructor(props) {
        super(props);
    }

    componentDidUpdate(prevProps) {
        const oldUserInfo = prevProps.userInfo;
        const { userInfo } = this.props;
        if(userInfo.logged && (oldUserInfo.logged !== userInfo.logged)) {
            const storedToken = sessionStorage.getItem('nntoken');
            if(storedToken) {
                this.props.actions.getUserInfo();
            }
        }
    }

    render() {
        const headerContent = <HeaderPage />;
        return headerContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return { userInfo: state.auth.userInfo };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(authActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(Header);