import React from 'react';
import { Alert } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';

class NNUserMessage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        
        const msgIconStyle = { "marginRight": "15px" };
        var msgClass = "nnusermessage";
        var msgIcon = '';
        var iconColor = '';

        switch (this.props.msgType) {
            case "danger":
                msgIcon = <FontAwesome fixedWidth name="exclamation-circle" style={msgIconStyle} />;
                msgClass += " msg-danger";
                iconColor = "txt-tpc-red";
                break;

            case "warning":
                msgIcon = <FontAwesome fixedWidth name="exclamation-triangle" style={msgIconStyle} />;
                msgClass += " msg-warn";
                iconColor = "txt-tpc-gold";
                break;

            default:
                msgIcon = <FontAwesome fixedWidth name="info-circle"  style={msgIconStyle} />;
                msgClass += " msg-neutral";
                iconColor = "txt-tpc-blue";
                break;
        }

        return(
            <div className={msgClass}>
                <span className={iconColor}>{msgIcon}</span>
                {this.props.message}
            </div>
        )
    }
}

export default NNUserMessage;