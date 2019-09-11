import React from 'react';
import {Grid, Row, Col, Panel, OverlayTrigger, Tooltip, Modal} from 'react-bootstrap';
import moment from 'moment';
import {withRouter} from 'react-router-dom';
import FontAwesome from 'react-fontawesome';
import UserJustShareWidgetMessage from './UserJustShareWidgetMessage.react'

class UserJustShareWidgetMessages extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { messages } = this.props;
        const messagesContent = messages.map((msg, msgNum) => {
            var resultArray = [];
            resultArray.push(<UserJustShareWidgetMessage message={msg} msgNum={msgNum} />);          
            msg.Responses.map( (subMsg, subMsgNum) => resultArray.push(<UserJustShareWidgetMessage message={subMsg} msgNum={subMsgNum} subMessage />));
            return resultArray;
        });

        const content = 
                <Grid fluid>
                    {messagesContent}
                </Grid>;

        return content;
   }
};

export default withRouter(UserJustShareWidgetMessages);