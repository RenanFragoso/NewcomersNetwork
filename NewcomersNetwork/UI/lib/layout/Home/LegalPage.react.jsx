import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';

class LegalPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        
        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Legal/Privacy</span>
                </Col>
            </Row>;

            const contentSection =  
                <Row>
                    <Grid className="content-panel">
                        <Row>
                            <Col xs={12}>
                                <h4 className="txt-tpc-red sub-title" style={{"marginBottom": "10px"}}>Charitable Registration</h4>
                                <p>“The Peoples Church” and “Living Truth” are ministries and official trade names of Peoples Ministries Inc., a registered charity in Canada. Donations may be designated to The Peoples Church, Living Truth and Peoples Ministries Inc. under our charitable registration number: 13063-7051 RR0001.</p>
                                <h4 className="txt-tpc-red sub-title" style={{"marginBottom": "10px", "marginTop": "20px"}}>Scripture Quotations</h4>
                                <p>Unless otherwise noted, Scripture quotations are taken from Holy Bible, New International Version® Anglicized, NIV® Copyright © 1979, 1984, 2011 by Biblica, Inc.® Used by permission. All rights reserved worldwide.</p>
                            </Col>
                        </Row>
                    </Grid>
                </Row>;
        
        const legalContents = 
            <Grid fluid>
                {headSection}
                {contentSection}
            </Grid>;
        
        return legalContents;
    }
};

export default LegalPage;