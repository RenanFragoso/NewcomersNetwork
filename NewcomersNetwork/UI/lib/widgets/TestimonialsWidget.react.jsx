import React from 'react';
import { Col, Row } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import OwlCarousel from 'react-owl-carousel';

class TestimonialsWidget extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
        //Load testimonials
    }

    render() {

        const sliderSettings = {
            margin: 10,
            nav: false,
            responsive: {
                0:{
                    items:1
                },
                768:{
                    items:1
                },
                1024:{
                    items:1
                },
                2560: {
                    items:1
                }
            }
        };

        var testimonials = [];
        this.props.testimonials.map((testimonial, i) => {
            testimonials.push(
                <div className="oc-item" key={i}>
                    <div className="testimonial">
                        <div className="testi-image">
                            <img src={testimonial.Image} alt="Testimonails" className="nn-avatar img-thumbnail tstmn-avatar" />
                        </div>
                        <div className="testi-content">
                            <div className="testi-meta">
                                <strong>{testimonial.Author}</strong>
                                <span style={{"color": "#ba0f6b"}}>{testimonial.Title}</span>
                            </div>
                            <p>{testimonial.Testimonial}</p>
                        </div>
                    </div>
                </div>
            );
        });

        var testimonialsSlider = <div></div>;
        if(testimonials.length > 0) {
            testimonialsSlider =    <OwlCarousel {...sliderSettings} className="owl-theme">
                                        {testimonials}
                                    </OwlCarousel>;
        };

        return (
            testimonialsSlider
        );
    }
}

export default TestimonialsWidget;