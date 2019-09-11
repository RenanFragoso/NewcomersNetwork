import React from 'react';
import EventsPage from './layout/Events/EventsPage.react';

class Events extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <EventsPage />
        );
    }
};

export default Events;
