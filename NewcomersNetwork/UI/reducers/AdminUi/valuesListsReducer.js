import * as types from 'actions/AdminUi/actionTypes';
import initialState from './initialState';

export default function valuesListsReducer(state = initialState.valuesLists, action) {
    
    var newState;
    
    switch (action.type) {

        case types.VALUESLISTS_RECEIVED:
            newState = Object.assign(   {}, 
                                        state,
                                        { 
                                            lists: data.ValueLists
                                        });
            break;

        default:
            return state;
    }
    
    return newState;
}