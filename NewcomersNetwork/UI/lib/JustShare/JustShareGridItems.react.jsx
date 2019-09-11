import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import {Grid, Row, Col} from 'react-bootstrap';
import moment from 'moment';
import {withRouter} from 'react-router-dom';
import FontAwesome from 'react-fontawesome';
import JustShareGridItem from './JustShareGridItem.react';
import * as justShareActions from '../../actions/justShareActions';

class JustShareGridItems extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        var itemsContent = [];
        this.props.items.map((item, i) => {
            itemsContent.push( <JustShareGridItem item={item} itmNum={i} key={i} />);
        });
        const gridItemsContent =    <Row className="js-itemsgrid">
                                        <Grid>
                                            {itemsContent}
                                        </Grid>
                                    </Row>;
        
        return gridItemsContent;
    }
};

function mapStateToProps(state, ownProps) {  
    return {    
        userInfo: state.auth.userInfo
    };
}

function mapDispatchToProps(dispatch) {
    return {    
        actions: bindActionCreators(justShareActions, dispatch)
    };
}

JustShareGridItems = withRouter(JustShareGridItems);
export default connect(mapStateToProps, mapDispatchToProps)(JustShareGridItems);
