import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import ResetPasswordForm from 'lib/forms/ResetPasswordForm.react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as usrAdmActions from 'actions/usrAdmActions';
import { withRouter } from 'react-router-dom';

class ResetPasswordPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const confirmCode = this.props.match.params.confirmation;
        const headSection =
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">Password Recovery</span>
                </Col>
            </Row>;

        const resetPasswordDetails =
            <Row>
                <Col xs={12}>
                    <Grid style={{"backgroundColor": "#fff", "padding": "40px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                        <Row>
                            <Col lg={12} style={{"textAlign": "center"}}>
                                <ResetPasswordForm code={confirmCode} />
                            </Col>
                        </Row>
                    </Grid>
                </Col>
            </Row>;
        
        const resetPasswordContent =
            <Grid fluid>
                {headSection}
                {resetPasswordDetails}
            </Grid>;
        
        return resetPasswordContent;
    }
};

ResetPasswordPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { auth: state.auth };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(usrAdmActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(ResetPasswordPage));