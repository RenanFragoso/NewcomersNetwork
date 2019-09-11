import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Grid, Row, Col } from 'react-bootstrap';
import NNUserMessage from 'lib/widgets/NNUserMessage.react';
import EventsWidget from './EventsWidget.react';
import NNUserMessageWidget from './NNUserMessageWidget.react';
import * as homePageActions from 'actions/homePageActions';

class NNUserDashboard extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.props.actions.loadEventsWidget();
    }

    render() {

        return (
            <Grid fluid className="no-gutters">
                <Row className="usrdsh-subt">
                    <Col lg={12}>Newcomers Network Messages</Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        <NNUserMessageWidget userMessages={this.props.userAdm.userMessages.messages}/>
                    </Col>
                </Row>
                <Row className="usrdsh-subt">
                    <Col lg={12}>Upcoming Events</Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        <EventsWidget events={this.props.events}/>
                    </Col>
                </Row>
            </Grid>
        );
    }
}

NNUserDashboard.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        events: state.homePage.events,
        userAdm: state.userAdm
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(homePageActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(NNUserDashboard);