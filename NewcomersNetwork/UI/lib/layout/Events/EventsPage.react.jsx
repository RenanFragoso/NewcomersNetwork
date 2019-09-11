import React from 'react';
import { withRouter, Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import * as eventsActions from 'actions/homePageActions';
import moment from 'moment';

class EventsPage extends React.Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.actions.loadEventsWidget();
    }

    render() {
        const { events } = this.props;
        const headSection = 
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Newcomers Network Events</span>
                </Col>
            </Row>;

        var eventsItems = [];
        events.map(
            (evnt, i) => {
                if( (i % 2) === 0 ) {
                    eventsItems.push(
                        <Row>
                            <Link to={evnt.Link}>
                                <div className="evt_left_panel">
                                    <img className="img-responsive svc-itm-img" src={evnt.Image} />
                                    <div className="evt-wdg-name">
                                        <span className="svc-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{evnt.Title}</span>
                                    </div>
                                </div>
                            </Link>
                            <div className="evt_left_line" />
                            <div className="evt_left_date">
                                <span>{moment(evnt.StartDate).format("DD")}</span>
                                <span>{moment(evnt.StartDate).format("MMM")}</span>
                            </div>
                        </Row>
                    );
                }
                else {
                    eventsItems.push(
                        <Row>
                            <div className="evt_right_date">
                                <span>{moment(evnt.StartDate).format("DD")}</span>
                                <span>{moment(evnt.StartDate).format("MMM")}</span>
                            </div>
                            <div className="evt_right_line" />
                            <Link to={evnt.Link}>
                                <div className="evt_right_panel">
                                    <img className="img-responsive svc-itm-img" src={evnt.Image} />
                                    <div className="evt-wdg-name">
                                        <span className="svc-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{evnt.Title}</span>
                                    </div>
                                </div>
                            </Link>
                        </Row>
                    );
                }
            }
        );

        const eventsGrid =
            <Row>
                <Col xs={12}>
                    <Grid>
                        <Row>
                            <div className="evt_tree_line" />
                        </Row>
                        <Row style={{"marginBottom": "80px"}}>
                            <div className="evt_right_date">
                                <span>{moment().format("YYYY")}</span>
                            </div>
                        </Row>
                        {eventsItems}
                    </Grid>
                </Col>
            </Row>;

        const eventsContent = 
            <Grid fluid className="evt_tree">
                {headSection}
                {eventsGrid}
            </Grid>;

        return eventsContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return { events: state.homePage.events };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(eventsActions, dispatch) };
}

EventsPage = withRouter(EventsPage);
export default connect(mapStateToProps, mapDispatchToProps)(EventsPage);