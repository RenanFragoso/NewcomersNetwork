import React from 'react';
import { Col, Row } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import { withRouter, Link } from 'react-router-dom';
import OwlCarousel from 'react-owl-carousel';

class ServicesWidget extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        /*
        const sliderSettings = {
            dots: true,
            infinite: false,
            speed: 500,
            slidesToShow: 4,
            slidesToScroll: 1,
            arrows: true,
            centerMode: false
        };

        <Slider {...sliderSettings}>
        */
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
                    items:2
                },
                2560: {
                    items:2
                }
            }
        };

        var services = [];
        var pushValue = 0;
        
        this.props.services.map((service, i) => {
            //pushValue = ( i == 0 ? 0 : ( (i % 2) == 0 ) ? 0 : 2 );
            services.push(
                <Col xs={12} sm={6} lg={3} className={"svc-" + service.BlockColor} key={i}>
                    <Link to={service.Link}>
                        <FontAwesome name={service.Icon} size="5x" className="svc-icon" />
                        <span className="svc-title">{service.Text}</span>
                    </Link>
                </Col>
            );
        });

        return services;
    }
}

export default withRouter(ServicesWidget);