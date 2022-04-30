'use strict';

import React from 'react';
import { Link, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class NNAdminMenu extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { userInfo } = this.props;
        const menuContent = 
            <ul className="nnadm-menu">
                <li className="nnadm-menuitem nnadm_menuactive">
                <Link to="/nnadmin"><FontAwesome name="dashboard"/>Dashboard</Link>
                </li>
                <li className="nnadm-menuitem">
                    <FontAwesome name="list-ul"/>JustShare
                </li>
                <li className="nnadm-menuitem">
                    <Link to="/nnadmin/users"><FontAwesome name="users"/>Users</Link>
                </li>
            </ul>;
                                    
        return menuContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return { userInfo: state.auth.userInfo };
}

function mapDispatchToProps(dispatch) {
    return {};
}

NNAdminMenu = connect(mapStateToProps, mapDispatchToProps)(NNAdminMenu);
export default withRouter(NNAdminMenu);
