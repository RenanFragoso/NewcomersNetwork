import {createStore, applyMiddleware} from 'redux';
import rootReducer from '../reducers'
import thunkMiddleware from 'redux-thunk';

export default function configureStore(initialState, history) {
    return createStore( rootReducer, 
                        initialState, 
                        applyMiddleware(thunkMiddleware));
};