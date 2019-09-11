import React from 'react';
import { Grid, Row, Col, Nav, NavItem, Tab } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

import UserDetailsPage from './UserDetailsPage.react';
import UpdateProfilePage from './UpdateProfilePage.react';
import ChangePasswordPage from './ChangePasswordPage.react';

class AccountActionsPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        var intakeTab = <NavItem eventKey="4" className="useradm-navtab">Intake Form</NavItem>;
        var intakeContent = <Tab.Pane eventKey="4" className="tabcontent"></Tab.Pane>;

        const content = <Grid fluid className="useradm-tabs">
                            <Row>
                                <Col lg={12}>
                                    <Tab.Container id="accountActions" defaultActiveKey="1">
                                        <Row className="clearfix">
                                            <Nav bsStyle="pills">
                                                <NavItem eventKey="1" className="useradm-navtab">User Information</NavItem>
                                                {intakeTab}
                                            </Nav>
                                            <Tab.Content animation>
                                                <Tab.Pane eventKey="1" className="tabcontent">
                                                    <UserDetailsPage />
                                                </Tab.Pane>
                                                {intakeContent}
                                            </Tab.Content>
                                        </Row>
                                    </Tab.Container>
                                </Col>
                            </Row>
                        </Grid>;
        return content;
    }
}

export default AccountActionsPage;