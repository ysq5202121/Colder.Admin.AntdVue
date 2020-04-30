<template>
  <div>
    <van-notice-bar color="#1989fa" background="#ecf9ff" left-icon="volume-o">点餐时间为:</van-notice-bar>
    <van-empty description="未发布菜品" v-if="isempt" />
    <div v-for="item in data" :key="item.Id">
      <van-card :price="item.Price" :thumb="item.ImgUrl" @click-thumb="handleImage(item.ImgUrl)">
        <template #title>
          <div style="font-size: 15px;">{{ item.FoodName }}</div>
        </template>
        <template #desc>
          <div style="font-size: 12px;">{{ item.FoodDesc }}</div>
        </template>
        <template #price>
          <div style="color:red">
            <b>¥{{ item.Price }}.00</b>
          </div>
        </template>
        <template #tags>
          <van-tag plain type="danger">{{ item.SupplierName }}</van-tag>
        </template>
        <template #tag>
          <van-tag mark type="danger" v-show="item.FoodQty==0">售罄</van-tag>
        </template>
        <template #num>剩余:{{ item.FoodQty }}份</template>
        <template #bottom>
          <van-rate v-model="item.FoodQty" readonly />
        </template>
        <template #footer>
          <van-stepper v-model="item.Num" min="0" :max="item.FoodQty" @change="onChange(item)" />
        </template>
      </van-card>
    </div>
    <div style="height:90px">
      <van-submit-bar
        :price="total"
        button-text="提交订单"
        @submit="onSubmit"
        tip-icon="shop"
        v-if="!isempt"
        :loading="loading"
      >
        <template #default>您的收货地址是：大餐厅</template>
        <template #tip>您的收货地址是：大餐厅</template>
      </van-submit-bar>
    </div>
  </div>
</template>

<script>
import { ImagePreview } from 'vant'
export default {
  mounted() {
    this.getDataList()
  },
  components: {
    [ImagePreview.Component.name]: ImagePreview.Component
  },
  data: function() {
    return {
      data: [],
      shopCar: [],
      isempt: false,
      loading: false
    }
  },
  methods: {
    getDataList() {
      this.loading = true
      this.$http.post('/ServerFood/F_PublishFood/GetDataListToMobile', {}).then(resJson => {
        this.loading = false
        // 扩展对象集合
        const newData = []
        Object.assign(newData, resJson.Data)
        newData.forEach(a => {
          this.$set(a, 'Num', 0)
        })
        this.data = newData
        if (this.data.length === 0) {
          this.isempt = true
        }
      })
    },
    onSubmit() {
      if (this.total === 0) {
        this.$message.error('请先选择商品在提交')
        return
      }
      this.loading = true
      this.$http.post('/ServerFood/F_Order/PlaceOrder', this.shopCar).then(resJson => {
        this.loading = false
        if (resJson.Success) {
          this.$message.success('操作成功!')
          this.visible = false
          this.getDataList()
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    onChange(item) {
      var isOn = true
      this.shopCar = this.shopCar.filter(a => a.Num > 0)
      this.shopCar.some(a => {
        if (a.Id === item.Id) {
          a.Num = item.Num
          isOn = false
          return true
        }
      })
      if (isOn) {
        this.shopCar.push({
          Id: item.Id,
          Price: item.Price,
          Num: item.Num
        })
      }
    },
    handleImage(img) {
      ImagePreview({ images: [img], closeable: 'true' })
    }
  },
  computed: {
    total() {
      // 计算总价的方法
      let sum = 0
      for (let i = 0; i < this.shopCar.length; i++) {
        sum += parseFloat(this.shopCar[i].Price) * parseFloat(this.shopCar[i].Num)
      }
      sum = sum * 10 * 10 // 转换成分
      return sum
    }
  }
}
</script>
