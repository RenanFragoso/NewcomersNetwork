import React from 'react';
import { withRouter } from 'react-router-dom';
import { Grid, Row, Col, Label } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import moment from 'moment';

class GridUserDetails extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { data } = this.props;
        debugger;
        
        const userStatus = ( data.Status == "O" ?
            <Label bsStyle="success">Active</Label> :
            <Label bsStyle="danger">Inactive</Label> );
        const IsImmigrant = ( data.IsImmigrant ? 
            <Label bsStyle="success">Yes</Label> :
            <Label bsStyle="primary">No</Label> );
        const userRoles = data.UserRoles.map( 
            (role, itm) => (    role !== '' ? 
                                <Label bsStyle='primary' className='usrdet-usrroles' key={itm}>{role}</Label> : 
                                ''));
        
        const gridUsersDetailsContents = 
            <Grid>
                <Row>
                    <Col xs={12} className="nopadding">
                        <div className="usrdet-subtitle">User Details</div>
                    </Col>
                    <Col xs={6} className="nopadding">
                        <ul className="usrdet-itms-l">
                            <li><span>ID: </span>{data.Id}</li>
                            <li><span>UserName: </span>{data.UserName}</li>
                            <li><span>First Name: </span>{data.FirstName}</li>
                            <li><span>Last Name: </span>{data.LastName}</li>
                            <li><span>E-Mail: </span>{data.Email}</li>
                            <li><span>Gender: </span>{data.Gender}</li>
                            <li><span>Age Range: </span>{data.AgeRange}</li>
                            <li><span>Marital Status: </span>{data.MaritalStatus}</li>
                        </ul>
                    </Col>
                    <Col xs={6} className="nopadding">
                        <ul className="usrdet-itms-r">
                            <li><span>Education: </span>{data.Education}</li>
                            <li><span>Postal Code: </span>{data.PostalCode}</li>
                            <li><span>Nearest Intersection: </span>{data.NearestIntersection}</li>
                            <li><span>Is Immigrant: </span>{IsImmigrant}</li>
                            <li><span>Create Date: </span>{moment(data.CreateDate).format("DD/MM/YYYY hh:mm A")}</li>
                            <li><span>Last Modified: </span>{moment(data.LastModified).format("DD/MM/YYYY hh:mm A")}</li>
                            <li><span>Status: </span>{userStatus}</li>
                        </ul>
                    </Col>
                </Row>
                <Row>
                    <Col xs={6} className="usrdet-lcol">
                        <div className="usrdet-subtitle">User Roles</div>
                    </Col>
                    <Col xs={6} className="usrdet-rcol">
                        <div className="usrdet-subtitle">Actions</div>
                    </Col>
                </Row>
                <Row>
                    <Col xs={6} className="usrdet-lcol">
                        {userRoles}
                    </Col>
                    <Col xs={6} className="usrdet-rcol">

                    </Col>
                </Row>
            </Grid>;
        
        return gridUsersDetailsContents;
    }
};

export default withRouter(GridUserDetails);