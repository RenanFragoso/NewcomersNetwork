import React from 'react';
import { HelpBlock } from 'react-bootstrap';

class ImgCanvasRender extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            avatarImageContent: null,
            changedImage: false
        }

        this.clickFile = this._clickFile.bind(this);
        this.changePicture = this._changePicture.bind(this);
    }

    componentDidMount() {
        const canvas = this.refs.canvas
        const avatarfile = this.refs.avatarfile;
        const ctx = canvas.getContext("2d");
        const img = this.refs.image;
        
        img.onload = () => {
          ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
        }

        window.addEventListener("avatarclick", this.clickFile);
    }

    componentWillUnmount() {
        window.removeEventListener("avatarclick", this.clickFile);
    }

    _clickFile() {
        this.refs.avatarfile.click();
    }
    
    _changePicture() {
        const canvas = this.refs.canvas
        const avatarfile = this.refs.avatarfile;
        var ctx = canvas.getContext("2d");
        var img = this.refs.image;
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        img.src = URL.createObjectURL(avatarfile.files[0]);
        this.props.updateFormImageFile(avatarfile);
    }

    render() {
        const { errors } = this.props;
        const canvasContent =         
            <div>
                <canvas ref="canvas" width={200} height={200} className="aligncenter img-usrprofile img-thumbnail" />
                <img ref="image" src={this.props.image} className="hidden" />
                <input type="file" ref="avatarfile" id="Avatarfile" name="Avatarfile" className="hidden" onChange={this.changePicture}/>
                <HelpBlock>{(errors)}</HelpBlock>
            </div>;
        
        return canvasContent;
    }
}

export default ImgCanvasRender;