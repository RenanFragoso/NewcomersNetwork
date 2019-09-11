import React from 'react';
import { withRouter, Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import * as servicesActions from 'actions/servicesActions';
import moment from 'moment';

class ServicesPage extends React.Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { groupId } = this.props.match.params;
        this.props.actions.getServiceGroup(groupId);
    }

    render() {
        const { serviceGroup } = this.props;
        const { Services } = serviceGroup;

        const headSection = 
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Newcomers Network Services</span>
                </Col>
            </Row>;
        
        var servicesDetails = '';

        if(serviceGroup.GroupName == undefined) {
            servicesDetails =
                <Row style={{"backgroundColor": "#fff", "padding": "15px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                    <Col xs={12}>
                        <div className="loader" style={{"margin": "0px auto"}}/>
                    </Col>
                </Row>;
        }
        else {

            var servicesItems = [];
            Services.map( 
                (svc, i) => {
                    servicesItems.push(
                        <Link to={svc.Link} key={i}>
                            <Col xs={12} md={4} style={{"marginBottom": "20px"}}>
                                <img className="img-responsive" src={svc.ServiceImage} />
                                <div className="svc-float-name">
                                    <span className="svc-itm-title" style={{"fontWeight": "bold", "color": "#fec33e", "fontSize": "1.2em"}}>{svc.ServiceName}</span>
                                </div>
                            </Col>
                        </Link>);
                });
            
            servicesDetails = 
                <Row>
                    <Col xs={12}>
                        <Grid style={{"backgroundColor": "#fff", "padding": "40px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                            <Row>
                                <Col xs={12} md={6}>
                                    <img className="img-responsive" src={serviceGroup.GroupImage}  />
                                </Col>
                                <Col xs={12} md={6}>
                                    <span className="svc-name-title">
                                        <FontAwesome fixedWidth name={serviceGroup.GroupIcon} style={{"marginRight": "10px"}} />
                                        {serviceGroup.GroupName}
                                    </span>
                                    <p className="svc-description">{serviceGroup.GroupDescription}</p>
                                </Col>
                            </Row>
                            <Row style={{"marginTop": "40px"}}>
                                <Col xs={12} style={{"textAlign":"center"}}>
                                    <span className="svc-svcitms-title">Check the Newcomers Network services provided by {serviceGroup.GroupName}</span>
                                </Col>
                            </Row>
                            <Row style={{"marginTop": "40px"}}>
                                {servicesItems}
                            </Row>
                        </Grid>
                    </Col>
                </Row>;
        }

        const servicesContent =             
            <Grid fluid>
                {headSection}
                {servicesDetails}
            </Grid>;
        
        return servicesContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return { serviceGroup: state.services.serviceGroup };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(servicesActions, dispatch) };
}

ServicesPage = withRouter(ServicesPage);
export default connect(mapStateToProps, mapDispatchToProps)(ServicesPage);