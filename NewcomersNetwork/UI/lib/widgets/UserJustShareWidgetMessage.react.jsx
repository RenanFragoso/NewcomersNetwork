import React from 'react';
import {Grid, Row, Col, Panel, OverlayTrigger, Tooltip, Modal} from 'react-bootstrap';
import moment from 'moment';
import {withRouter} from 'react-router-dom';
import FontAwesome from 'react-fontawesome';

class UserJustShareWidgetMessage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { message, msgNum, subMessage } = this.props;
        
        const footerContent = "On " + moment(message.Date).format("MM/DD/YYYY - h:mm a") + " " + ( message.IsOwner ? "" : "Reply" );
        const messageFrom = ( message.IsOwner ?
                                "You: " :
                                message.MessageFrom + ": " );
        const content = (   subMessage ?
                            <Row className="usradm-msgrow">
                                <Col xs={11} xsPush={1} className="usradm-msgbox">
                                    <span>{messageFrom}</span>
                                    <span>{message.Message}</span>
                                </Col>
                                <Col xs={11} xsPush={1} className="usradm-msgfootr">
                                    <span>{footerContent}</span>
                                </Col>
                            </Row> :
                            <Row className="usradm-msgrow">
                                <Col xs={12} className="usradm-msgbox">
                                    <span>{messageFrom}</span>
                                    <span>{message.Message}</span>
                                </Col>
                                <Col xs={12} className="usradm-msgfootr">
                                    <span>{footerContent}</span>
                                </Col>
                            </Row> );

        return content;
   }
};

export default withRouter(UserJustShareWidgetMessage);