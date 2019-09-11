import React from 'react'
import Slider from 'react-slick';

class Carousel extends React.Component {
    render() {

        const sliderSettings = {
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: true,
            centerMode: true,
            autoplay: true,
            autoplaySpeed: 10000,
            centerPadding: "0px",
            fade: true
        };

        var banners = [];
        this.props.slides.map((banner, i) => {
            banners.push(<div key={i}><img src={banner.Image}/></div>);
        });

        return (
            <Slider {...sliderSettings}>
                {banners}
            </Slider>
        );
    }
}

export default Carousel;