import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col} from 'react-bootstrap';
import JustShareGridFilter from './JustShareGridFilter.react';
import JustShareGridItems from './JustShareGridItems.react';
import JustShareGridPagination from './JustShareGridPagination.react';
import JustShareGridButtons from './JustShareGridButtons.react';
import * as justShareGridActions from '../../actions/justShareActions';

class JustShareGrid extends React.Component {

    constructor(props) {
        super(props);
        
        this.state = {
            loadedData: false,
            itemsPerPage: 10,
            currPage: 1,
            selectedFilters: [],
            newSearchTriggered: false
        }
    }

    render() {
        var { justShareGrid, mode, userInfo } = this.props;
        var jsGridClass = justShareGrid.status ? '' : 'hidden';
        var loaderClass = justShareGrid.status ? 'hidden loader' : 'loader';

        const loader =  <Grid id="ClsWrapper" style={{"margin": "10px 0 20px 0"}}>
                            <div className={loaderClass} style={{ "margin": "0px auto" }}></div>
                        </Grid>;

        const jsFilters =   this.props.showFilters ? 
                                <JustShareGridFilter /> :
                                '';

        const jsGridItems = justShareGrid.status ? <JustShareGridItems items={justShareGrid.items} /> : loader;

        const jsPagination = <JustShareGridPagination />;
                                
        const jsGridButtons = <JustShareGridButtons mode={mode} />;

        const gridContent = <Grid id="ClsWrapper" style={{"margin": "10px 0 20px 0"}}>
                                {jsFilters}
                                {jsGridItems}
                                {jsPagination}
                                {jsGridButtons}
                            </Grid>;

        //var content = justShareGrid.status ? gridContent : loader; 

        return gridContent;
    }
};

JustShareGrid.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        justShareGrid: state.justShare.justShareGrid
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareGridActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(JustShareGrid);