import React from 'react';
import EventPage from './layout/Events/EventPage.react';

class Event extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const eventId = this.props.match.params.id;

        return( 
            <EventPage { ...eventId } />
        );
    }
};

export default Event;
