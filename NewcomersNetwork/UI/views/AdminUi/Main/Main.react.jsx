import React from 'react';
import { Switch, Route, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import { NNAdminHeader, NNAdminMenu } from 'views/AdminUi/Main';
import { Dashboard } from 'views/AdminUi/Dashboard';
import { Users } from 'views/AdminUi/Users';
import { NotFound } from 'views/AdminUi/Errors';
import { ToastContainer } from 'react-toastify';

class Main extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const headerContent = 
            <Row className="nopadding nomargin adminbg nnadmin-header">
                <Col xs={12} className="nopadding">
                    <NNAdminHeader />
                </Col>
            </Row>;

        const menuContent = 
            <Col xs={12} md={2} className="nopadding adminbg sidemenu">
                <NNAdminMenu />
            </Col>;

        const routerContent =
            <Col xs={12} md={10}>
                <Switch>
                    <Route exact path='/nnadmin' component={Dashboard} />
                    <Route exact path='/nnadmin/users' component={Users} />
                    <Route path="*" component={NotFound} />
                </Switch>
            </Col>;

        const mainContent = 
            <Row className="nopadding nomargin admincontent">
                {menuContent}
                {routerContent}
            </Row>;

        const adminContent = 
            <Grid fluid className="adminpage nopadding">
                <ToastContainer />
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