import * as types from 'actions/AdminUi/actionTypes';
import initialState from './initialState';

export default function usersReducer(state = initialState.users, action) {
    
    var newState;
    
    switch (action.type) {

        case types.USERS_RECEIVED_LIST:
            newState = Object.assign(   {}, 
                                        state,
                                        { 
                                            usersList: { 
                                                users: action.data.usersList,
                                                pagination: action.data.pagination
                                            }
                                        });
            break;

            case types.USERS_RECEIVED_ROLES:
                debugger;
                newState = Object.assign(   {}, 
                                            state,
                                            { 
                                                roles: data.roles
                                            });
            break;

        default:
            return state;
    }
    
    return newState;
}