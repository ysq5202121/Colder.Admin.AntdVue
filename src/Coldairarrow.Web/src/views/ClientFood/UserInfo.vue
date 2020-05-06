<template>
  <div>
    <van-card>
      <template #tags>
        <van-tag plain type="danger">VIP</van-tag>
      </template>
      <template #thumb>
        <van-image round width="80" height="80" :src="data.UserImgUrl" />
      </template>
      <template #title>
        <div style="font-size: 15px;font-weight:500">{{ data.UserName }}</div>
      </template>
      <template #desc>
        <div style="font-size: 12px;">{{ data.Department }}</div>
      </template>
    </van-card>
    <van-cell title="我的设置" is-link url="/ClientFood/UserInfoSet" />
    <van-field
      readonly
      clickable
      name="picker"
      :value="value"
      label="选择器"
      placeholder="点击选择城市"
      @click="showPicker = true"
    />
    <van-popup v-model="showPicker" position="bottom">
      <van-picker
        show-toolbar
        :columns="columns"
        @confirm="onConfirm"
        @cancel="showPicker = false"
      />
    </van-popup>
    <van-cell title="关于我们" is-link />
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  mounted() {
    this.getData()
  },
  data: function() {
    return {
      data: [],
      value: '',
      columns: ['杭州', '宁波', '温州', '嘉兴', '湖州'],
      showPicker: false
    }
  },
  components: {
    FoodTabbar
  },
  methods: {
    getData() {
      this.loading = true
      this.$http.post('/ServerFood/F_UserInfo/GetUserInfoToMoblie', {}).then(resJson => {
        this.loading = false
        this.data = resJson.Data
      })
    },
    onConfirm(value) {
      this.value = value
      this.showPicker = false
    }
  }
}
</script>
