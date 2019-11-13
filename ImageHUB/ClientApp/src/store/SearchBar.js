/*const requestSearchResultsType = 'REQUEST_SEARCH_RESULTS';
const receiveSearchResultsType = 'RECEIVE_SEARCH_RESULTS';
const initialState = { results: [], isLoading: false };

export const actionCreators = {
    requestSearchResults: index => async (dispatch, getState) => {
        if (index === getState().results.index) {
          return;
        }

    dispatch({ type: requestSearchResultsType, index });

    const url = `api/profile`;
    axios.get(url)
      .then(res => {
        const results = res.data;
        dispatch({ type: requestSearchResultsType, index, results });
      }).catch(err => {
        console.log(err);
        console.log("to profiles");
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestSearchResultsType) {
    return {
      ...state,
      startDateIndex: action.startDateIndex,
      isLoading: true
    };
  }

  if (action.type === receiveSearchResultsType) {
    return {
      ...state,
      startDateIndex: action.startDateIndex,
      results: action.results,
      isLoading: false
    };
  }

  return state;
};*/
