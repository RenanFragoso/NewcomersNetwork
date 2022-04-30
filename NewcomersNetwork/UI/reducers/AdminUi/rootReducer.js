import { combineReducers } from 'redux';
import { reducer as reduxFormReducer } from 'redux-form';
import auth from './authReducer';
import users from './usersReducer';
import valuesLists from './valuesListsReducer';
import { routerReducer } from 'react-router-redux';

const rootReducer = combineReducers({
    auth,
    users,
    valuesLists,
    forms: reduxFormReducer,
    router: routerReducer
});

export default rootReducer;