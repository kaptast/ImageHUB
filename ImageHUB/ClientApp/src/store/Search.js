import axios from 'axios';

const requestSearchResultsType = 'REQUEST_SEARCH_RESULTS';
const receiveSearchResultsType = 'RECEIVE_SEARCH_RESULTS';

const initialState = { results: [], isLoading: false };

export const actionCreators = {
  requestSearchResults: index => async (dispatch, getState) => {
    console.log(index);
    console.log(getState().results.index);

    if (index === getState().results.index) {
      console.log("Same ID");
      return;
    }

    dispatch({ type: requestSearchResultsType, index });

    const url = `api/profile/GetAllByName?name=${index}`;
    console.log(url);
    axios.get(url, {
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(res => {
        const results = res.data;
        console.log(results);
        dispatch({ type: receiveSearchResultsType, index, results });
      })
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestSearchResultsType) {
    return {
      ...state,
      index: action.index,
      isLoading: true
    };
  }

  if (action.type === receiveSearchResultsType) {
    return {
      ...state,
      index: action.index,
      results: action.results,
      isLoading: false
    };
  }

  return state;
};