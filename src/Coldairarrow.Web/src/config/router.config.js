// eslint-disable-next-line
import { UserLayout, PageView } from '@/layouts'
import WeChatHelper from '@/utils/helper/WeChatHelper'

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
    path: '/ClientFood/ScanCode',
    name: 'ScanCode',
    component: () => import('@/views/ClientFood/ScanCode')
  },
  {
    path: '/WeChat/index',
    name: 'index',
    component: () => import('@/views/WeChat/index'),
    beforeEnter: (to, from, next) => {
      // 路由导航拦截 modify by ysq 2020-04-29
      if (localStorage.getItem('jwtToken')) {
        next({ path: WeChatHelper.GetGoToUrl(to.query.Id) })
      } else {
        next()
      }
    }
  },
  {
    path: '/WeChat/Authorize',
    name: 'Authorize',
    component: () => import('@/views/WeChat/Authorize'),
    beforeEnter: (to, from, next) => {
      // 路由导航拦截 modify by ysq 2020-04-29
      if (localStorage.getItem('jwtToken')) {
        next({ path: WeChatHelper.GetGoToUrl(to.query.Id) })
      } else {
        next()
      }
    }
  },
  {
    path: '/ClientRoom/ConferenceRoom',
    name: 'ConferenceRoom',
    component: () => import('@/views/ClientRoom/ConferenceRoom')
  },
  {
    path: '/ClientRoom/MyMeeting',
    name: 'MyMeeting',
    component: () => import('@/views/ClientRoom/MyMeeting')
  },
  {
    path: '/ClientRoom/More',
    name: 'More',
    component: () => import('@/views/ClientRoom/More')
  },
  {
    path: '/ClientRoom/AppointmentMeetingRoom',
    name: 'AppointmentMeetingRoom',
    component: () => import('@/views/ClientRoom/AppointmentMeetingRoom')
  },
  {
    path: '/ClientReport/ClientReportView',
    name: 'ClientReportView',
    component: () => import('@/views/ClientReport/ClientReportView')
  },
  {
    path: '/ClientReport/ClientReportView2',
    name: 'ClientReportView2',
    component: () => import('@/views/ClientReport/ClientReportView2')
  },
  {
    path: '/ClientReport/ReportNoAuth',
    name: 'ReportNoAuth',
    component: () => import('@/views/ClientReport/ReportNoAuth')
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  }
]
