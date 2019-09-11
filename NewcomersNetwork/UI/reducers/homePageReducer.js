import * as types from '../actions/actionTypes';
import initialState from './initialState';

export default function homePageReducer(state = initialState.homePage, action) {

    switch (action.type) {
        case types.HPAGE_RECEIVED_BANNERS:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                banners: action.banners
                                            });
            return newState;
            break;
        
        case types.HPAGE_RECEIVED_SBLOCKS:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                serviceBlocks: action.serviceBlocks
                                            });
            return newState;
            break;

        case types.HPAGE_RECEIVED_JSHARE:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                justShare: action.justShare
                                            });
            return newState;
            break;
        
        case types.HPAGE_RECEIVED_SERVICES:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                services: action.services
                                            });
            return newState;
            break;
        
        case types.HPAGE_RECEIVED_EVENTS:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                events: action.events
                                            });
            return newState;
            break;

        case types.HPAGE_RECEIVED_TESTIMONIALS:
            var newState = Object.assign(   {}, 
                                            state, 
                                            {   
                                                testimonials: action.testimonials
                                            });
            return newState;
            break;

        case types.HPAGE_ERROR_MESSAGE:
        var newState = Object.assign(   {}, 
                                        state, 
                                        {   
                                            errorMessage: action.error
                                        });
            return newState;
            break;
        
        default:
            return state;        
            break;
    }
    
    return state;
}