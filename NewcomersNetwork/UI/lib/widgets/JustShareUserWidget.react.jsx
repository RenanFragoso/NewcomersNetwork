import React from 'react';
import { Grid, Row, Col, Label, Modal } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as justShareActions from 'actions/justShareActions';
import UserJustShareWidgetItem from './UserJustShareWidgetItem.react';
import {goToDetails} from 'actions/justShareActions';
import {withRouter} from 'react-router-dom';

class JustShareUserWidget extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            offset: 5
        }
    }

    componentWillMount() {
        this.props.actions.getUserJustShare(this.state.page, this.state.offset);
    }

    render() {
        const { userJustShares } = this.props;
        const justShares = userJustShares.justShareWidget.UserJustShare || [];
        const userJusrSharesItems = justShares.map((item,n) => <UserJustShareWidgetItem item={item} key={n} />);
        const paginationButtons = '';
        const contents =             
            <Grid fluid>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        {userJusrSharesItems}
                    </Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        {paginationButtons}
                    </Col>
                </Row>
            </Grid>;
        
        return contents;
    }
}

function mapStateToProps(state, ownProps) {  
    return { 
        userJustShares: state.justShare.userJustShares
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

JustShareUserWidget = withRouter(JustShareUserWidget);
export default connect(mapStateToProps, mapDispatchToProps)(JustShareUserWidget);