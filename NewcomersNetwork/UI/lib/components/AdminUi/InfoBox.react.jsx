import React from 'react';
import { withRouter } from 'react-router-dom';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class InfoBox extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { type, size, content, icon, data} = this.props;
        
        var infoClass = "";

        switch(type) {
            case "red":
                infoClass = 'infoicon bg-tpc-red txt-tpc-white';
                break;
            case "blue":
                infoClass = 'infoicon bg-tpc-blue txt-tpc-white';
                break;
            case "gold":
                infoClass = 'infoicon bg-tpc-gold txt-tpc-black';
                break;
            case "lime":
                infoClass = 'infoicon bg-tpc-lime txt-tpc-black';
                break;
            default:
                infoClass = 'infoicon bg-tpc-slate txt-tpc-white';
        };

        const infoBoxContent = 
            <Col xs={6} md={size}>
                <Grid fluid className="infobox">
                    <Row className="nomargin inforow">
                        <Col xs={4} className={infoClass}>
                            <FontAwesome name={icon}/>
                        </Col>
                        <Col xs={8} className="infotext">
                            <span className="infodata">{data}</span>
                            <span className="infocontent">{content}</span>
                        </Col>
                    </Row>
                </Grid>
            </Col>;

        return infoBoxContent;
    }
};

export default withRouter(InfoBox);