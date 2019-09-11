import React from 'react';
import { Switch, Route, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

import Home from './Home.react';
import About from './About.react';
import Legal from './Legal.react';
import Terms from './Terms.react';
import Contact from './Contact.react';
import Event from './Event.react';
import Events from './Events.react';
import JustShare from './JustShare.react';
import JustShareDetails from './JustShareDetails.react';
import Login from './Login.react';
import SignUp from './SignUp.react';
import UserProfile from './UserProfile.react';
import ForgotPassword from './ForgotPassword.react';
import ResetPassword from './ResetPassword.react';
import Service from './Service.react';
import Services from './Services.react';
import ConfirmAccount from './ConfirmAccount.react';
import NotFound from './NotFound.react';
import AccountCreated from './AccountCreated.react';

class Main extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return( 
            <main>
                <Switch>
                    <Route exact path='/' component={Home}/>
                    <Route path='/about' component={About}/>
                    <Route path='/accountcreated' component={AccountCreated}/>
                    <Route path='/confirmaccount/:accountConfirmation' component={ConfirmAccount}/>
                    <Route path='/contact' component={Contact}/>
                    <Route path='/events' component={Events}/>
                    <Route path='/event/:eventId' component={Event}/>
                    <Route path='/forgot' component={ForgotPassword}/>
                    <Route path='/legal' component={Legal}/>
                    <Route path='/login' component={Login}/>
                    <Route exact path='/justshare' component={JustShare}/>
                    <Route path='/justshare/:id' component={JustShareDetails}/>
                    <Route path='/resetpassword/:confirmation' component={ResetPassword}/>
                    <Route path='/services/:groupId' component={Services}/>
                    <Route path='/service/:serviceId' component={Service}/>
                    <Route path='/signup' component={SignUp}/>
                    <Route path='/terms' component={Terms}/>
                    <Route path='/userprofile' component={UserProfile}/>
                    <Route path="*" component={NotFound} />
                </Switch>
            </main>
        );
    }
}

function mapStateToProps(state, ownProps) {  
    return {};
}

function mapDispatchToProps(dispatch) {
    return {};
}

Main = connect(mapStateToProps, mapDispatchToProps)(Main);
export default withRouter(Main);