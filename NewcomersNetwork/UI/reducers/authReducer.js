import * as types from '../actions/actionTypes';
import initialState from './initialState';

export default function authReducer(state = initialState.auth, action) {
    
    var newState;
    
    switch (action.type) {

        case types.AUTH_RECEIVED_USERINFO:
            newState = Object.assign(   {}, 
                                        state,
                                        { 
                                            userInfo: Object.assign({}, action.userInfo, { logged: true})
                                        });
            break;

        case types.AUTH_RECEIVED_LOGINSUCCESS:
            sessionStorage.setItem("nntoken", action.userInfo.token);
            newState = Object.assign(   {}, 
                                        state,
                                        { 
                                            userInfo: Object.assign(    {}, 
                                                                        action.userInfo, 
                                                                        { logged: true }),
                                            result: true,
                                            status: true
                                        });
            break;

        case types.AUTH_RECEIVED_LOGINERROR:
            newState = Object.assign(   {}, 
                                        state,
                                        {
                                            result: false,
                                            status: true,
                                            errorMessage: action.error
                                        });
            break;

        case types.AUTH_RECEIVED_LOGOUT:
            sessionStorage.removeItem("nntoken");
            newState = Object.assign(   {}, 
                                        state,
                                        {   userInfo: Object.assign(    {}, 
                                                                        {   UserName: '',
                                                                            Name: '',
                                                                            Title: '',
                                                                            Picture: '',
                                                                            token: '',
                                                                            Roles: [],
                                                                            logged: false }),
                                            result: false,
                                            status: true,
                                            errorMessage: ""
                                        });
            break;
            
        case types.AUTH_USERINFO_ERROR:
            sessionStorage.removeItem("nntoken");
            newState = Object.assign(   {}, 
                                        state,
                                        {
                                            result: false,
                                            status: true,
                                            errorMessage: action.error
                                        });
            break;

        case types.ERROR_MESSAGE:
            newState = Object.assign(   {}, 
                                        state,
                                        {
                                            errorMessage: action.error
                                        });
            break;

        default:
            return state;
    }
    
    return newState;
}