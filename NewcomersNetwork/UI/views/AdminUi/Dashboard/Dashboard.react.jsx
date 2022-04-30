import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import { InfoBox, WidgetPanel } from 'lib/components/AdminUi';

class Dashboard extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        const dashboardContent = 
            <Grid fluid>
                <Row>
                    <Col xs={12} className="subp-header">
                        Dashboard
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} className="nopadding infoline">

                        <Grid fluid className="nopadding">
                            <Row className="nopadding nomargin">
                                <InfoBox 
                                    size={3}
                                    data="80"
                                    content="JustShare awaiting approval"
                                    type="red"
                                    icon="list-ul" />
                                <InfoBox 
                                    size={3}
                                    data="30%"
                                    content="Lorem Ipsum"
                                    type="blue"
                                    icon="list-ul" />
                                <InfoBox 
                                    size={3}
                                    data="50"
                                    content="Active Users"
                                    type="gold"
                                    icon="users" />
                                <InfoBox 
                                    size={3}
                                    data="1500"
                                    content="Lorem Impsum"
                                    type="lime"
                                    icon="list-ul" />
                            </Row>
                        </Grid>

                    </Col>
                </Row>
                <Row className="dashcontent">
                    <WidgetPanel 
                        icon="signal" 
                        header="This is a Panel Title">
                                <InfoBox 
                                    size={3}
                                    data="1500"
                                    content="Lorem Impsum"
                                    type="lime"
                                    icon="list-ul" />
                    </WidgetPanel>

                    <WidgetPanel 
                        icon="signal" 
                        header="This is a collapsible Panel"
                        collapsible
                        id="panel1"
                        defaultopen >
                    </WidgetPanel>
                </Row>
            </Grid>;

        return dashboardContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return {};
}

Dashboard = connect(mapStateToProps, mapDispatchToProps)(Dashboard);
export default withRouter(Dashboard);