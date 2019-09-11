import React from 'react';
import {Grid, Row, Col, Label} from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class JsFilterBtn extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            selected: true
        }

        this.clickFilter = this._clickFilter.bind(this);
    }

    componentDidUpdate(prevProps, prevState) {
        const { category } = this.props;
        const selected = (this.props.filterArray.find((itm) => itm === category.Id) !== undefined);
        if (prevState.selected !== selected){
            this.setState({ selected });
        }
    }

    _clickFilter() {
        this.setState({ selected: !this.state.selected });
        if ( this.props.clickAction ) {
            this.props.clickAction(this.props.category.Id);
        }
    }

    render() {
        const { category } = this.props;
        const iconBtn = ( this.state.selected ? <FontAwesome name="check-square" /> : <FontAwesome name="square"/>);
        const clsName = ( this.state.selected ? "filtersBtn" : "filtersBtn unselected");
        const rdMoreContent = 
            <Label className={clsName} style={{"color": category.TextColor, "background": category.GroupColor}} onClick={this.clickFilter}>
                {iconBtn} {category.GroupName}
            </Label>;

        return rdMoreContent;
    }
}

export default JsFilterBtn;