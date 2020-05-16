<template>
  <div>
    <van-tabbar route>
      <van-tabbar-item replace to="/ClientFood/Order" icon="shop-o" :badge="badge>0?badge:''">点餐</van-tabbar-item>
      <van-tabbar-item replace to="/ClientFood/OrderList" icon="orders-o">订单</van-tabbar-item>
      <van-tabbar-item replace to="/ClientFood/UserInfo" icon="user-o">我的</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
export default {
  date: {},
  props: {
    badge: Number
  }
}
</script>
