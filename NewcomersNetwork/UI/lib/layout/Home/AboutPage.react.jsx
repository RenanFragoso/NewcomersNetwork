import React from 'react';
import { Link } from 'react-router-dom';
import { Grid, Row, Col } from 'react-bootstrap';

class AboutPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        
        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">About Us</span>
                </Col>
            </Row>;
        
        const contentSection =  
            <Row>
                <Grid className="content-panel">
                    <Row>
                        <Col xs={12} md={6}>
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Who We Are</h4>
                            <p className="description-content">
                                The Newcomers Network of The Peoples Church provides newcomer settlement services, referral and orientation, in partnership with the YMCA and the Mennonite New Life Centre.
                            </p>
                            <br />
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Together we serve God</h4>
                            <p className="description-content">
                                Along with lay leaders and participating members, we endeavour to serve in the following areas:
                            </p>
                            <ul style={{"marginLeft": "20px"}}>
                                <li>New Immigrants Ministry</li>
                                <li>International Students Ministry</li>
                                <li>Refugee Ministry</li>
                                <li>Employment Mentoring Ministry</li>
                                <li>ESL Ministry</li>
                                <li>Walk with a Newcomer</li>
                            </ul>
                        </Col>
                        <Col xs={12} md={6}>
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Objective of This Website Section</h4>
                            <p className="description-content">
                                The Newcomers Network website provides a forum for newcomers and volunteers to connect and help each other in a community of Christ.
                            </p>
                            <br />
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Newcomers Corner</h4>
                            <p className="description-content">
                                This section provides various resources and tools for newcomers to help settle in and connect with others in the community.
                            </p>
                            <br />
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Volunteers Corner</h4>
                            <p className="description-content">
                                This section provides various volunteer opportunities for church members and volunteers to engage and offer help.
                            </p>
                            <br />
                            <h4 style={{"color": "#00588d", "fontWeight": "bold"}}>Just Share</h4>
                            <p className="description-content">
                                This section is aimed to be a useful tool for newcomers to post their needs on settlement, employment and housing.
                            </p>
                        </Col>
                    </Row>
                </Grid>
            </Row>;
        
        const aboutContents = 
            <Grid fluid>
                {headSection}
                {contentSection}
            </Grid>;
        
        return aboutContents;
    }
};

export default AboutPage;