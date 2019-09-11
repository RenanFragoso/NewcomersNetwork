import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col, Panel, Pagination} from 'react-bootstrap';
import * as justShareGridActions from '../../actions/justShareActions';

class JustShareGridPagination extends React.Component {

    constructor(props) {
        super(props);

        this.changePage = this._changePage.bind(this);
        this.goToFirst = this._goToFirst.bind(this);
        this.goToLast = this._goToLast.bind(this);
        this.goOneUp = this._goOneUp.bind(this);
        this.goOneDown = this._goOneDown.bind(this);
    }

    _changePage(event) {
        const selected = event.target['dataset'].id;
        var { filterData, pagination } = this.props.justShareGrid;
        filterData.Page = selected;
        this.props.actions.loadJustShareGridData(filterData);
    }

    _goToFirst() {
        var { filterData } = this.props.justShareGrid;
        filterData.Page = 1;
        this.props.actions.loadJustShareGridData(filterData);
    }

    _goToLast() {
        var { filterData, pagination } = this.props.justShareGrid;
        filterData.Page = pagination.TotalPages;
        this.props.actions.loadJustShareGridData(filterData);
    }

    _goOneUp() {
        var { filterData, pagination } = this.props.justShareGrid;
        var goToPage = ( (pagination.CurrentPage + 1) <= pagination.TotalPages ? pagination.CurrentPage + 1 : pagination.TotalPages);
        filterData.Page = goToPage;
        this.props.actions.loadJustShareGridData(filterData);
    }

    _goOneDown() {
        var { filterData, pagination } = this.props.justShareGrid;
        var goToPage = ( (pagination.CurrentPage - 1) >= 1 ? pagination.CurrentPage - 1 : 1);
        filterData.Page = goToPage;
        this.props.actions.loadJustShareGridData(filterData);
    }

    render() {
        
        const { justShareGrid } = this.props;
        const { pagination } = justShareGrid;

        var paginationButtons = [];
        for( var i=1; i <= pagination.TotalPages; i++) {
            //<Pagination.Ellipsis />
            paginationButtons.push(<Pagination.Item key={i} id={i} active={i === pagination.CurrentPage} onClick={this.changePage} data-id={i}>{i}</Pagination.Item>);
        }

        const paginationContent = 
            <Row className="justify-content-md-center">
                <Col md={1}></Col>
                <Col className="col-md-auto" style={{"textAlign": "center"}}> 
                    <Pagination bsSize="large">
                        <Pagination.First onClick={this.goToFirst} disabled={pagination.CurrentPage === 1}/>
                        <Pagination.Prev onClick={this.goOneDown} disabled={pagination.CurrentPage === 1}/>
                            {paginationButtons}
                        <Pagination.Next onClick={this.goOneUp} disabled={pagination.CurrentPage === pagination.TotalPages}/>
                        <Pagination.Last onClick={this.goToLast} disabled={pagination.CurrentPage === pagination.TotalPages}/>
                    </Pagination>
                </Col>
                <Col md={1}></Col>
            </Row>;

        return(paginationContent);
    }
};

function mapStateToProps(state, ownProps) {  
    return { 
        justShareGrid: state.justShare.justShareGrid
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareGridActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(JustShareGridPagination);
