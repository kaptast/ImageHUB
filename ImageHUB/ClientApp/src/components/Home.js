import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Home';
import Feed from './Feed';

class Home extends Component {
  componentWillMount() {
    this.props.requestHomePosts(0);
  }

  componentWillReceiveProps(nextProps) {
    this.props.requestHomePosts(0);
  }

  render() {
    return (
      <div>
      <p>{this.props.name}</p>
      <Feed posts={this.props.homePosts} />
      </div>
    );
  }
}

export default connect(
  state => state.homePosts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Home);

