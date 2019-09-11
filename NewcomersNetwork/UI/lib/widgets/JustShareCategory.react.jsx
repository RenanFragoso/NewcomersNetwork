import React from 'react';
import { Col } from 'react-bootstrap';
import { withRouter } from 'react-router-dom';
import JustShareClassified from './JustShareClassified.react';

class JustShareCategory extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        var classifieds = [];
        if(this.props.category.Classifieds)
        {
            this.props.category.Classifieds.map( (classified, i)=> {
                classifieds.push(<JustShareClassified classified={classified} key={i}/>);
            });
        };

        const contents = 
            <Col lg={4} md={4} sm={12} xs={12}>
                <div className="jswdgt-panel">
                    <div className="jswdgt-header text-center">
                        <span style={{"color": "#00588d", "fontSize": "1.5em", "marginBottom": "20px", "fontWeight": "bold"}}>
                            {this.props.category.CategoryName}
                        </span>
                    </div>
                    <div className="jswdgt-content" style={{"minHeight": "250px", "paddingTop": "30px"}}>
                        {classifieds}
                    </div>
                </div>
            </Col>;
        
        return contents;
    }
}

export default withRouter(JustShareCategory);