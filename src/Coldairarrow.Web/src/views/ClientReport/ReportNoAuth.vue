<template>
  <div>
    <a-result status="warning" title="很抱歉,您没有访问权限，请联系相关人员!">
      <!-- <template #extra>
        <a-button @click="goToOrderView" >点餐</a-button>
        <a-button @click="goToOrderListView">我的订单</a-button>
      </template> -->
    </a-result>

  </div>
</template>

<script>

export default {

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
