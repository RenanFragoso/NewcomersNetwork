import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Row, Col, Panel } from 'react-bootstrap';
import JustShareForm from 'lib/forms/JustShareForm.react';
import * as justShareGridActions from '../../actions/justShareActions';
import FontAwesome from 'react-fontawesome';

class JustShareGridButtons extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        
        const { userInfo, justShareGrid } = this.props;
        const needBtnClass = 'panel-group' + (this.props.mode === 'n' ? '' : ' hidden');
        const suppBtnClass = 'panel-group' + (this.props.mode === 's' ? '' : ' hidden');
        const loginBtnClass = 'btn bg-tpc-red nn-style-btn-2';

        var buttonsContent = ( userInfo.logged ?
            <Row style={{"backgroundColor": "#fff", "border": "1px solid #dedede", "marginBottom": "20px", "marginTop": "20px", "paddingTop":"20px", "display": "block"}}>
                
                <Col lg={12} className={needBtnClass}>
                    <Panel eventKey="1" className="js-fpanel">
                        <Panel.Heading className="js-fpanelhead">
                            <Panel.Title className="panel-title" toggle><FontAwesome fixedWidth name="plus" className="plus" /> Add Your Need</Panel.Title>
                        </Panel.Heading>
                        <Panel.Body collapsible>
                            <JustShareForm mode='n' />
                        </Panel.Body>
                    </Panel>
                </Col>

                <Col lg={12} className={suppBtnClass}>
                    <Panel eventKey="1" className="js-fpanel">
                        <Panel.Heading className="js-fpanelhead">
                            <Panel.Title className="panel-title" toggle><FontAwesome fixedWidth name="plus" className="plus" /> Add Your Support</Panel.Title>
                        </Panel.Heading>
                        <Panel.Body collapsible>
                            <JustShareForm type='s' />
                        </Panel.Body>
                    </Panel>
                </Col>
            </Row>:
            '');

        return(buttonsContent);
    }
};

function mapStateToProps(state, ownProps) {  
    return { 
        justShareGrid: state.justShare.justShareGrid,
        userInfo: state.auth.userInfo
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareGridActions, dispatch) };
}

export default connect(mapStateToProps, mapDispatchToProps)(JustShareGridButtons);
