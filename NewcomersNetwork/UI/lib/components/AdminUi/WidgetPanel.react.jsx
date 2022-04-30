import React from 'react';
import { withRouter } from 'react-router-dom';
import { Grid, Row, Col } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class WidgetPanel extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { children, icon, header, id, collapsible, defaultopen } = this.props;
        const contentClass = ( collapsible ? "collapse wdgtcontent" : "wdgtcontent") + (collapsible && defaultopen ? " in" : "");
        const collapseBtn = ( collapsible ? { role: "button", "data-toggle": "collapse", href: "#"+id } : {} );

        const widgetPanelContent = 
            <Grid fluid className="wdgtpanel">
                <Row className="panelheader" {...collapseBtn}>
                    <Col xs={12} className="nopadding">
                        <div className="pheadicon"><FontAwesome name={icon}/></div>
                        <div className="pheadtitle">{header}</div>
                    </Col>
                </Row>
                <Row className={contentClass} id={id}>
                    <Col xs={12}>
                        {children}
                    </Col>
                </Row>
            </Grid>
        
        return widgetPanelContent;
    }
};

export default withRouter(WidgetPanel);