import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Grid, Row, Col } from 'react-bootstrap';

class NNAdminMenu extends React.Component {

    constructor(props) {
        super(props);
        
        this.state = {
        }
    }

    render() {

        const menuContent = 
            <Col>
                Menu
            </Col>;

        return menuContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return { };
}

NNAdminMenu = connect(mapStateToProps, mapDispatchToProps)(NNAdminMenu);
export default withRouter(NNAdminMenu);