<template>
  <div>
    <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="getDataList">
      <van-cell
        v-for="item in data"
        :key="item.Id"
        :title="item.CreatorName"
        :value="item.OldDepartment"
        :label="item.FoodName"
      />
    </van-list>
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
      data: [],
      loading: false,
      finished: false
    }
  },
  methods: {
    getDataList() {
      this.loading = true
      this.$http.post('/ServerFood/F_OrderInfo/ScanCode?day=' + this.$route.query.day, {}).then(resJson => {
        this.loading = false
        this.data = resJson.Data
        this.finished = true
      })
    }
  }
}
</script>
