const requestHomePostsType = 'REQUEST_HOME_POSTS';
const receiveHomePostsType = 'RECEIVE_HOME_POSTS';
const initialState = { homePosts: [], isLoading: false};

export const actionCreators = {
    requestHomePosts: index => async (dispatch, getState) => {
        if (index === getState().homePosts.index) {
            return;
        }

        dispatch({type: requestHomePostsType, index});

        const url = `api/images`
        const response = await fetch(url);
        const homePosts = await response.json();

        dispatch({type: receiveHomePostsType, index, homePosts});
    }
};

export const reducer = (state, action) => {
    state = state || initialState;
  
    if (action.type === requestHomePostsType) {
      return {
        ...state,
        index: action.index,
        isLoading: true
      };
    }
  
    if (action.type === receiveHomePostsType) {
      return {
        ...state,
        index: action.index,
        homePosts: action.homePosts,
        isLoading: false
      };
    }
  
    return state;
  };