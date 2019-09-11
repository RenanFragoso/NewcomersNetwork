import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';

class Dashboard extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        const dashboardContent = 
            <Grid fluid>
                <Row>
                    <Col xs={12}>
                        Dashboard
                    </Col>
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