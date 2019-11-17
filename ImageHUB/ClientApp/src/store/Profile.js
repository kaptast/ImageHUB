import axios from 'axios';

const requestProfileType = 'REQUEST_PROFILE';
const receiveProfileType = 'RECEIVE_PROFILE';

const defaultProfile = {
  name: "",
  index: "asd",
  posts: [],
  friends: [],
  showFriendButton: false,
}

const initialState = { profile: defaultProfile, isLoading: false };

export const actionCreators = {
  requestProfile: index => async (dispatch, getState) => {
    if (index === getState().profile.index) {
      console.log("Same ID");
      console.log(index);
      return;
    }
    dispatch({ type: requestProfileType, index });

    const url = `api/profile/GetById?id=${index}`;
    console.log(url);
    axios.get(url)
      .then(res => {
        console.log("ok home store.");
        console.log(res);
        const profile = res.data;
        dispatch({ type: receiveProfileType, index, profile });
      })
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