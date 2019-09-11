import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link, withRouter } from 'react-router-dom';

import * as usrAdmActions from 'actions/usrAdmActions';

class ConfirmAccountPage extends React.Component {

    constructor(props) {
        super(props);

    }

    componentWillMount() {
        this.props.actions.tryConfirmAccount({ ConfirmationData: this.props.match.params.accountConfirmation });
    }

    render() {
        const confirm = this.props.accountConfirmation;
        const resultOk = <span style={{"fontWeight": "bold"}}>
                            Account confirmation succeful. You can now 
                            <div className="btn nn-style-btn-1 btn-brd3-b" style={{"marginLeft": "5px", "marginRight": "5px"}}>
                                <Link to="/login" replace>Log-in</Link>
                            </div>
                            into Newcomers Network.
                        </span>;
        const resultError = <span style={{"fontWeight": "bold"}}>
                                Account confirmation failed. You can 
                                <div className="btn nn-style-btn-1 btn-brd3-b" style={{"marginLeft": "5px", "marginRight": "5px"}}>
                                    Re-Send
                                </div>
                                the confirmation e-mail.
                            </span>;
        const loader = <div className="loader" style={{ "margin": "0px auto" }}></div>;

        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Account Confirmation</span>
                </Col>
            </Row>;

        const confirmDetails =
            <Row>
                <Col xs={12}>
                    <Grid style={{"backgroundColor": "#fff", "padding": "40px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                        <Row>
                            <Col lg={12} style={{"textAlign": "center"}}>
                                {( 
                                    confirm.status ?
                                        (confirm.result ? resultOk : resultError) :
                                        loader
                                )}
                            </Col>
                        </Row>
                    </Grid>
                </Col>
            </Row>;
        
        const confirmAccContent =
            <Grid fluid>
                {headSection}
                {confirmDetails}
            </Grid>;
        
        return confirmAccContent;
    }
}

function mapStateToProps(state, ownProps) {  
    return { 
        accountConfirmation: state.userAdm.accountConfirmation
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

ConfirmAccountPage = withRouter(ConfirmAccountPage);
export default connect(mapStateToProps, mapDispatchToProps)(ConfirmAccountPage);
