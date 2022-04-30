import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import FontAwesome from 'react-fontawesome';
import { GridUserDetails } from 'lib/components/AdminUi';
import * as usersActions from 'actions/AdminUi/usersActions';
import DataTable from 'react-data-table-component';

class GridUsers extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            filterData: {
                Page: 1,
                PageSize: 5,
                Word: ""
            }
        }

        this.pageChanged = this._pageChanged.bind(this);
        this.rowsChanged = this._rowsChanged.bind(this);
    }

    componentDidMount() {
        const { filterData } = this.state;
        this.props.usersActions.getUserList(filterData);
    }

    _pageChanged(newPage, totalRows) {
        var { filterData } = this.state;
        filterData.Page = newPage
        this.props.usersActions.getUserList(filterData);
        this.setState({ filterData });
    }

    _rowsChanged(newPageSize, currentPage) {
        var { filterData } = this.state;
        filterData.Page = 1;
        filterData.PageSize = newPageSize;
        this.props.usersActions.getUserList(filterData);
        this.setState({ filterData });
    }

    render() {
        debugger;
        const { usersList } = this.props;
        const { filterData } = this.state;
        const users = (usersList.users ? usersList.users : []);
        const totalItems = ( usersList.pagination ? usersList.pagination.TotalItems : 0 );
        const columns = [ 
            {
                name: "Username", 
                selector: "UserName", 
                sortable: true
            },
            {
                name: "Name", 
                selector: "FirstName", 
                sortable: true
            },
            {
                name: "E-Mail", 
                selector: "Email", 
                sortable: true
            },
        ];

        const gridUsersContents = 
            <DataTable  
                columns={columns}
                data={users} 
                noHeader
                highlightOnHover
                pointerOnHover 
                expandableRows
                expandableRowsComponent={<GridUserDetails />}
                sortIcon={<FontAwesome name='sort' />}
                onTableUpdate={()=>{}}
                pagination 
                paginationServer 
                paginationPerPage={filterData.PageSize}
                paginationTotalRows={totalItems}
                paginationRowsPerPageOptions={[5,10]}
                onChangePage={this.pageChanged} 
                onChangeRowsPerPage={this.rowsChanged} />
        
        return gridUsersContents;
    }
};

function mapStateToProps(state, ownProps) {  
    return { usersList: state.users.usersList };
}

function mapDispatchToProps(dispatch) {
    return { usersActions: bindActionCreators(usersActions, dispatch) };
}

GridUsers = withRouter(GridUsers);
export default connect(mapStateToProps, mapDispatchToProps)(GridUsers);