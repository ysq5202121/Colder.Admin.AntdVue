<template>
  <div>
    <a-result status="success" title="订单提交成功!">
      <template #extra>
        <a-button @click="goToOrderView" >点餐</a-button>
        <a-button @click="goToOrderListView">我的订单</a-button>
      </template>
    </a-result>
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  components: {
    FoodTabbar
  },
  data: function() {
    return {
      greeting: 'Hello'
    }
  },
  methods: {
    goToOrderView() {
      this.$router.push({ path: '/ClientFood/Order' })
    },
    goToOrderListView() {
      this.$router.push({ path: '/ClientFood/OrderList' })
    }
  }
}
</script>
