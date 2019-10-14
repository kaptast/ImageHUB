import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Profile';
import Feed from './ProfileFeed';
import Header from './Profile/Header';

class Profile extends Component {
  componentWillMount() {
    const index = parseInt(this.props.match.params.index, 10) || 0;
    this.props.requestProfile(index);
  }

  componentWillReceiveProps(nextProps) {
    const index = parseInt(nextProps.match.params.index, 10) || 0;
    this.props.requestProfile(index);
  }

  render() {
    console.log(this.props.profile);
    return (
      <div>
        <Header profile={this.props.profile} />
        <Feed posts={this.props.profile.posts} />
      </div>
    );
  }
}

export default connect(
  state => state.profile,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Profile);

