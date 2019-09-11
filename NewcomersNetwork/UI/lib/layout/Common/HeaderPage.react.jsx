import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link, withRouter } from 'react-router-dom';
import HeaderAvatar from 'lib/components/HeaderAvatar.react';
import * as authActions from 'actions/authActions';

class HeaderPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            scrollAtTop: true
        };
        this.listenScroll = this.listenScroll.bind(this);
    };

    listenScroll() {
        if( window.scrollY > 100 ){
            this.setState({scrollAtTop: false});
        }
        else {
            this.setState({scrollAtTop: true});
        }
    }

    componentDidMount() {
        //window.addEventListener("scroll", this.listenScroll);
    }

    componentWillUnmount() {
        //window.removeEventListener("scroll", this.listenScroll);
    }

    render() {
        var { userInfo } = this.props;
        var headerClass = this.state.scrollAtTop ? "header" : "header-fixed";
        var loginContent =  <HeaderAvatar userInfo={userInfo} />;
        var navContent =    <nav className="navbar navbar-default">
                                <div className="container">
                                    <div className="navbar-header">
                                        <div id="logo" className="logo">
                                            <Link to="/" className="standard-logo"><img src="/Content/images/NN_Logo.png" alt="Newcomers Network" /></Link>
                                            <Link to="/" className="small-logo"><img src="/Content/images/NN_Logo.png" alt="Newcomers Network" /></Link>
                                        </div>
                                    </div>
                                    <ul className="nav navbar-nav navbar-right">
                                        <li>
                                            {loginContent}
                                        </li>
                                    </ul>
                                </div>
                            </nav>;
        return( 
            <header id="header" className={headerClass}>
                {navContent}
            </header>
        );
    }
}

function mapStateToProps(state, ownProps) {  
    return { userInfo: state.auth.userInfo };
}

function mapDispatchToProps(dispatch) {
    return { actions: bindActionCreators(authActions, dispatch) };
}

HeaderPage = withRouter(HeaderPage);
export default connect(mapStateToProps, mapDispatchToProps)(HeaderPage);