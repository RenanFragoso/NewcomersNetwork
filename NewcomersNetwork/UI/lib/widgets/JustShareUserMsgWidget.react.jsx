import React from 'react';
import { Grid, Row, Col, Label } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as justShareActions from 'actions/justShareActions';

class JustShareUserMsgWidget extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            offset: 10
        }
    }

    componentWillMount() {
        //this.props.actions.getUserJustShare(this.state.page, this.state.offset);
    }

    render() {
        //const { userJustShares } = this.props;
        //const justShares = userJustShares.justShareWidget.UserJustShare || [];
        //const userJusrSharesItems = justShares.map((item,n) => <UserJustShareWidgetItem item={item} key={n} />);

        return (
            <Grid fluid>
                <Row className="usrdsh-subt">
                    <Col lg={12}>
                        Your Messages
                    </Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        Received messages
                    </Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        ...
                    </Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        Sent messages
                    </Col>
                </Row>
                <Row className="usrdsh-content">
                    <Col lg={12}>
                        ...
                    </Col>
                </Row>
            </Grid>
        );
    }
}

JustShareUserMsgWidget.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        //userJustShares: state.justShare.userJustShares
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(JustShareUserMsgWidget);