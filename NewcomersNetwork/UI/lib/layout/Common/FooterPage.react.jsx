import React from 'react';
import PropTypes from 'prop-types';
import FontAwesome from 'react-fontawesome';
import { Col, Row, Grid } from 'react-bootstrap';
import { Link } from 'react-router-dom';

class FooterPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <footer className="footer">
                <div className="container-fluid">
                    <Row>
                        <div className="container">
                            <Col sm={12} md={3} lg={3}>
                                <a href="https://thepeopleschurch.ca/" className="footer-logo">
                                    <img src="/Content/images/tpc-logo-transparent.png" alt="" className="footer-logo" />
                                </a>
                            </Col>

                            <Col sm={12} mdOffset={2} md={3} lg={3}>
                                <dl>
                                    <dd><Link to="/legal">Legal/Privacy</Link></dd>
                                    <dd><Link to="/terms">Terms of Use</Link></dd>
                                </dl>
                            </Col>

                            <Col sm={12} mdOffset={2} md={2} lg={2}>
                                <dl>
                                    <dd><Link to="/about">About Us</Link></dd>
                                    <dd><Link to="/contact">Find/Contact Us</Link></dd>
                                </dl>
                            </Col>
                        </div>
                    </Row>

                    <Row style={{"textAlign":"center"}} className="footer-tpctxt">
                        <div className="container">
                            <Col lg={12}>
                                <span style={{"alignContent":"center", "color": "#ba0f6b", "fontWeight": "bold", "fontSize": "2em", "display": "block"}}>The Peoples Church</span>
                                374 Sheppard Avenue East - Toronto, Ontario M2N 3B6 - Phone: 416.222.3341
                            </Col>
                        </div>
                    </Row>

                    <Row className="footer-cright">
                        <div className="container">
                            <Col lg={5}>
                                Copyright &copy; 2019 All Rights Reserved |
                                <Link to="/terms"> Terms</Link> | <Link to="/legal"> Legal/Privacy</Link>
                            </Col>
                            <Col lg={4} lgOffset={3}>
                                <a href="https://www.facebook.com/NewcomersNetworkTPC/" target="_blank" className="social-icon si-small si-borderless nobottommargin si-facebook pull-right">
                                    <FontAwesome name="facebook-square" size="2x" className="si-facebook"/>
                                </a>
                                <a href="https://twitter.com/NewcomersTO" target="_blank" className="social-icon si-small si-borderless nobottommargin si-twitter pull-right">
                                    <FontAwesome name="twitter-square" size="2x" className="si-twitter"/>
                                </a>
                                <a href="https://vimeo.com/thepeopleschurchtoronto" target="_blank" className="social-icon si-small si-borderless nobottommargin si-vimeo pull-right">
                                    <FontAwesome name="vimeo-square" size="2x" className="si-vimeo"/>
                                </a>
                                <a href="https://www.instagram.com/TPCToronto/" target="_blank" className="social-icon si-small si-borderless nobottommargin si-instagram pull-right">
                                    <FontAwesome name="instagram" size="2x" className="si-instagram"/>
                                </a>
                            </Col>
                        </div>
                    </Row>
                </div>
            </footer>
        );
    }
}

FooterPage.propTypes = {}

export default FooterPage;