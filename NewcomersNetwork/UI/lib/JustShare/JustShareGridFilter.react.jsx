import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col, Checkbox, ToggleButton, ToggleButtonGroup, Label } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import * as justShareGridActions from '../../actions/justShareActions';
import JsFilterBtn from 'lib/components/JsFilterBtn.react';

class JustShareGridFilter extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            filters: []
        }

        this.changeFilters = this._changeFilters.bind(this);
    }

    componentDidMount() {
        const { justShareGrid } = this.props;
        const filters = justShareGrid.categories.Itens.map((i) => i.id);
        this.setState({ filters });
    }

    _changeFilters(category) {
        var filterData = this._toggleFilter(this.state.filters, category);
        this.props.actions.loadJustShareGridData(filterData);
    }

    _toggleFilter(filters, category) {
        var selectedFilters = filters;
        var { filterData } = this.props.justShareGrid;
        if( filters.find( i => i === category ) ) {
            selectedFilters = filters.filter( (value, i) => value !== category )
        }
        else {
            selectedFilters.push(category);
        }
        this.setState({ filters: selectedFilters });
        return Object.assign(   {},
                                filterData,
                                { Categories: selectedFilters });
    }

    render() {
        var { justShareGrid } = this.props;
        var filtersBtn  = justShareGrid.categories ? justShareGrid.categories.Itens.map(
            (item, i) => {
                const itmObjct = JSON.parse(item.text);
                return <JsFilterBtn key={i} category={itmObjct} clickAction={this.changeFilters} filterArray={this.state.filters}/>;
            }):'';
        var filterContent = <Row style={{"backgroundColor": "#fff", "border": "1px solid #dedede", "marginTop": "20px", "paddingTop":"20px", "display": "block"}}>
                                <Col xs={12} className="panel-group">
                                    <div className="panel js-fpanel">
                                        <div className="panel-heading js-fpanelhead">
                                            <h4 className="panel-title">
                                                <a data-toggle="collapse" href="#collapse1">
                                                    <FontAwesome name="bars" style={{ "marginRight": "10px"}}/> 
                                                    <span style={{"fontSize": "1.2em", "fontWeight": "bold"}}>Category Filters</span>
                                                </a>
                                            </h4>
                                        </div>
        	                            <div id="collapse1" className="panel-collapse collapse" style={{"marginTop": "20px"}}>
                                            <div id="ClsFilters" className="panel-body">
                                                {filtersBtn}
                                            </div>
                                        </div>
                                    </div>
                                </Col>
                            </Row>;
        
        return filterContent;
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

export default connect(mapStateToProps, mapDispatchToProps)(JustShareGridFilter);
