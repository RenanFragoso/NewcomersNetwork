import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

import Header from './Header.react';
import Main from './Main.react';
import Footer from './Footer.react';

class NewcomersNetworkUI extends React.Component { 
    
    constructor(props) {
        super(props);
    }
   
    render() {

        return(
            <div className="main-body clearfix">
                <Header />
                <Main />
                <Footer />
            </div>
        );
    }
}

NewcomersNetworkUI.propTypes = {}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return {};
}

NewcomersNetworkUI = connect(mapStateToProps, mapDispatchToProps)(NewcomersNetworkUI);

export default withRouter(NewcomersNetworkUI);