import React from 'react';
import ServicePage from './layout/Services/ServicePage.react';

class Service extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const serviceId = this.props.match.params.id;

        return( 
            <ServicePage { ...serviceId } />
        );
    }
};

export default Service;
