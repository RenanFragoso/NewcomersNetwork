import { combineReducers } from 'redux';
import { reducer as reduxFormReducer } from 'redux-form';
import auth from './authReducer';
import { routerReducer } from 'react-router-redux';

const rootReducer = combineReducers({
    auth,
    form: reduxFormReducer,
    router: routerReducer
});

export default rootReducer;