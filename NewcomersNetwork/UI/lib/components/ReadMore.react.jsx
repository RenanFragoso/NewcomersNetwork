import React from 'react';
import {Grid, Row, Col, Label} from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class ReadMore extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            open: false
        }

        this.clickReadMore = this._clickReadMore.bind(this);
    }

    _clickReadMore() {
        this.setState({ open: !this.state.open })
    }

    render() {
        
        const { text, itmNum } = this.props;
        const labelContent = ( this.state.open ? 
            <span>Read Less <FontAwesome name="chevron-up" className="chevron-up" style={{"size": "1.1em", "marginLeft": "15px"}}/></span> :
            <span>Read More <FontAwesome name="chevron-down" className="chevron-down" style={{"size": "1.1em", "marginLeft": "15px"}}/></span> );

        const rdMoreContent = 
            <div className="jsTextColl" style={{"display": "block", "marginTop": "20px"}}>
                <p className="collapse" id={"jsTxt-" + itmNum} aria-expanded="false">
                    {text}
                </p>
                <Label className="bg-tpc-slate">
                    <a role="button" className="collapsed" data-toggle="collapse" href={"#jsTxt-" + itmNum} aria-expanded="false" aria-controls="collapseExample" onClick={this.clickReadMore}>
                        {labelContent}
                    </a>
                </Label>
            </div>;

        return rdMoreContent;
    }
}

export default ReadMore;