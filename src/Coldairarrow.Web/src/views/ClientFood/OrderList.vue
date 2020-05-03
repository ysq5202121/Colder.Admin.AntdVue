<template>
  <div>
    <div v-for="item in data" :key="item.Id">
      <van-card :price="item.Price" :thumb="avatar">
        <template #title>
          <div style="font-size: 15px;">订单编号:{{ item.OrderCode }}</div>
        </template>
        <template #desc>
          <div style="font-size: 12px;">下单时间:{{ item.CreateTime }}</div>
        </template>
        <template #price>
          <div style="color:red;font-size: 13px;">
            <b>¥{{ item.Price }}</b>
          </div>
        </template>
        <template #num>总数量:{{ item.OrderCount }}</template>
        <template #footer>
          <van-button size="mini">详情</van-button>
        </template>
      </van-card>
    </div>
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  mounted() {
    this.getDataList()
  },
  data: function() {
    return {
      data: [],
      shopCar: [],
      isempt: false,
      loading: false,
      avatar: require('@/assets/image/shop.jpg')
    }
  },
  components: {
    FoodTabbar
  },
  methods: {
    getDataList() {
      this.loading = true
      this.$http.post('/ServerFood/F_Order/GetDataListToMoblie', {}).then(resJson => {
        this.loading = false
        this.data = resJson.Data

        if (this.data !== undefined && this.data.length === 0) {
          this.isempt = true
        }
      })
    }
  }
}
</script>
