import React from 'react';
import { Col } from 'react-bootstrap';
import { Link, withRouter } from 'react-router-dom';

class JustShareClassified extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { classified } = this.props;
        const contents = 
            <Col sm={12} lg={12} style={{"marginBottom": "20px", "cursor": "pointer"}}>
                <Link to={classified.Link}>
                    <img src={classified.Image} style={{"width": "50px", "height": "50px", "display": "inlineBlock", "float": "left", "marginRight": "10px"}} />
                    <span style={{"display": "block", "fontWeight": "bold"}}>{classified.Title}</span>
                </Link>
            </Col>;
        
        return contents;
    }
}

export default withRouter(JustShareClassified);