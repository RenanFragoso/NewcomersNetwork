import React from 'react';
import { Grid, Row, Col, Nav, NavItem, Tab, Label } from 'react-bootstrap';
import { JustShareUserWidget, JustShareUserMsgWidget, NNUserDashboard } from 'lib/widgets';

class DashboardPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const content = <Grid fluid className="useradm-tabs">
                            <Row>
                                <Col lg={12}>
                                    <Tab.Container id="dashboard" defaultActiveKey="1">
                                        <Row className="clearfix">
                                            <Nav bsStyle="pills">
                                                <NavItem eventKey="1" className="useradm-navtab">My Newcomers Network</NavItem>
                                                <NavItem eventKey="2" className="useradm-navtab">Just Share</NavItem>
                                                <NavItem eventKey="3" className="useradm-navtab">My Calendar</NavItem>
                                            </Nav>
                                            <Tab.Content animation>
                                                <Tab.Pane eventKey="1" className="tabcontent">
                                                    <NNUserDashboard />
                                                </Tab.Pane>
                                                <Tab.Pane eventKey="2" className="tabcontent">
                                                    <JustShareUserWidget />
                                                    <JustShareUserMsgWidget />
                                                </Tab.Pane>
                                                <Tab.Pane eventKey="3" className="tabcontent">
                                                </Tab.Pane>
                                            </Tab.Content>
                                        </Row>
                                    </Tab.Container>
                                </Col>
                            </Row>
                        </Grid>;
        return content;
    }
}

export default DashboardPage;