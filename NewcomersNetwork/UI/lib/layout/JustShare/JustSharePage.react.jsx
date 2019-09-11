import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom';
import {Grid, Row, Col, Image} from 'react-bootstrap';
import {JustShareGrid} from 'lib/JustShare';
import * as justShareActions from 'actions/justShareActions'
import LoginModal from 'lib/layout/Auth/LoginModal.react';

class JustSharePage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            hiddenGrid: true,
            mode: '',
            showLoginModal: false,
            showJsFormModal: false,
            showJsMessageModal: false,
            selectedJs: '',
        }

        this.needHelpClick = this._needHelpClick.bind(this);
        this.wantToHelpClick = this._wantToHelpClick.bind(this);
    }

    componentDidMount() {
        this.props.actions.getValuesLists();
    }

    _needHelpClick() {
        this.setState({
            hiddenGrid: false,
            mode: 'n'
        });

        var { filterData } = this.props.justShareGrid;
        this.props.actions.selectJsType('s');
        filterData.Type = 's';
        this.props.actions.loadJustShareGridData(filterData);
    }

    _wantToHelpClick() {
        this.setState ({
            hiddenGrid: false,
            mode: 's'
        });
        
        var { filterData } = this.props.justShareGrid;
        this.props.actions.selectJsType('n');
        filterData.Type = 'n';
        this.props.actions.loadJustShareGridData(filterData);
    }

    render() {
        const loginModal =  <LoginModal />;
        const headSection = <section id="page-title">
                                <Grid fluid>
                                    <Row className="bg-tpc-gold">
                                        <Image src="/Content/images/JustShare.png" responsive className="sub-head-img"/>
                                    </Row>
                                    <Row className="bg-tpc-red txt-tpc-white js-sub-header">
                                        <Col lg={12} style={{"textAlign": "center"}}>
                                            <span style={{"display": "block", "padding": "20px 20px 20px 0px", "marginBottom": "20px", "marginTop": "20px", "fontSize": "2em", "fontWeight": "bold"}}>Welcome to JustShare. Please, let us know how we can help you </span>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col lg={12} style={{"textAlign": "center", "marginTop": "30px", "marginBottom": "20px"}}>
                                            <div className="btn nn-style-btn-2 btn-brd3-r" style={{"marginRight": "20px", "marginBottom": "20px", "marginTop": "20px", "padding": "30px"}} onClick={this.needHelpClick}>
                                                <span style={{"fontSize": "2em", "fontWeight": "bold", "color": "#ba0f6b"}}> I Need Help</span>
                                            </div>
                                            <div className="btn nn-style-btn-2 btn-brd3-r" style={{"marginRight": "50px", "marginBottom": "20px", "marginTop": "20px", "padding": "30px"}} onClick={this.wantToHelpClick}>
                                                <span style={{"fontSize": "2em", "fontWeight": "bold", "color": "#ba0f6b"}}> I Want To Help</span>
                                            </div>
                                        </Col>
                                    </Row>
                                </Grid>
                            </section>;
        
        const jsGrid =  this.state.hiddenGrid ?
                        '' :
                        <JustShareGrid 
                            hidden={this.state.hiddenGrid} 
                            mode={this.state.mode}
                            loginClick={this.loginClick}
                            addJsClick={this.addJustShareClick}
                            jsBtnClick={this.jsBtnClick}
                            showFilters={true}
                            filters={this.props.valuesLists} />;

        const contentSection =  <section id="content" style={{"background": "#efefef"}}>
                                    <div className="content-wrap" style={{"padding": "20px 0 !important"}}>
                                        <div className="container fluid clearfix">
                                            {jsGrid}
                                        </div>
                                    </div>
                                </section>;
        return( 
            <div>
                {headSection}
                {contentSection}
                {loginModal}
            </div>
        );
    }
};

JustSharePage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {  
    return { 
        valuesLists: state.forms.valuesLists,
        justShareGrid: state.justShare.justShareGrid
    };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(justShareActions, dispatch) };
}

JustSharePage = withRouter(JustSharePage);
export default connect(mapStateToProps, mapDispatchToProps)(JustSharePage);