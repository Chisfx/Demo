import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/HomeView.vue'
import User from './components/User.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/users',
      name: 'users',
      component: User
    }
  ]
})
