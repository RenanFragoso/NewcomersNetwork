import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function modalReducer(state = initialState.modal, action) {
    var newState;
    switch (action.type) {

        // LOGIN
        case types.MODAL_LOGIN_SHOW:
            newState = Object.assign(   {}, 
                                        state, 
                                        { login: { show: true } 
                                    });
            return newState;        
        
        case types.MODAL_LOGIN_HIDE:
            newState = Object.assign(   {}, 
                                        state, 
                                        { login: { show: false } 
                                    });
            return newState;

        case types.AUTH_RECEIVED_LOGINSUCCESS:
            //Hides the modal when login success
            newState = Object.assign(   {}, 
                state, 
                { login: { show: false } 
            });
            
            return newState;

        default:
            return state;
    }
};