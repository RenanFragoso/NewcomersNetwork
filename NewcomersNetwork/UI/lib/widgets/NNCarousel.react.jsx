import React from 'react'
import { Carousel } from 'react-bootstrap';

class NNCarousel extends React.Component {
    
    render() {
        const carouselSettings = {
            interval: 10000,
        };

        var banners = [];
        this.props.slides.map((banner, i) => {
            banners.push(   <Carousel.Item key={i}>
                                <img src={banner.Image} />
                            </Carousel.Item>);
        });

        return (
            <Carousel {...carouselSettings}>
                {banners}
            </Carousel>
        );
    }
}

export default NNCarousel;