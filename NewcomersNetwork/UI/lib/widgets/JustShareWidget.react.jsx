import React from 'react';
import { Row, Grid } from 'react-bootstrap';
import { withRouter } from 'react-router-dom';
import JustShareCategory from './JustShareCategory.react';

class JustShareWidget extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        var justShare = [];
        if(this.props.justShare.Categories)
        {
            this.props.justShare.Categories.map((category, i) => {
                justShare.push(<JustShareCategory category={category} key={i}/>);
            });
        };

        return justShare;
    }
}

export default withRouter(JustShareWidget);