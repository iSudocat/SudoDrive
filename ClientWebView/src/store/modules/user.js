import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

const getDefaultState = () => {
  return {
    token: getToken(),
    name: '',
    avatar: '',
    role: 'editor'
  }
}

const state = getDefaultState()

const mutations = {
  RESET_STATE: (state) => {
    Object.assign(state, getDefaultState())
  },
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { username, password } = userInfo
    if (typeof (CefSharp) === 'undefined') {
      return new Promise((resolve, reject) => {
        login({ username: username.trim(), password: password }).then(response => {
          const { data } = response
          commit('SET_TOKEN', data.token)
          setToken(data.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    } else {
      return new Promise((resolve, reject) => {
        window.cloudFileFunction.login(String(username), String(password)).then(response => {
          const res = JSON.parse(response)
          console.log('getInfo')
          console.log(res)
          if (res.status === 0) {
            commit('SET_TOKEN', res.data.token)
            setToken(res.data.token)
          } else {
            alert('用户名密码格式错误')
          }
          resolve()
        }).catch(error => {
          alert('错误的用户名密码')
          reject(error)
        })
      })
    }
  },

  // get user info
  getInfo({ commit, state }) {
    if (typeof (CefSharp) === 'undefined') {
      return new Promise((resolve, reject) => {
        getInfo(state.token).then(response => {
          const { data } = response
          if (!data) {
            return reject('Verification failed, please Login again.')
          }
          const { name, avatar } = data
          commit('SET_NAME', name)
          commit('SET_AVATAR', avatar)
          resolve(data)
        }).catch(error => {
          reject(error)
        })
      })
    } else {
      return new Promise((resolve, reject) => {
        window.cloudFileFunction.getLoginInfo().then(response => {
          const ret = JSON.parse(response)
          console.log('getInfo')
          console.log(ret)
          commit('SET_NAME', ret.username)
          commit('SET_AVATAR', 'https://avatars3.githubusercontent.com/u/42528067?s=400&u=375133832a4d631e7a1be26aa764b0ff91565f7f&v=4')
          resolve(ret)
        }).catch(error => {
          reject(error)
        })
      })
    }
  },

  // user logout
  logout({ commit, state }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        removeToken() // must remove  token  first
        resetRouter()
        commit('RESET_STATE')
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      removeToken() // must remove  token  first
      commit('RESET_STATE')
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

