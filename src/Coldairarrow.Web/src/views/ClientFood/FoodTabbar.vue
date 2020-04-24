<template>
   <div>
    <router-view />
    <van-tabbar route>
    <van-tabbar-item replace to="/ClientFood/Order" icon="home-o">
        点餐
    </van-tabbar-item>
    <van-tabbar-item replace to="/ClientFood/OrderList" icon="search">
        订单
    </van-tabbar-item>
    <van-tabbar-item replace to="/search" icon="search">
        我的
    </van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
module.exports = 
{
      date:{}
};
</script>