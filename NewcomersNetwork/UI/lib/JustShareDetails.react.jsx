import React from 'react';
import JustShareDetailsPage from './layout/JustShare/JustShareDetailsPage.react';

class JustShareDetails extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const justShareId = this.props.match.params.id;
        
        return( 
            <JustShareDetailsPage id={justShareId}/>
        );
    }
};

export default JustShareDetails;
