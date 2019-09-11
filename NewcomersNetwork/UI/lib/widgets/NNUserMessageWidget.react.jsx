import React from 'react';
import { connect } from 'react-redux';
import NNUserMessage from './NNUserMessage.react';

class NNUserMessageWidget extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        var messages = [];
        const { userAdm } = this.props;
        const userMessages = (userAdm.userMessages && userAdm.userMessages.messages ? userAdm.userMessages.messages : []);
        userMessages.map( (item,n) => {
            messages.push(<NNUserMessage message={item.message} msgType={item.msgType} key={n}/>);
        });
        return messages;
    }
}

function mapStateToProps(state, ownProps) {  
    return { 
        userAdm: state.userAdm 
    };
}

export default connect(mapStateToProps)(NNUserMessageWidget);