import React from 'react';
import { Col, Row } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import FacebookProvider, { Page } from 'react-facebook-sdk';
import { Timeline } from 'react-twitter-widgets';

class SocialMediaWidget extends React.Component {

    constructor(props) {
        super(props);
    }
    render() {

        const twitterComponent =    <Col sm={12} lgOffset={1} lg={5} className="media-panel" style={{"marginTop": "20px"}}>
                                        <div className="mdp-header">
                                            <a href="https://twitter.com/NewcomersTO" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="twitter-square" size="2x" className="si-twitter"/> @NewcomersTO</a>
                                        </div>
                                    </Col>;

        const fbComponent = <Col sm={12} lgOffset={1} lg={5} className="media-panel" style={{"marginTop": "20px"}}>
                                <div className="mdp-header">
                                    <a href="https://www.facebook.com/NewcomersNetworkTPC/" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="facebook-square" size="2x" className="si-facebook"/> NewcomersNetworkTPC</a>
                                </div>
                            </Col>;

        const vimeoComponent =   <Col sm={12} lgOffset={1} lg={5} className="media-panel" style={{"marginTop": "30px", "marginBottom": "20px"}}>
                                    <div className="mdp-header">
                                        <a href="https://vimeo.com/thepeopleschurchtoronto" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="vimeo-square" size="2x" className="si-vimeo"/> The Peoples Church</a>
                                    </div>
                                </Col>;

        const instagramComponent =  <Col sm={12} lgOffset={1} lg={5} className="media-panel"style={{"marginTop": "30px", "marginBottom": "20px"}}>
                                        <div className="mdp-header">
                                            <a href="https://www.instagram.com/TPCToronto/" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="instagram" size="2x" className="si-instagram"/> TPCToronto</a>
                                        </div>
                                    </Col>;

        /*
            <Col sm={12} xs={12} className="media-smpanel">
                        <a href="https://twitter.com/NewcomersTO" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="twitter-square" size="2x" className="si-twitter"/> @NewcomersTO</a>
                        <a href="https://www.facebook.com/NewcomersNetworkTPC/" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="facebook-square" size="2x" className="si-facebook"/> NewcomersNetworkTPC</a>
                    </Col>

                    <Col lgOffset={1} lg={5} className="media-panel">
                        <div className="mdp-header">
                            <a href="https://twitter.com/NewcomersTO" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="twitter-square" size="2x" className="si-twitter"/> @NewcomersTO</a>
                        </div>
                        <div className="mdp-content">
                            <Timeline
                                dataSource={{
                                    sourceType: 'profile',
                                    screenName: 'NewcomersTO'
                                }}
                                options={{
                                    username: 'NewcomersTO',
                                    height: '500',
                                    chrome: [ 'noheader', 'nofooter', 'noborders' ]
                                }} />
                        </div>
                        <div className="mdp-footer">
                        </div>
                    </Col>
                    
                    <Col lgOffset={1} lg={5} className="media-panel">
                        <div className="mdp-header">
                            <a href="https://www.facebook.com/NewcomersNetworkTPC/" style={{"lineHeight": "10px", "fontWeight": "bold"}} target="_blank;"><FontAwesome name="facebook-square" size="2x" className="si-facebook"/> NewcomersNetworkTPC</a>
                        </div>
                        <div className="mdp-content">
                            <FacebookProvider appId="999000" >
                                <Page   href="https://www.facebook.com/NewcomersNetworkTPC/" 
                                        tabs="timeline" 
                                        height={500} 
                                        width={500} 
                                        smallHeader={true} 
                                        adaptContainerWidth={true} 
                                        hideCover={true}
                                        showFacepile={false} />
                            </FacebookProvider>
                        </div>
                        <div className="mdp-footer">
                        </div>
                    </Col>
                */

        return (
                <div className="container">
                    {twitterComponent}
                    {fbComponent}
                    {vimeoComponent}
                    {instagramComponent}
                </div>
        );
    }
}

export default SocialMediaWidget;