import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Link, IndexLink } from 'react-router';

import HomePage from './layout/Home/HomePage.react';

class Home extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <HomePage />
        );
    }
}

Home.propTypes = {}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return {};
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);