import React from 'react';
import { withRouter, Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import * as eventsActions from 'actions/eventsActions';
import moment from 'moment';

class EventPage extends React.Component {

    constructor(props) {
        super(props);

        this.joinClick = this._joinClick.bind(this);
    }

    componentDidMount() {
        const { eventId } = this.props.match.params;
        this.props.actions.getEventById(eventId);
    }

    _joinClick() {
        const { event } = this.props;
        window.location = event.ExternalLink;
    }

    render() {
        const { event } = this.props;
        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Newcomers Network Event</span>
                </Col>
            </Row>;
        
        var eventDetails = '';

        if(event.Name == undefined) {
            eventDetails =
                <Row style={{"backgroundColor": "#fff", "padding": "15px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                    <Col xs={12}>
                        <div className="loader" style={{"margin": "0px auto"}}/>
                    </Col>
                </Row>;
        }
        else {
            eventDetails = 
                <Row>
                    <Col xs={12}>
                        <Grid style={{"backgroundColor": "#fff", "padding": "40px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                            <Row className="evt-head-title">
                                <Col xs={12}>
                                    <span className="evt-name-title">
                                        {event.Name}
                                    </span>
                                </Col>
                            </Row>
                            <Row className="evt-panel">
                                <Col xs={12} md={8} className="evt-head-img">
                                    <img className="img-responsive" src={event.HeadImg}  />
                                </Col>
                                <Col xs={12} md={4} className="evt-inf-panel">
                                    
                                    <Grid fluid style={{"minHeight": "100%"}}>
                                        <Row className="evt-inf-title">
                                            <Col xs={12}>
                                                Event Information
                                            </Col>
                                        </Row>
                                        <Row className="evt-inf-content">
                                            <Col xs={12} className="evt-inf-items">
                                                <span>Location</span>
                                                {event.Location}
                                                <span>Starts</span>
                                                {moment(event.StartDate).format("MM/DD/YYYY hh:mm A")}
                                                <span>Ends</span> 
                                                {moment(event.EndDate).format("MM/DD/YYYY hh:mm A")}
                                            </Col>
                                            <Col xs={12} className="btn nn-style-btn-1 btn-brd3-b" onClick={this.joinClick}>
                                                <span style={{"fontSize": "1em", "fontWeight": "bold"}}>Click to Join</span>
                                            </Col>
                                        </Row>
                                    </Grid>

                                </Col>
                            </Row>
                            <Row className="evt-det-text1">
                                <Col xs={12}>
                                    {event.Text1}
                                </Col>
                            </Row>
                            <Row className="evt-det-text2">
                                <Col xs={12}>
                                    {event.Text2}
                                </Col>
                            </Row>
                            <Row className="evt-det-footer">
                                <Col xs={12}>
                                    {event.Footer}
                                </Col>
                            </Row>
                        </Grid>
                    </Col>
                </Row>;
        }

        const eventContent =             
            <Grid fluid>
                {headSection}
                {eventDetails}
            </Grid>;
        
        return eventContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return { event: state.events.event };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(eventsActions, dispatch) };
}

EventPage = withRouter(EventPage);
export default connect(mapStateToProps, mapDispatchToProps)(EventPage);