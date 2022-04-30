import React from 'react';
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import { InfoBox, WidgetPanel } from 'lib/components/AdminUi';
import { FontAwesome } from 'react-fontawesome';
import { GridUsers } from 'lib/components/AdminUi';
import * as usersActions from 'actions/AdminUi/usersActions';
import DataTable from 'react-data-table-component';

class Users extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const usersContent = 
            <Grid fluid>
                <Row>
                    <Col xs={12} className="subp-header">
                        Users
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} className="nopadding actionline">

                        <Grid fluid className="nopadding">
                            <Row className="nopadding nomargin">
                            </Row>
                        </Grid>

                    </Col>
                </Row>
                <Row className="dashcontent">
                    <WidgetPanel 
                        icon="users" 
                        header="Newcomers Network Users">
                        
                        <GridUsers />
                    
                    </WidgetPanel>
                </Row>
            </Grid>;

        return usersContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return { users: state.users };
}

function mapDispatchToProps(dispatch) {
    return { usersActions: bindActionCreators(usersActions, dispatch) };
}

Users = withRouter(Users);
export default connect(mapStateToProps, mapDispatchToProps)(Users);