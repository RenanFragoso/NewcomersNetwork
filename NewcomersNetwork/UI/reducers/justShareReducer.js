import * as types from 'actions/actionTypes';
import initialState from './initialState';

export default function justShareGridReducer(state = initialState.justShare, action) {
    var newState;
    var newUserJustShares;
    var filterData;
    switch (action.type) {
        
        case types.RECEIVED_VALUELISTS_SUCCESS:
            var Categories = [];
            var filters = (action.valuesLists ? action.valuesLists.find( item => item.ListName === "classifiedgroups") : []);
            var categories = (action.valuesLists ? action.valuesLists.find( item => item.ListName === "classifiedgroupsfull") : []);
            if(filters) {
                filters.Itens.forEach( item => Categories.push(item.id) )
            }
        
            filterData = Object.assign( {}, 
                                        state.justShareGrid.filterData,
                                        { Categories });

            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign( {}, 
                                                                        state.justShareGrid, 
                                                                        { filterData, filters, categories })
                                        });
            
            return newState;        
        
        case types.JSHARE_RECEIVED_GRID_DATA:
            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign({}, state.justShareGrid, action.justShareGridData, { result: true, status: true }) });
            return newState;

        case types.JUSTSHARE_GRID_LOADING:
            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign({}, state.justShareGrid, { result: false, status: false }) });
            return newState;

        case types.JUSTSHARE_GRID_DONELOADING:
            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign({}, state.justShareGrid, { status: true }) });
            return newState;

        case types.JSHARE_ERROR_MESSAGE:
            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign({}, state.justShareGrid, action.justShareGridData, { result: false, status: true }) });
            return newState;

        case types.JSHARE_RECEIVED_DETAILS:
        newState = Object.assign(   {}, 
                                        state, 
                                        { justShareDetails: action.justShareDetails });
            return newState;

        case types.JUSTSHARE_NEWID_SUCCESS:
            newState = Object.assign(   {}, 
                                        state, 
                                        { newJustShareId: action.newId });
            return newState;
        
        case types.JUSTSHARE_USERJUSTSHARE_SUCCESS:
            newUserJustShares =     Object.assign(  {}, 
                                                    state.userJustShares,
                                                    {
                                                        justShareWidget: action.userJustShare,
                                                        result: true,
                                                        status: true,
                                                        error: ""
                                                    });
            newState = Object.assign(   {}, 
                                        state, 
                                        {
                                            userJustShares: newUserJustShares
                                        });
            return newState;

        case types.JUSTSHARE_USERJUSTSHARE_FAIL:
            const userJustSharesError = Object.assign(  {}, 
                                                        state.userJustShares,
                                                        {
                                                            justShares: [],
                                                            result: false,
                                                            status: false,
                                                            error: action.error
                                                        });
            newState = Object.assign(   {}, 
                                        state, 
                                        {
                                            userJustSharesError
                                        });
            return newState;

        case types.JUSTSHARE_FINISH_SUCCESS:
            newUserJustShares =     Object.assign(  {}, 
                                                    state.userJustShares,
                                                    {
                                                        justShareWidget: {},
                                                        result: true,
                                                        status: false,
                                                        error: ""
                                                    });
            newState = Object.assign(   {}, 
                                        state, 
                                        {
                                            userJustShares: newUserJustShares
                                        });
            return newState;

        case types.JUSTSHARE_FINISH_FAIL:
            newUserJustShares =     Object.assign(  {}, 
                                                    state.userJustShares,
                                                    {
                                                        result: false,
                                                        status: false,
                                                        error: action.error
                                                    });
            newState = Object.assign(   {}, 
                                        state, 
                                        {
                                            userJustShares: newUserJustShares
                                        });
            return newState;

        case types.JUSTSHARE_SELECTED_GRIDTYPE:
             filterData = Object.assign(    {}, 
                                            state.justShareGrid.filterData,
                                            { Type: action.jsType });

            newState = Object.assign(   {}, 
                                        state, 
                                        { justShareGrid: Object.assign( {}, 
                                                                        state.justShareGrid, 
                                                                        { filterData })
                                        });
            return newState;

        default:
            return state;
    }

    return newState;
};