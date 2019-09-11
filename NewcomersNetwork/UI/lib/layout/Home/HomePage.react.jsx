import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter, Link } from 'react-router-dom';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import { JustShareWidget, ServicesWidget, EventsWidget, TestimonialsWidget, SocialMediaWidget } from '../../widgets';
import { NNCarousel } from '../../widgets';
import * as homePageActions from '../../../actions/homePageActions';

class HomePage extends React.Component {

    constructor(props) {
        super(props);

        this.goToJsPage = this._goToJsPage.bind(this);
        this.goToEventsPage = this._goToEventsPage.bind(this);
    }

    componentDidMount() {
        this.props.actions.loadBannersWidget();
        this.props.actions.loadSBlocksWidget();
        this.props.actions.loadJustShareWidget();
        this.props.actions.loadServicesWidget();
        this.props.actions.loadEventsWidget();
        this.props.actions.loadTestimonialsWidget();
    }

    _goToJsPage() {
        this.props.history.push("/justshare");
    }

    _goToEventsPage() {
        this.props.history.push("/events");
    }

    render() {
        // Slider
        const bannerSlider = <Row>
                                <Grid fluid className="nopadding">
                                    <Row>
                                        <NNCarousel slides={this.props.homePageContent.banners}/>
                                    </Row>
                                </Grid>
                            </Row>;

        // Services content
        /*
        const servicesContent = <Row>
                                    <Grid fluid>
                                        <Row>
                                            <Col lg={12} className="cat-banner bg-tpc-blue">
                                                <span className="banner-text txt-tpc-white" style={{"display": "block"}}>Newcomers Network</span>
                                                <span className="banner-text txt-tpc-white">Compassion. Connection. Community.</span>
                                            </Col>
                                        </Row>
                                        <Row className="svc-content">
                                            <div className="container" >
                                                <ServicesWidget services={this.props.homePageContent.serviceBlocks} />
                                            </div>
                                        </Row>
                                    </Grid>
                                </Row>;
        */
        const servicesContent = 
            <React.Fragment>
                <Row>
                    <ServicesWidget services={this.props.homePageContent.serviceBlocks} />
                </Row>
                <Row className="bg-tpc-white">
                    <Grid>
                        <Row>
                            <Col lg={12} className="cat-banner">
                                <span className="banner-text txt-tpc-blue" style={{"display": "block"}}>Newcomers Network</span>
                                <span className="banner-text txt-tpc-blue">Compassion. Connection. Community.</span>
                            </Col>
                        </Row>
                        <Row className="banner-sub">
                            <Col xs={12} md={4}>
                                <FontAwesome name="home" size="3x" className="txticon" />
                                <span className="subtxt">
                                    We are here to offer kindness and compassionate support. We understand that leaving your home country and arriving in Canada is a difficult transition — but it can be an exciting one too.
                                </span>
                            </Col>

                            <Col xs={12} md={4}>
                                <FontAwesome name="plus-square" size="3x" className="txticon" />
                                <span className="subtxt">
                                    We can help you connect to practical resources you need to establish your new life in Canada.
                                </span>
                            </Col>

                            <Col xs={12} md={4}>
                                <FontAwesome name="handshake-o" size="3x" className="txticon" />
                                <span className="subtxt">
                                    We would be pleased to introduce you to the opportunity for friendship with other internationals and Canadians who are part of our community.
                                </span>
                            </Col>

                        </Row>


                    </Grid>
                </Row>
            </React.Fragment>;

        // JustShare content
        const justShareContent = <Row>
                                    <Grid fluid>
                                        <Row>
                                            <Link to="/justshare">
                                                <Col lg={12} className="cat-banner bg-tpc-gold jsbanner">
                                                    <span className="title">JustShare</span>
                                                </Col>
                                            </Link>
                                        </Row>
                                    </Grid>
                                    <Grid>
                                        <Row className="js-subtxt">
                                            <Col xs={12}>
                                                <span>
                                                    JustShare is our channel that connects contributors and newcomers. Post articles you want to give away, or browse the list of available items to acquire things you need as you settle in your new community. 
                                                </span>
                                            </Col>
                                        </Row>

                                        <Row className="js-widget-content">
                                            <JustShareWidget justShare={this.props.homePageContent.justShare}/>
                                        </Row>

                                        <Row className="js-widget-content">
                                            <div className="container" >
                                                <Col lg={4} lgOffset={4} md={4} mdOffset={4} sm={12} xs={12} style={{"textAlign": "center", "padding": "15px"}} className="btn nn-style-btn-2 btn-brd3-r" onClick={this.goToJsPage}>
                                                    <span style={{"fontSize": "3em", "fontWeight": "bold", "color": "#ba0f6b"}}>More</span>
                                                </Col>
                                            </div>
                                        </Row>
                                    </Grid>
                                </Row>;
        // Events content
        const eventsContent = <Row>
                                <Grid fluid>
                                    <Row>
                                        <Col lg={12} className="cat-banner bg-tpc-red">
                                            <span className="nn-message" style={{"fontSize": "32px", "fontWeight": "bold", "color": "#fff"}}>Stay Tuned to the Newcomers Network Special Events</span>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col xs={12}>
                                            <EventsWidget events={this.props.homePageContent.events}/>
                                        </Col>
                                    </Row>

                                    <Row>
                                        <Col lg={4} lgOffset={4} md={4} mdOffset={4} sm={12} xs={12} style={{"textAlign": "center", "marginTop": "40px", "marginBottom": "40px", "padding": "15px"}} className="btn nn-style-btn-2 btn-brd3-b" onClick={this.goToEventsPage}>
                                            <span style={{"fontSize": "3em", "fontWeight": "bold"}}>More Events</span>
                                        </Col>
                                    </Row>

                                </Grid>
                            </Row>;

        // Testimonials content
        const testimonialsContent = <Row>
                                        <Grid fluid>
                                            <Row>
                                                <Col lg={12} className="cat-banner bg-tpc-lime">
                                                    <span className="banner-text">A safe Community for you to engage in.</span>                                            
                                                </Col>
                                            </Row>
                                            <Row className="tstmn-content">
                                                <div className="container" >
                                                    <Col xs={12} sm={12} md={5} lg={5}>
                                                    </Col>
                                                    <Col xs={12} sm={12} md={7} lg={7}>
                                                        <TestimonialsWidget testimonials={this.props.homePageContent.testimonials}/>
                                                    </Col>
                                                </div>
                                            </Row>
                                        </Grid>
                                    </Row>;

        // Social Media content
        const socialMediaContent =  <Row>
                                        <Grid fluid>
                                            <Row>
                                                <Col lg={12} className="cat-banner bg-tpc-gold" style={{"textAlign": "center", "fontWeight": "bold"}}>
                                                    <span className="banner-text">Follow us on the Social Media </span>
                                                </Col>
                                            </Row>
                                            <Row className="media-content">
                                                <SocialMediaWidget />
                                            </Row>
                                        </Grid>
                                    </Row>;
        return( 
            <Grid fluid>
                {bannerSlider}
                {servicesContent}
                {justShareContent}
                {eventsContent}
                {testimonialsContent}
                {socialMediaContent}
            </Grid>
        );
    }
}

function mapStateToProps(state, ownProps) {  
    return { homePageContent: state.homePage };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(homePageActions, dispatch) };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(HomePage));