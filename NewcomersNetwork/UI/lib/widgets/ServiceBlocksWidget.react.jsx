import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import { withRouter, Link } from 'react-router-dom';
import FontAwesome from 'react-fontawesome';

class ServiceBlocksWidget extends React.Component {

    constructor(props) {
        super(props);
    }
    render() {
        var blocks = [];
        this.props.serviceBlocks.map((block, i) => {
            blocks.push(
                <Col lg={3} md={6} sm={6} xs={12} className={"svc-grp-"+(block.Position+1)} key={i}>
                    <Link to={block.Link}>
                        <div className="center svc-grp-cont">
                            <FontAwesome name={block.Icon} size="5x" style={{"color": "#fff"}} />
                            <h3 style={{"color": "#fff"}}>{block.Text}</h3>
                        </div>
                    </Link>
                </Col>
            );
        });
               
        return (
            <Row>
                {blocks}
            </Row>
        );
    }
}

export default withRouter(ServiceBlocksWidget);