<template>
  <div>
    <van-pull-refresh v-model="loading" @refresh="getDataList">
      <div v-for="item in data" :key="item.Id">
        <van-card :price="item.Price" :thumb="item.ImageUrl">
          <template #title>
            <div style="font-size: 15px;">订单编号:{{ item.OrderCode }}</div>
          </template>
          <template #desc>
            <div style="font-size: 12px;">下单时间:{{ item.CreateTime }}</div>
          </template>
          <template #price-top>
            <div>订单状态：待评价</div>
          </template>
          <template #price>
            <div style="color:red;font-size: 13px;">
              总价
              <b>¥{{ item.Price }}</b>
            </div>
          </template>
          <template #num>总数量:{{ item.OrderCount }}</template>
          <template #footer>
            <van-button size="mini" @click="getDetials(item.OrderCode)">详情</van-button>
          </template>
        </van-card>
      </div>
      <van-empty description="没有订单信息" v-if="isempt" />
    </van-pull-refresh>
    <van-popup v-model="show" round position="bottom" :style="{ height: '50%' }" closeable>
      <div v-for="item in dataDetail" :key="item.Id">
        <van-card :price="item.Price" :thumb="item.ImageUrl">
          <template #title>
            <div style="font-size: 15px;">{{ item.FoodName }}</div>
          </template>
          <template #tags>
            <van-tag plain type="danger">{{ item.SupplierName }}</van-tag>
          </template>
          <template #desc>
            <div style="font-size: 12px;">{{ item.FoodDesc }}</div>
          </template>
          <template #price>
            <div style="color:red;font-size: 13px;">
              <b>¥{{ item.Price }}</b>
            </div>
          </template>
          <template #num>数量:{{ item.OrderInfoQty }}</template>
        </van-card>
      </div>
    </van-popup>
    <div style="height:50px">
      <FoodTabbar></FoodTabbar>
    </div>
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
      dataDetail: [],
      isempt: false,
      loading: false,
      avatar: require('@/assets/image/shop.jpg'),
      show: false
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
    },
    getDetials(OrderCode) {
      this.show = true
      this.$http
        .post('/ServerFood/F_OrderInfo/GetDataListToMoblie', {
          Condition: '',
          Keyword: OrderCode
        })
        .then(resJson => {
          this.dataDetail = resJson.Data
        })
    }
  }
}
</script>
