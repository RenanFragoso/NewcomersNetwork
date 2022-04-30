'use strict';

import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class NNAdminHeader extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { userInfo } = this.props;
        const headerContent = 
            <Grid fluid className="nopadding">
                <Row className="nopadding nomargin">
                    <Col xs={12} md={2} className="nnadmin-logo nopadding">
                        <img src="/Content/images/NN_Logo.png" alt="Newcomers Network" className="img-responsive nnadmin-logo"/>
                    </Col>
                    <Col xs={12} md={10}>
                        <Grid fluid className="nopadding">
                            <Row>
                                <Col xs={12} className="nnadm-headnav nopadding">
                                    <ul className="nnadm-nav">
                                        <li className="nnadm-navitem"><FontAwesome name="user" />{userInfo.Name}</li>
                                        <li className="nnadm-navitem"><FontAwesome name="sign-out" />Logout</li>
                                    </ul>
                                </Col>
                            </Row>
                            <Row className="nnadm-headsubline">
                                <Col xs={12} className="nnadm-headbread">
                                    <ul className="nnadm-breadlist">
                                        <li className="nnadm-breaditm"><FontAwesome name="dashboard"/> Dashboard</li>
                                    </ul>
                                </Col>
                            </Row>
                        </Grid>
                    </Col>
                </Row>
            </Grid>;
                                    
        return headerContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return { userInfo: state.auth.userInfo };
}

function mapDispatchToProps(dispatch) {
    return {};
}

NNAdminHeader = connect(mapStateToProps, mapDispatchToProps)(NNAdminHeader);
export default withRouter(NNAdminHeader);
