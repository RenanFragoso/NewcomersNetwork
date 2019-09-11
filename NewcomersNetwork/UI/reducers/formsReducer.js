import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function userAdmReducer(state = initialState.forms, action) {

    var newState = state;
    var loginForm = {};
    var jsForm = {};
    
    switch (action.type) {
        // VALUE LISTS
        case types.USRADM_USERDETAILVALUESLISTS_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { valuesLists: action.valuesLists });
            return newState;

        case types.RECEIVED_VALUELISTS_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { valuesLists: action.valuesLists });
            return newState;

        // USER DETAILS FORM
        case types.USRADM_USERDETAILSFORM_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { userDetailsForm: 
                                            Object.assign(  
                                                {},
                                                state.userDetailsForm,
                                                action.userDetailsForm
                                            )
                                        });
            return newState;

        // AVATAR UPDATE
        case types.USRADM_USERAVATARSUBMIT_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { updateAvatarForm: 
                                            Object.assign(  
                                                {},
                                                state.updateAvatarForm,
                                                action.updateAvatarForm
                                            )
                                        });
            return newState;
        
        case types.USRADM_USERAVATARSUBMIT_FAIL:
            newState = Object.assign(   {}, 
                                        state, 
                                        { updateAvatarForm: 
                                            Object.assign(  
                                                {},
                                                state.updateAvatarForm,
                                                {
                                                    errors: action.errors
                                                }
                                            )
                                        });
            return newState;

        // SIGN UP
        case types.FORM_SIGNUP_SUBMITTING:
            newState = Object.assign(   {}, 
                                        state, 
                                        { signUpForm: {
                                            status: false,
                                            result: false
                                        } });
            return newState;

        case types.FORM_SIGNUP_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { signUpForm: {
                                            status: true,
                                            result: true,
                                            errors: {},
                                            confirmMail: action.confirmMail
                                        } });
            return newState;

        case types.FORM_SIGNUP_FAIL:
            newState = Object.assign(   {}, 
                                        state, 
                                        { signUpForm: {
                                            status: true,
                                            result: false,
                                            errors: action.errors
                                        } });
            return newState;

        // LOGIN
        case types.FORM_LOGINFORM_SUBMITTING:
            loginForm = Object.assign(  {},
                                        state.loginForm,
                                        {   
                                            status: false,
                                            submitting: true
                                        });

            newState = Object.assign(   {}, 
                                        state, 
                                        { loginForm });
            return newState;

        case types.FORM_LOGINFORM_DONESUBMITTING:
            loginForm = Object.assign(  {},
                                        state.loginForm,
                                        {   status: true,
                                            submitting: false
                                        });

            newState = Object.assign(   {}, 
                                        state, 
                                        { loginForm });
            return newState;

        case types.AUTH_RECEIVED_LOGINSUCCESS:
            loginForm = Object.assign(  {},
                                        state.loginForm,
                                        { result: true });

            newState = Object.assign(   {}, 
                                        state, 
                                        { loginForm });
            return newState;

        case types.AUTH_RECEIVED_LOGINERROR:
            loginForm = Object.assign(  {},
                                        state.loginForm,
                                        { result: false });    

            newState = Object.assign(   {}, 
                                        state, 
                                        { loginForm });
            return newState;

        case types.FORM_LOGINFORM_CLEANERROR:
            loginForm = Object.assign(  {},
                                        state.loginForm,
                                        { result: true });    

            newState = Object.assign(   {}, 
                                        state, 
                                        { loginForm });
            return newState;

        // PASSWORD
        case types.FORM_PASSWDCHANGE_SUBMITTING:
            newState = Object.assign(   {}, 
                                        state, 
                                        { changePasswordForm: {
                                            status: false,
                                            result: false
                                        } });
            return newState;

        case types.FORM_PASSWDCHANGE_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { changePasswordForm: {
                                            status: true,
                                            result: true,
                                            errors: {}
                                        } });
            return newState;

        case types.FORM_PASSWDCHANGE_FAIL:
            newState = Object.assign(   {}, 
                                        state, 
                                        { changePasswordForm: {
                                            status: true,
                                            result: false,
                                            errors: action.errors
                                        } });
            return newState;

        // JUSTSHAREFORM
        case types.FORM_JUSTSHAREFORM_SUBMITTING:
            jsForm = Object.assign( {},
                                    state.addJustShareForm,
                                    {   
                                        status: false
                                    });

            newState = Object.assign(   {}, 
                                        state, 
                                        { jsForm });
            return newState;

        case types.JUSTSHARE_FORMSUBMIT_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { addJustShareForm: 
                                            Object.assign(  
                                                {},
                                                state.addJustShareForm,
                                                action.addJustShareForm,
                                                {   
                                                    status: true,
                                                    result: true
                                                })
                                        });
            return newState;

        case types.JUSTSHARE_FORMSUBMIT_ERROR:
        newState = Object.assign(   {}, 
                                    state, 
                                    { addJustShareForm: 
                                        Object.assign(  
                                            {},
                                            state.addJustShareForm,
                                            action.addJustShareForm,
                                            {   
                                                status: true,
                                                result: false,
                                                errors: action.errors
                                            })
                                    });
            return newState;

        default:
            return state;
    }

    return newState;
};