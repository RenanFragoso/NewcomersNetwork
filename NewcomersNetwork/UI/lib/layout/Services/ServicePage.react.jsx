import React from 'react';
import { withRouter, Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import * as servicesActions from 'actions/servicesActions';
import moment from 'moment';

class ServicePage extends React.Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { serviceId } = this.props.match.params;
        this.props.actions.getServiceById(serviceId);
    }

    render() {
        const { service } = this.props;
        
        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Newcomers Network Service</span>
                </Col>
            </Row>;
        
        var serviceDetails = '';

        if(service.ServiceName == undefined) {
            serviceDetails =
                <Row style={{"backgroundColor": "#fff", "padding": "15px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                    <Col xs={12}>
                        <div className="loader" style={{"margin": "0px auto"}}/>
                    </Col>
                </Row>;
        }
        else {
            serviceDetails = 
                <Row>
                    <Col xs={12}>
                        <Grid style={{"backgroundColor": "#fff", "padding": "40px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                            <Row>
                                <Col xs={12} md={6}>
                                    <img className="img-responsive" src={service.ServiceImage}  />
                                </Col>
                                <Col xs={12} md={6}>
                                    <span className="svc-name-title">
                                        {service.ServiceName}
                                    </span>
                                    <p className="svc-description">{service.ServiceDescription}</p>
                                </Col>
                            </Row>
                        </Grid>
                    </Col>
                </Row>;
        }

        const serviceContent =             
            <Grid fluid>
                {headSection}
                {serviceDetails}
            </Grid>;
        
        return serviceContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return { service: state.services.service };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(servicesActions, dispatch) };
}

ServicePage = withRouter(ServicePage);
export default connect(mapStateToProps, mapDispatchToProps)(ServicePage);