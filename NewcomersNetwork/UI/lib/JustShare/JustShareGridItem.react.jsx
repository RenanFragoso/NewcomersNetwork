import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col, Label} from 'react-bootstrap';
import moment from 'moment';
import {withRouter} from 'react-router-dom';
import FontAwesome from 'react-fontawesome';
import ReadMore from 'lib/components/ReadMore.react';
import * as justShareActions from '../../actions/justShareActions';
import * as modalActions from '../../actions/modalActions';
import JustShareMessageForm from 'lib/forms/JustShareMessageForm.react';

class JustShareGridItem extends React.Component {

    constructor(props) {
        super(props);

        this.justShareBtnClick = this._justShareBtnClick.bind(this);
    }

    _justShareBtnClick(event) {
        this.props.jsBtnClick(event.currentTarget.attributes["data-jsid"].nodeValue);
    }

    render() {
        const { item, itmNum, userInfo, modalActions } = this.props;
        const typeTitle = ( item.Type == "S" ? <span className="label pull-right bg-tpc-blue">Support</span> : <span className="label pull-right bg-tpc-red">Need</span>);
        const btnText = ( item.Type == "S" ? "I'm Interested On This Help" : "I Can Help" );
        const jsMsgFormContent = (userInfo.logged ? 
            <Row>
                <Col xs={12} className="pull-right">
                    <div className="pull-right">
                        <div    role="button" 
                                className="btn nn-style-btn-2 btn-brd3-b collapsed" 
                                data-toggle="collapse" 
                                href={"#jsMsgFrm-" + itmNum} 
                                aria-expanded="false" 
                                aria-controls="msgFormCollapse"
                                style={{"marginBottom": "10px", "marginTop": "10px", "padding": "15px"}} >
                            <span style={{"fontSize": "1.2em", "fontWeight": "bold"}}>
                                {btnText}
                            </span>
                        </div>
                    </div>
                </Col>
                <Col xs={12} style={{"textAlign": "center"}}>
                    <div className="collapse" id={"jsMsgFrm-" + itmNum} aria-expanded="false">
                            <JustShareMessageForm  form={"jsMsgForm-"+itmNum} jsId={item.Id}/>
                    </div>
                </Col>
            </Row> :
            <Row>
                <Col xs={12} className="pull-right">
                    <div className="pull-right" onClick={modalActions.showLoginModal}>
                        <div className="btn nn-style-btn-2 btn-brd3-r" style={{"marginBottom": "10px", "marginTop": "10px", "padding": "15px"}} >
                            <span style={{"fontSize": "1.2em", "fontWeight": "bold"}}>
                                Login to participate
                            </span>
                        </div>
                    </div>
                </Col>
            </Row>);
        const formContent = (item.SameOwnership ? '' : jsMsgFormContent);
        const jsGridItem =  <Row style={{"background": "#fff", "padding": "15px", "border": "1px solid #dedede"}} key={itmNum}>
                                <Grid fluid>
                                    <Row>
                                        <Col lg={3} lgPush={9} className="pull-rigth">
                                            <FontAwesome name="ellipsis-v" className="pull-right" style={{"size": "1.1em", "marginLeft": "20px"}}/> 
                                            {typeTitle}
                                        </Col>
                                        <Col lg={2} lgPull={3}>
                                            <a className="nn-nodecor" data-jsid={btoa(item.Id)} href="#" onClick={this.goToDetails} >
                                                <img src={item.Image} className="alignleft notopmargin nobottommargin nn-avatar" style={{"width": "150px", "height": "150px"}} />
                                            </a>
                                        </Col>
                                        <Col lg={7} lgPull={3}>
                                            <div>
                                                <span style={{"fontWeight": "bold", "fontSize": "2em", "display": "block"}}>{item.Title}</span>
                                                <span style={{"fontSize": "0.8em", "fontWeight": "bold", "display": "inline-block"}}>Posted on: {moment(item.CreatedDate).format("MM/DD/YYYY")}</span>
                                                <span className="label" style={{"marginLeft": "15px", "background": item.oCategory.GroupColor, "display": "inline-block", "color": item.oCategory.TextColor }}>{item.oCategory.GroupName}</span>
                                            </div>
                                            <ReadMore 
                                                text={item.Text}
                                                itmNum={itmNum} />
                                        </Col>
                                    </Row>
                                    {formContent}
                                </Grid>
                            </Row>;
        return jsGridItem;
    }    
};

function mapStateToProps(state, ownProps) {  
    return {    
        userInfo: state.auth.userInfo
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(justShareActions, dispatch),
        modalActions: bindActionCreators(modalActions, dispatch)
    };
}

JustShareGridItem = withRouter(JustShareGridItem);
export default connect(mapStateToProps, mapDispatchToProps)(JustShareGridItem);
