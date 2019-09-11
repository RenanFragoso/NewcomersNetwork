import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function userAdmReducer(state = initialState.userAdm, action) {
    var newState;
    switch (action.type) {

        case types.USRADM_ACCOUNTCONFIRM_SUCCESS:
    	        newState = Object.assign(   {}, 
                                            state, 
                                            {   accountConfirmation: 
                                                {
                                                    result: true,
                                                    status: true,
                                                    error: ""
                                                }
                                            });
            return newState;

        case types.USRADM_ACCOUNTCONFIRM_FAIL:
            newState = Object.assign(   {}, 
                                        state, 
                                        {   accountConfirmation: 
                                            {
                                                result: false,
                                                status: true,
                                                error: action.error
                                            }
                                        });
            return newState;

        case types.USRADM_REGISTER_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        {   signUpStatus: 
                                            {
                                                result: true,
                                                status: true,
                                                error: '',
                                                confirmMail: action.confirmMail
                                            }
                                        });
            return newState;

        case types.USRADM_REGISTER_FAIL:
            newState = Object.assign(   {}, 
                                        state, 
                                        {   signUpStatus: 
                                            {
                                                result: false,
                                                status: true,
                                                error: action.error,
                                                confirmEmail: ''
                                            }
                                        });
            return newState;

        case types.USRADM_CONFIRM_MSG:
            newState = Object.assign(   {}, 
                                        state, 
                                        {   signUpStatus: 
                                            {
                                                result: true,
                                                status: true,
                                                error: '',
                                                confirmEmail: ''
                                            }
                                        });
            return newState;

        case types.USRADM_USERDETAILS_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        {   
                                            userDetails: action.userDetails
                                        });
            return newState;
        
            default:
            return state;
    }

    return newState;
};