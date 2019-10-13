import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Profile';
import Feed from './ProfileFeed';

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
    return (
      <Feed posts={this.props.homePosts} />
    );
  }
}

export default connect(
  state => state.homePosts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Profile);

