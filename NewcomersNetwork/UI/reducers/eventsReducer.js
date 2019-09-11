import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function servicesReducer(state = initialState.events, action) {
    
    var newState = state;

    switch (action.type) {

        case types.EVENTS_RECEIVED_EVENT:
                
                newState = Object.assign(   {}, 
                                            state, 
                                            { event: action.event });
                return newState;
        
        default:
            return state;
    }
};