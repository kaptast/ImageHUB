const requestProfileType = 'REQUEST_PROFILE';
const receiveProfileType = 'RECEIVE_PROFILE';
const initialState = { profile: [], isLoading: false};

export const actionCreators = {
    requestProfile: index => async (dispatch, getState) =>{
        dispatch({type: requestProfileType, index});

        const url = `api/Profiles/GetProfile?id=${index}`
        const response = await fetch(url);
        const profile = await response.json();

        dispatch({type: receiveProfileType, index, profile});
    }
};

export const reducer = (state, action) => {
    state = state || initialState;
  
    if (action.type === requestProfileType) {
      return {
        ...state,
        index: action.index,
        isLoading: true
      };
    }
  
    if (action.type === receiveProfileType) {
      return {
        ...state,
        index: action.index,
        profile: action.profile,
        isLoading: false
      };
    }
  
    return state;
  };