import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import {Grid, Row, Col, Panel, OverlayTrigger, Tooltip, Modal, Label} from 'react-bootstrap';
import moment from 'moment';
import {withRouter} from 'react-router-dom';
import FontAwesome from 'react-fontawesome';
import UserJustShareWidgetMessages from './UserJustShareWidgetMessages.react';
import * as justShareActions from 'actions/justShareActions';

class UserJustShareWidgetItem extends React.Component {

    constructor(props) {
        super(props);
    }

    _finishJustShare(event) {
        this.props.actions.finishJustShare(this.state.selectedJustShare);
    }

    render() {
        const { item } = this.props;
        const typeTitle = ( item.Type == "S" ? <span className="label bg-tpc-blue" style={{"float": "right"}}>Support</span> : <span className="label bg-tpc-red" style={{"float": "right"}}>Need</span>);
        const groupStyle = { 
            "WebkitBoxShadow": "inset 5px 0px 0px 0px " + item.Category.GroupColor,
            "MozBoxShadow": "inset 5px 0px 0px 0px " + item.Category.GroupColor,
            "boxShadow": "inset 5px 0px 0px 0px " + item.Category.GroupColor
        };
        const finishTooltip =   <Tooltip placement="top" id={"FT-"+item.Id}>
                                    <strong>Finish JustShare</strong>
                                </Tooltip>;

        const typeTooltip = <Tooltip placement="top" id={"TT-"+item.Id}>
                                <strong>JustShare Type</strong>
                            </Tooltip>;

        const catTooltip =  <Tooltip placement="right" id={"TT-"+item.Id}>
                                <strong>JustShare Category</strong>
                            </Tooltip>;
        
        const msgToolTip =  <Tooltip placement="right" id={"MT-"+item.Id}>
                                <strong>JustShare Messages</strong>
                            </Tooltip>;
        
        const msgCount = (  item.TotalMessages > 0 ?
                            <Label bsStyle="danger" style={{"marginLeft" : "10px"}}>{item.TotalMessages}</Label> :
                            "" );
        const content = 
                <Row>
                    <Panel id={item.Id}>
                        <Panel.Heading style={groupStyle}>
                            <Panel.Title toggle>
                                <Row>
                                    <Col xs={7}>
                                        <span style={{"fontWeight": "bold", "fontSize": "1.2em"}}>
                                            {item.Title}
                                        </span>
                                        <span style={{"fontSize": "0.7em", "fontWeight": "bold", "display": "block"}}>
                                            Posted on: {moment(item.CreateDate).format("MM/DD/YYYY")}
                                        </span>
                                    </Col>
                                    <Col xs={5}>
                                        <OverlayTrigger placement="top" overlay={typeTooltip}>
                                            {typeTitle}
                                        </OverlayTrigger>
                                        <br />
                                        <br />
                                        <OverlayTrigger placement="right" overlay={catTooltip}>
                                            <span className="label" style={{"float": "right", "background": item.Category.GroupColor, "display": "inline-block", "marginBottom": "10px" }}>
                                                {item.Category.GroupName}
                                            </span>
                                        </OverlayTrigger>
                                    </Col>
                                </Row>
                            </Panel.Title>
                        </Panel.Heading>
                        <Panel.Collapse>
                            <Panel.Body>
                                <Row style={{"marginBottom": "20px"}}>
                                    <Col xs={2}>
                                        <img src={item.Image} className="alignleft notopmargin nobottommargin nn-avatar" style={{"width": "50px", "height": "50px"}} />
                                    </Col>
                                    <Col xs={10}>
                                        {item.Text.length > 100 ? item.Text.substr(0, 100) + ' ... ' : item.Text}
                                    </Col>
                                </Row>
                                <Row className="panel-divider" />
                                <Row style={{"marginTop": "20px"}}>
                                    <Col xs={12}>
                                        <OverlayTrigger placement="top" overlay={msgToolTip}>
                                            <div className="btn nn-style-btn-1 btn-brd3-b" role="button" data-toggle="collapse" href={"#M-" + item.Id} aria-expanded="false" aria-controls="MsgJs">
                                                <FontAwesome name="envelope" fixedWidth/>
                                                <span style={{ "fontWeight" : "bold" }}> Messages</span>
                                                {msgCount}
                                            </div>
                                        </OverlayTrigger>
                                        <OverlayTrigger placement="top" overlay={finishTooltip}>
                                            <div className="btn nn-style-btn-1 btn-brd3-r pull-right collapsed" role="button" data-toggle="collapse" href={"#F-" + item.Id} aria-expanded="false" aria-controls="finishJs" >
                                                <FontAwesome name="ban" fixedWidth/>
                                                <span style={{ "fontWeight" : "bold" }}> Finish</span>
                                            </div>
                                        </OverlayTrigger>
                                    </Col>
                                </Row>
                                <Row className="collapse panel-divider" id={"F-" + item.Id} aria-expanded="false" style={{"marginTop": "20px", "padding": "20px"}}>
                                    <Col xs={12} style={{"alignItems":"center", "textAlign": "center"}}>
                                        <span style={{"fontWeight": "bold", "display": "block", "marginBottom": "10px"}}>Really Finish this JustShare?</span>
                                        <div className="btn nn-style-btn-1 btn-brd3-r" data-jsid={btoa(item.Id)} style={{"marginRight": "20px"}}>
                                            <span style={{ "fontWeight" : "bold" }}> Yes</span>
                                        </div>
                                        <div className="btn nn-style-btn-1 btn-brd3-b" role="button" data-toggle="collapse" href={"#F-" + item.Id} aria-expanded="false" aria-controls="finishJs">
                                            <span style={{ "fontWeight" : "bold" }}> No</span>
                                        </div>
                                    </Col>                                            
                                </Row>                                
                                <Row className="collapse panel-divider" id={"M-" + item.Id} aria-expanded="false" style={{"marginTop": "20px", "padding": "20px"}}>
                                    <Col xs={12} style={{"alignItems":"center", "textAlign": "center"}}>
                                        <span style={{"fontWeight": "bold", "display": "block", "marginBottom": "10px"}}>JustShare Messages</span>
                                        <UserJustShareWidgetMessages messages={item.Messages}/>
                                    </Col>                                            
                                </Row>
                            </Panel.Body>
                        </Panel.Collapse>
                    </Panel>
                </Row>;

        return content;
   }
};

function mapStateToProps(state, ownProps) {  
    return { 
        userJustShares: state.justShare.userJustShares
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

UserJustShareWidgetItem = withRouter(UserJustShareWidgetItem);
export default connect(mapStateToProps, mapDispatchToProps)(UserJustShareWidgetItem);