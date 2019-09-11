import React from 'react';
import BreadCrumbs from './BreadCrumbs.react';

class HeaderSection extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const styleClasses = this.props.bgClass + ' ' + this.props.textClass;
        const headSection = <section id="page-title" className={styleClasses + " sub-header"} >
                                <div className="container clearfix">
                                    <h1 className={this.props.textClass}>Legal / Privacy</h1>
                                    <span className={this.props.textClass}>{this.props.headContent}</span>
                                    <BreadCrumbs links={this.props.breadCrumbs} />
                                </div>
                            </section>;
        return headSection;
    }
};

export default HeaderSection;