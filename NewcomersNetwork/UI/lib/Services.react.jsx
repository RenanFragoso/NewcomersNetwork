import React from 'react';
import ServicesPage from './layout/Services/ServicesPage.react';

class Services extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        const groupId = this.props.match.params.id;

        return( 
            <ServicesPage />
        );
    }
};

export default Services;
