import React from 'react';
import { Link, withRouter } from 'react-router-dom';
class HeaderAvatar extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { userInfo } = this.props;
        /*
        const avatarContent =   <div id="top-account" className="dropdown">
                                    <a href="#" className="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <i className="icon-user" /><span className="nn-usrname-mnu">{userInfo.Name}</span> <i className="icon-angle-down" />
                                    </a>
                                    <ul className="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu1">
                                        <li>
                                            <img src={userInfo.Picture} className="aligncenter img-usrprofile img-thumbnail notopmargin nobottommargin" alt="Avatar" style={{"maxWidth": "84px", "display": "block"}} />
                                        </li>
                                        <li>
                                            <h5 style={{"textAlign": "center", "marginTop": "10px !important", "marginBottom": "10px !important"}}>{userInfo.Name}</h5>
                                        </li>
                                        <li role="separator" className="divider"></li>
                                        <li><a href="/userprofile"><i className="icon-user" /> Profile</a></li>
                                        {adminContent}
                                        <li role="separator" className="divider"></li>
                                        <li><a href="/logout"><i className="icon-signout" /> Logout</a></li>
                                    </ul>
                                </div>;
        */
        
        const avatarContent =   ( userInfo.logged ?  
                                    <Link to="/userprofile" className="btn nn-style-btn-2 bg-tpc-red txt-tpc-white" style={{ "textAlign": "center", "width": "100%", "marginTop": "-7.5px" }}>
                                        <span className="txt-tpc-white" style={{ "textAlign": "center", "fontSize": "1.2em", "fontWeight": "bold", "display": "block"}}>{userInfo.Name}</span>
                                        <span className="txt-tpc-white" style={{ "textAlign": "center", "fontSize": "0.8em"}}>My Account</span>
                                    </Link> :
                                    <Link to="/login" className="btn nn-style-btn-2 bg-tpc-red" style={{ "textAlign": "center", "width": "100%"}}>
                                        <span className="txt-tpc-white" style={{ "textAlign": "center", "fontSize": "1em", "fontWeight": "bold" }}>My Account</span>
                                    </Link> );
        return avatarContent;
    }
}

HeaderAvatar = withRouter(HeaderAvatar);
export default HeaderAvatar;