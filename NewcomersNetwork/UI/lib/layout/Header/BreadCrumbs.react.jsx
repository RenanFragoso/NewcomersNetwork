import React from 'react';
import Link from 'react-router-dom';
import PropTypes from 'prop-types';

class BreadCrumbs extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return  <ol className="breadcrumb">
                </ol>;
    }
};

BreadCrumbs.propTypes = {
    links: PropTypes.array.isRequired
}

export default BreadCrumbs;