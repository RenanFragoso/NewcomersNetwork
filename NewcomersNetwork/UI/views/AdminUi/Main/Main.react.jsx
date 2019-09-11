import React from 'react';
import { Switch, Route, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';

import { Dashboard } from 'views/AdminUi/Dashboard';
import { NotFound } from 'views/AdminUi/Errors';

class Main extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const headerContent = 
            <Row>
            </Row>;

        const menuContent = 
            <Col xs={12} md={2}>
            </Col>;

        const routerContent =
            <Col xs={12} md={10}>
                <Switch>
                    <Route exact path='/nnadmin' component={Dashboard}/>
                    <Route path="*" component={NotFound} />
                </Switch>
            </Col>;

        const mainContent = 
            <Row>
                {menuContent}
                {routerContent}
            </Row>;

        const adminContent = 
            <Grid fluid>
                {headerContent}
                {mainContent}
            </Grid>;

        return adminContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return {};
}

Main = connect(mapStateToProps, mapDispatchToProps)(Main);
export default withRouter(Main);