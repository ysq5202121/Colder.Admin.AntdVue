// eslint-disable-next-line
import { UserLayout, PageView } from '@/layouts'

/**
 * 基础路由,修改基础路由信息
 * modify by ysq 2020-04-23
 * @type { *[] }
 */
export const constantRouterMap = [
  {
    path: '/Home',
    component: UserLayout,
    redirect: '/Home/Login',
    hidden: true,
    children: [
      {
        path: '/Home/Login',
        name: 'Login',
        component: () => import('@/views/Home/Login')
      }
    ]
  },
  {
    path: '/ClientFood/FoodCenter',
    name: 'FoodCenter',
    component: () => import('@/views/ClientFood/FoodCenter')
  },
  {
    path: '/ClientFood/FoodManger',
    name: 'FoodManger',
    component: () => import('@/views/ClientFood/FoodManger')
  },
  {
    path: '/ClientFood/FoodAdd',
    name: 'FoodAdd',
    component: () => import('@/views/ClientFood/FoodAdd')
  },
  {
    path: '/ClientFood/Order',
    name: 'Order',
    component: () => import('@/views/ClientFood/Order')
  },
  {
    path: '/ClientFood/OrderList',
    name: 'OrderList',
    component: () => import('@/views/ClientFood/OrderList')
  },
  {
    path: '/ClientFood/OrderOk',
    name: 'OrderOk',
    component: () => import('@/views/ClientFood/OrderOk')
  },
  {
    path: '/ClientFood/UserInfo',
    name: 'UserInfo',
    component: () => import('@/views/ClientFood/UserInfo')
  },
  {
    path: '/ClientFood/UserInfoSet',
    name: 'UserInfoSet',
    component: () => import('@/views/ClientFood/UserInfoSet')
  },
  {
    path: '/ClientFood/FoodTabbar',
    name: 'FoodTabbar',
    component: () => import('@/views/ClientFood/FoodTabbar')
  },
  {
    path: '/WeChat/index',
    name: 'index',
    component: () => import('@/views/WeChat/index')
  },
  {
    path: '/WeChat/Authorize',
    name: 'Authorize',
    component: () => import('@/views/WeChat/Authorize'),
    beforeEnter: (to, from, next) => {
      // 路由导航拦截 modify by ysq 2020-04-29
      if (localStorage.getItem('jwtToken')) {
        next({ path: '/ClientFood/Order' })
      } else {
        next()
      }
    }
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  }
]
