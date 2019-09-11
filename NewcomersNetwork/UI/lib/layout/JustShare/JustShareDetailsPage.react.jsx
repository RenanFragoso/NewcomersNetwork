import React from 'react';
import {Grid, Row, Col, Image} from 'react-bootstrap';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link, withRouter } from 'react-router-dom';
import * as justShareActions from 'actions/justShareActions';
import moment from 'moment';
import FontAwesome from 'react-fontawesome';
import JustShareMessageFormModal from 'lib/layout/JustShare/JustShareMessageFormModal.react';
import LoginModal from 'lib/layout/Auth/LoginModal.react';
import * as modalActions from 'actions/modalActions';
import JustShareMessageForm from 'lib/forms/JustShareMessageForm.react';
import UserJustShareWidgetMessages from 'lib/widgets/UserJustShareWidgetMessages.react';

class JustShareDetailsPage extends React.Component {

    constructor(props) {
        super(props);
    }
    
    componentDidMount() {
       this.props.actions.loadJustShareDetails(this.props.id);
    }

    render() {
        const { modalActions, justShareDetails, userInfo } = this.props;
        const jshare = justShareDetails;
        
        if(jshare.Id == undefined) {
            return <Row style={{"backgroundColor": "#fff", "padding": "15px", "border": "1px solid #dedede", "marginBottom": "20px", "display": "block"}}>
                <Col xs={12}>
                    <div className="loader" style={{"margin": "0px auto"}}/>
                </Col>
            </Row>;
        }
        
        const headSection = 
            <Row className="sub-header" >
                <Col xs={12}>
                    <span className="txt-tpc-red">JustShare</span>
                </Col>
            </Row>;

        const typeTitle = ( jshare.Type == "S" ? <span className="label pull-right bg-tpc-blue">Support</span> : <span className="label pull-right bg-tpc-red">Need</span>);
        const btnText = ( jshare.Type == "S" ? "I'm Interested" : "I Can Help" );
        var actionButton = '';
        
        if(userInfo.logged) {
            actionButton = ( jshare.HasOwnership ? 
                '':
                <Row style={{"marginTop": "20px"}}>
                    <Col xs={12}>
                        <div className="pull-right">
                            <div    role="button" 
                                    className="btn nn-style-btn-2 btn-brd3-b collapsed" 
                                    data-toggle="collapse" 
                                    href={"#jsMsgFrm"} 
                                    aria-expanded="false" 
                                    aria-controls="msgFormCollapse"
                                    style={{"marginBottom": "10px", "marginTop": "10px", "padding": "15px"}} >
                                <span style={{"fontSize": "1.2em", "fontWeight": "bold"}}>
                                    {btnText}
                                </span>
                            </div>
                        </div>
                    </Col>
                </Row>);
        }
        else {
            actionButton =  
                <Row style={{"marginTop": "20px"}}>
                    <Col xs={12}>
                        <div className="btn nn-style-btn-2 btn-brd3-r" style={{"marginBottom": "10px", "marginTop": "10px", "padding": "15px", "float": "right"}} onClick={modalActions.showLoginModal}>
                            <span style={{"fontSize": "1.2em", "fontWeight": "bold"}}>
                                Login to Participate
                            </span>
                        </div>
                    </Col>
                </Row>;
        }

        const detailsContent =
            <Row>
                <Col xs={3}>
                    <img src={jshare.Image} className="alignleft notopmargin nobottommargin nn-avatar" style={{"width": "200px", "height": "200px"}} />
                </Col>
                <Col xs={9}>
                    <div>
                        <FontAwesome name="ellipsis-v" className="pull-right" style={{"size": "1.1em", "marginLeft": "20px", "cursor": "pointer"}}/> 
                        {typeTitle}
                        <span style={{"fontWeight": "bold", "fontSize": "2em", "display": "block"}}>{jshare.Title}</span>
                        <span style={{"fontSize": "0.8em", "fontWeight": "bold", "display": "inline-block"}}>Posted on: {moment(jshare.CreateDate).format("DD/MM/YYYY")}</span>
                        <span className="label" style={{"marginLeft": "15px", "background": jshare.Category.GroupColor, "display": "inline-block", "color": jshare.Category.TextColor }}>{jshare.Category.GroupName}</span>
                    </div>
                    <div style={{"display": "block", "marginTop": "20px"}}>{jshare.Text}</div>
                </Col>
            </Row>;

        const msgFormContent = 
            <Row className="collapse panel-divider" style={{"marginTop": "20px", "padding": "20px", "textAlign": "center"}} aria-expanded="false" id="jsMsgFrm"> 
                <Col xs={12}>
                    <JustShareMessageForm form={"jsMsgForm"} jsId={jshare.Id}/>
                </Col>
            </Row>;

        const messagesContent = ( userInfo.logged && ( jshare.Messages.length > 0 ) ?
            <Row className="panel-divider" style={{"marginTop": "20px", "padding": "20px", "textAlign": "center"}}>
                <Col xs={12}>
                    <span style={{"fontWeight": "bold", "display": "block", "marginBottom": "20px"}} className="pull-center">Your Messages</span>
                    <UserJustShareWidgetMessages messages={jshare.Messages}/>
                </Col>
            </Row> :
            '');

        const contents =  
            <Grid fluid className="bg-tpc-grey">
                {headSection}
                <Row>
                    <Col xs={12}>
                        <Grid style={{"backgroundColor": "#fff", "padding": "20px", "border": "1px solid #dedede", "marginBottom": "80px", "display": "block"}}>
                            {detailsContent}
                            {actionButton}
                            {msgFormContent}
                            {messagesContent}
                        </Grid>
                    </Col>
                </Row>
                <LoginModal />
            </Grid>;

        return contents;
    }
};

function mapStateToProps(state, ownProps) {  
    return { 
        justShareDetails: state.justShare.justShareDetails,
        userInfo: state.auth.userInfo 
    };
}

function mapDispatchToProps(dispatch) {
    return {    actions: bindActionCreators(justShareActions, dispatch),
                modalActions: bindActionCreators(modalActions, dispatch) };
}


JustShareDetailsPage = withRouter(JustShareDetailsPage);
export default connect(mapStateToProps, mapDispatchToProps)(JustShareDetailsPage);