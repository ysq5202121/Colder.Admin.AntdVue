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
    component: () => import('@/views/WeChat/Authorize')
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  }
]
