import axios from 'axios';

const requestHomePostsType = 'REQUEST_HOME_POSTS';
const receiveHomePostsType = 'RECEIVE_HOME_POSTS';
const initialState = { homePosts: [], isLoading: false };

export const actionCreators = {
  requestHomePosts: index => async (dispatch, getState) => {
    if (index === getState().homePosts.index) {
      return;
    }

    dispatch({ type: requestHomePostsType, index });

    const url = `api/images`

    axios.get(url)
      .then(res => {
        console.log("ok home store.");
        console.log(res);
        const homePosts = res.data;
        dispatch({ type: receiveHomePostsType, index, homePosts });
      }).catch(err => {
        console.log(err);
        console.log("to get images");
      });
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