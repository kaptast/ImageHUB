import '../index.css';
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/WeatherForecasts';
import Grid from '@material-ui/core/Grid';

class FetchData extends Component {
  /*componentWillMount() {
    // This method runs when the component is first added to the page
    const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
    this.props.requestWeatherForecasts(startDateIndex);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const startDateIndex = parseInt(nextProps.match.params.startDateIndex, 10) || 0;
    this.props.requestWeatherForecasts(startDateIndex);
  }*/

  render() {
    return (
      <Grid container>
        <Grid item xs={12}>
          <img alt="WIP" src="https://media.giphy.com/media/3d4OvvUo8WlpfibzzL/giphy.gif" />
        </Grid>
      </Grid>
    );
  }
}

export default connect(
  state => state.weatherForecasts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(FetchData);
