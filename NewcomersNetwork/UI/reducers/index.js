import { combineReducers } from 'redux';
import { reducer as reduxFormReducer } from 'redux-form';
import auth from './authReducer';
import homePage from './homePageReducer';
import justShare from './justShareReducer';
import events from './eventsReducer';
import services from './servicesReducer';
import forms from './formsReducer';
import userAdm from './userAdmReducer';
import modal from './modalReducer'
import { routerReducer } from 'react-router-redux';

const rootReducer = combineReducers({
    auth,
    homePage,
    justShare,
    events,
    services,
    forms,
    userAdm,
    modal,
    form: reduxFormReducer,
    router: routerReducer
});

export default rootReducer;