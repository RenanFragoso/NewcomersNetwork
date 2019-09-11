'use strict';

import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import GoogleMapReact from 'google-map-react';

import { apiConfig } from 'api';

class ContactPage extends React.Component {

    constructor(props) {
        super(props);

        this.renderMarkers = this._renderMarkers.bind(this);
    }

    _renderMarkers(map, maps) {
        let marker = new maps.Marker({
          position: { lat: 43.766065, lng: -79.393555 },
          map,
          title: 'The Peoples Church'
        });
    }

    render() {
        
        const headSection =
                <Row className="sub-header" >
                    <Col xs={12}>
                        <span className="txt-tpc-red">Find/Contact Us</span>
                    </Col>
                </Row>;

        const mapSection = 
            <Row>
                <Col xs={12} style={{ "height": "400px", "padding": "0px"}}>
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: apiConfig.gmapsKey }}
                        defaultCenter={{ lat: 43.766065, lng: -79.393555 }}
                        defaultZoom={16} 
                        onGoogleApiLoaded={({map, maps}) => this.renderMarkers(map, maps)} 
                        yesIWantToUseGoogleMapApiInternals={true} />
                </Col>
            </Row>;

        const contentSection =  
            <Row style={{"marginTop": "40px"}}>
                <Grid className="content-panel">
                    <Row>
                        <Col lg={6} sm={12}>
                            <address>
                                <span style={{"color": "#ba0f6b", "fontWeight": "bold"}}>The Peoples Church</span><br />
                                374 Sheppard Avenue East<br />
                                Toronto, Ontario M2N 3B6<br />
                                <abbr title="Phone Number"><strong>Phone:</strong></abbr> (416) 222 3341<br />
                                <abbr title="Fax Number"><strong>Fax:</strong></abbr> (416) 222 3344
                            </address>
                        </Col>

                        <Col lg={6} sm={12}>
                            <address>
                                <span style={{"color": "#ba0f6b", "fontWeight": "bold"}}>Mailing Address</span><br />
                                The Peoples Church<br />
                                374 Sheppard Avenue East<br />
                                Toronto, Ontario<br />
                                Canada M2N 3B6
                            </address>
                        </Col>                                            
                    </Row>
                    <Row>
                        <Col lg={6} sm={12}>
                            <address>
                                <span style={{"color": "#ba0f6b", "fontWeight": "bold"}}>Welcome Centre Hours</span><br />
                                Monday, Tuesday & Thursday: 8:00 AM – 7:30 PM<br />
                                Wednesday & Friday: 8:00 AM – 9:30 PM<br />
                                Saturday: 8:30 AM – 2:00 PM<br />
                                Sunday (Connection Centre): 9:00 AM – 4:00 PM
                            </address>
                        </Col>
                        <Col lg={6} sm={12}>
                            <address>
                                <span style={{"color": "#ba0f6b", "fontWeight": "bold"}}>Business Office Hours</span><br />
                                Monday – Friday: 8:30AM – 4:30PM<br />
                                Saturday: Closed<br />
                                Sunday: Closed
                            </address>
                        </Col>
                    </Row>
                </Grid>
            </Row>;
        
        const contactContents = 
            <Grid fluid>
                {headSection}
                {mapSection}
                {contentSection}
            </Grid>;
        
        return contactContents;
    }
};

export default ContactPage;