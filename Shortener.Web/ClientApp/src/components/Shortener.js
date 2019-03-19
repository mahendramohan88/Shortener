import React, { Component } from 'react';

export class Shortener extends Component {

    constructor (props) {
        super(props);
        this.state = { longUrl: '', shortUrl: '', loading: false, error: '' };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ longUrl: event.target.value, loading: false, error: '' });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.setState({ shortUrl: '', loading: true });

        fetch('api/Shortener/Create', {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'longUrl': this.state.longUrl
            }), 
        })
        .then(handleErrors)
        .then(response => response.json())
        .then(data => {
            this.setState({ shortUrl: data, loading: false, error: '' });
        })
        .catch(err => {
            console.log(err.message);
            this.setState({ shortUrl: '', loading: false, error: err.message });
        });
    }

    render() {

        const generateButton = (
            <button type="submit" className="btn btn-primary">Generate</button>
        );

        const loadingSpinner = (
            <button type="button" className="btn btn-primary" disabled>
                <span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...
            </button>
        );

        const shortUrlDisplay = (
            <div className="card col-md-6 col-sm-12">
                <div className="card-body">
                    <h1>Short URL</h1>
                    <p class="card-text">You can now use the link below to go to the URL you entered.</p>
                    <h5 class="card-title"><a href={this.state.shortUrl} target="blank">{this.state.shortUrl}</a></h5>
                </div>
            </div>
        );

        const errorDisplay = (
            <div className="alert alert-danger">{this.state.error}</div>
            );

        return (
            <div className="row">
                <form onSubmit={this.handleSubmit} className="col-md-6 col-sm-12">
                    <p></p>
                    <h1>Shortener</h1>
                    <div className="form-group has-danger">
                        <label htmlFor="inputUrl">Enter a URL below to receive a shortened URL</label>
                        <input id="inputUrl" type="text" className="form-control" value={this.state.value} onChange={this.handleChange} />
                        {this.state.error !== '' ? errorDisplay : ''}
                    </div>
                    {this.state.loading ? loadingSpinner : generateButton}
                    <p></p>
                </form>
                {this.state.shortUrl !== '' ? shortUrlDisplay : ''}
            </div>
        );
    }
}

function handleErrors(response) {
    if (response.status === 400) throw Error("Please enter a valid URL starting with https:// or http://.");
    if (!response.ok) throw Error(response.statusText);
    return response;
}