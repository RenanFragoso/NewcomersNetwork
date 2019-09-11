import * as types from 'actions/AdminUi/actionTypes';
import initialState from './initialState';

export default function authReducer(state = initialState.auth, action) {
    
    var newState;
    
    switch (action.type) {

        case types.AUTH_RECEIVED_USERINFO:
            newState = Object.assign(   {}, 
                                        state,
                                        { 
                                            userInfo: Object.assign({}, action.userInfo, { logged: true })
                                        });
            break;

        default:
            return state;
    }
    
    return newState;
}