import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col} from 'react-bootstrap';

class JustShareDetails extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.props.actions.loadJustShareDetails(this.props.id);
    }

    render() {

        var detailsContent = <Grid></Grid>;

        return(
            detailsContent
        );
    
    }
};

export default JustShareDetails;