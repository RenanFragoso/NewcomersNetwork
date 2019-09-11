import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function servicesReducer(state = initialState.services, action) {
    
    var newState = state;

    switch (action.type) {

        case types.SERVICES_RECEIVED_SERVICE:

                newState = Object.assign(   {}, 
                                            state, 
                                            { service: action.service });
                return newState;
        
        case types.SERVICES_RECEIVED_SERVICEGROUP:

            newState = Object.assign(   {}, 
                                        state, 
                                        { serviceGroup: action.serviceGroup });
            return newState;

        default:
            return state;
    }
};