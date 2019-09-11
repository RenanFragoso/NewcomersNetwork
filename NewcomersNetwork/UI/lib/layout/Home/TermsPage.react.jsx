import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';

class TermsPage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const headSection =
                <Row className="sub-header" >
                    <Col xs={12}>
                        <span className="txt-tpc-red">Terms of Use/User Agreement</span>
                    </Col>
                </Row>;

        const contentSection =                
            <Row>
                <Grid className="content-panel">
                    <Row>
                        <Col xs={12}>
                            <p>
                                The Newcomers Network website (“The Website”) is created and administered by volunteers of the Newcomers Network which operates under the Peoples Church Inc (“TPC”).
                            </p>
                            <p>
                                TPC Newcomers Network makes available a classifieds section on The Website for use by newcomers and volunteers (“Users”). The <strong>JustShare</strong> section is meant to help
                                newcomers to connect with individuals who may wish to provide help in settlement, employment and housing. It is not intended as a forum for commercial purposes.
                            </p>
                            <p>
                                The content in the <strong>JustShare</strong> section is provided on an "as is, where is" posted basis by users. TPC Newcomers Network, its employees and administration
                                volunteers do not screen classifieds posted on The Website, and further provide no endorsement or representation of any kind regarding the goods or services offered
                                and obtained through The Website. Each user of The Website is responsible for verifying any goods and services offered before agreeing to use them.
                            </p>
                            <p>
                                By using The Website <strong>JustShare</strong> section, you accept any risk or injury or loss of any kind relating to or arising out of your use in the <strong>JustShare</strong> section.
                                You agree to release TPC Newcomers Network, its staff and volunteers from liability of any kind relating to or arising out of the use of the information,
                                goods and services offered through the <strong>JustShare</strong> section.
                            </p>
                            <p>
                            </p>
                            <ul className="sub-list">
                                <li>post content or items in an inappropriate category</li>
                                <li>post multiple JustShare classifieds ads for the same item in the same category</li>
                                <li>post JustShare classifieds for commercial purposes</li>
                                <li>post JustShare classifieds containing information that is false, inaccurate, misleading, defamatory, libelous or invasive of a person"s privacy</li>
                                <li>post material that is abusive, vulgar, hateful, harassing, obscene, threatening, or otherwise in violation of any law.</li>
                            </ul>

                            TPC Newcomers Network reserves the right to delete or alter any classifieds. TPC Newcomers Network ministry works with local and national law enforcement
                            agencies to identify and prosecute perpetrators of illegal activity on The Website. TPC Newcomers Network reserves the right to reveal your information in the event of a complaint or legal action arising from any <strong>JustShare</strong> classified posted by you.

                            <p></p>
                            <p>
                                <strong>Personal Information and Privacy</strong> except where otherwise stated in the Agreement, TPC will not disclose any personal information to third parties without your explicit consent.
                            </p>
                            <p>
                                By navigating The Website, you agree that TPC may collect personal information through your interaction on the Website, including but not limited to:
                            </p>

                            <ul className="sub-list">
                                <li>account and volunteer information, including email address and physical contact information;</li>
                                <li>classified postings through the Website, and correspondence sent to the Newcomers Network staff and volunteers;</li>
                                <li>other information from your interaction with the Website, including but not limited to: computer and connection information, statistics on page views,
                                    traffic to and from the Website, IP address, and cookies.</li>
                            </ul>

                            <p></p>
                            <p>
                                You further agree that TPC may disclose personal information as necessary without your consent in order to:
                            </p>

                            <ul className="sub-list">
                                <li>respond to legal requirements;</li>
                                <li>enforce the policies of TPC;</li>
                                <li>respond to claims that a listing or other content violates the rights of others, or;</li>
                                <li>protect anyone"s rights, property, or safety.</li>
                                <li>Such information will be disclosed in accordance with applicable laws and regulations.</li>
                            </ul>
                            <p></p>
                            <p>
                                TPC may use cookies to better understand how The Website is used and to improve the performance of the Website for our users. TPC does not use cookies to
                                collect personal information about our Users, and we do not sell information about our traffic patterns to third parties.
                            </p>
                            <p>
                                <strong>Human Rights and&nbsp;Equity</strong>
                                TPC’s policy prohibits discrimination and harassment based on all grounds identified by the <a href="http://www.ohrc.on.ca/">ontario human rights code</a>: sex, sexual orientation, race, colour,
                                ancestry, place of origin, ethnic origin, citizenship, creed (faith), age, marital status, family status and same-sex partnerships, and receipt of public assistance.
                                content that is found to be in violation of these policies will be immediately deleted from the website.
                            </p>
                            <p>
                                <strong>Changes to the Agreement</strong>
                                The TPC reserves the right to revise The Agreement at any time in the future without notice. Revisions to The Agreement will be posted on The Website. By using
                                The Website, Users accept to adhere to any revisions made to The Agreement at any point in the future. Users are responsible for familiarizing themselves with the
                                terms of The Agreement and conducting themselves accordingly.
                            </p>
                        </Col>
                    </Row>
                </Grid>
            </Row>;

        const termsContent = 
            <Grid fluid>
                {headSection}
                {contentSection}
            </Grid>;

        return termsContent;
    }
};

export default TermsPage;