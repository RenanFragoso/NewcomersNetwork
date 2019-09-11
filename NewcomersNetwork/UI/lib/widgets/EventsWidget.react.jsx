import React from 'react';
import { withRouter, Link } from 'react-router-dom';
import { Grid, Col, Row } from 'react-bootstrap';
import moment from 'moment';
import FontAwesome from 'react-fontawesome';
import OwlCarousel from 'react-owl-carousel';

class EventsWidget extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        /*
        const sliderSettings = {
            margin: 10,
            nav: false,
            responsive: {
                0:{
                    items:1
                },
                768:{
                    items:2
                },
                1024:{
                    items:3
                },
                2560: {
                    items:4
                }
            }
        };

        var events = [];
        this.props.events.map((event, i) => {
            events.push(
                <div className="item service-item" key={i}>
                    <img className="img-responsive svc-itm-img" src={event.Image} />
                    <div className="svc-wdg-itm">
                        <span className="svc-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{event.Title}</span>
                        <span className="svc-itm-desc" style={{"color": "#fff", "fontSize": "0.8em"}}>{event.Description}</span>
                    </div>
                </div>
            );
        });

        var eventsSlider = <div></div>;
        if(events.length > 0) {
            eventsSlider =  <OwlCarousel {...sliderSettings} className="owl-theme">
                                {events}
                            </OwlCarousel>;
        };
        
        return (
            eventsSlider
        );*/
  
        if(this.props.events.length === 0) {
            return '';
        }
        
        const mainEvents = this.props.events.slice(0,4);
        var eventsItems = [];

        var firstEvent = mainEvents.slice(0,1)[0];
        eventsItems.push(
            <Row key={0}>
                <Col xs={6}>
                    <Link to={firstEvent.Link}>
                        <img className="img-responsive svc-itm-img" src={firstEvent.Image} />
                        <div className="evt-wdg-date" style={{"textAlign": "center"}}>
                            <span style={{"color": "#fff", "fontWeight": "bold", "fontSize": "1.5em"}}>{moment(firstEvent.StartDate).format("MMMM DD")}</span>
                        </div>
                        <div className="evt-wdg-name">
                            <span className="svc-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{firstEvent.Title}</span>
                        </div>
                    </Link>
                </Col>
            </Row>
        );

        var subEvents = [];
        const secondary = mainEvents.slice(1);
        secondary.map(
            (evnt, i) => {
                subEvents.push(
                    <Col xs={12} sm={12} md={4} key={i}>
                        <Link to={evnt.Link}>
                            <img className="img-responsive svc-itm-img" src={evnt.Image} />
                            <div className="evt-wdg-date" style={{"textAlign": "center"}}>
                                <span style={{"color": "#fff", "fontWeight": "bold", "fontSize": "1.5em"}}>{moment(evnt.StartDate).format("MMMM DD")}</span>
                            </div>
                            <div className="evt-wdg-name">
                                <span className="evt-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{evnt.Title}</span>
                            </div>
                        </Link>
                    </Col>
                );
            }
        );

        eventsItems.push(
            <Row style={{"marginTop": "40px"}} key={1}>
                {subEvents}
            </Row>);

        const eventsContent = 
            <Grid style={{"marginTop": "40px", "marginBottom": "40px"}}>
                {eventsItems}
            </Grid>;

        return eventsContent;
    }
}

export default withRouter(EventsWidget);