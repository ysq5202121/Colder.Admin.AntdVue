<template>
  <div>
    <div v-if="isOk">
      <van-card v-if="data">
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
      <van-field
        readonly
        clickable
        name="picker"
        :value="SelectValue"
        label="关联门店"
        placeholder="点击选择门店"
        @click="OpenShopNameWin"
      />
      <van-popup v-model="showPicker" position="bottom">
        <van-picker
          show-toolbar
          :columns="columns"
          @confirm="onConfirm"
          @cancel="showPicker = false"
          :default-index="defaultIndex"
        />
      </van-popup>
      <van-cell title="我的设置" is-link url="/ClientFood/UserInfoSet" />
      <van-cell title="关于我们" is-link />
    </div>
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  mounted() {
    this.getUserInfoList()
    this.getShopInfoList()
  },

  data: function() {
    return {
      data: [],
      columns: [],
      showPicker: false,
      SelectValue: '',
      dataShopName: [],
      loading: false,
      isOk: false,
      defaultIndex: 1
    }
  },
  components: {
    FoodTabbar
  },
  methods: {
    getUserInfoList() {
      this.loading = true
      this.$http.post('/ServerFood/F_UserInfo/GetUserInfoToMoblie', {}).then(resJson => {
        this.data = resJson.Data
        this.loading = false
        if (this.data !== undefined) {
          this.SelectValue = this.data.ShopName
        }
        this.isOk = true
      })
    },
    getShopInfoList() {
      this.loading = true
      this.$http.post('/ServerFood/F_ShopInfo/GetDataListToMoblie', {}).then(resJson => {
        this.loading = false
        this.dataShopName = resJson.Data
        resJson.Data.forEach(a => {
          this.columns.push(a.ShopName)
        })
      })
    },
    onConfirm(value) {
      this.loading = true
      const shopData = this.dataShopName.find(a => a.ShopName === value)
      if (value !== this.SelectValue) {
        this.$http
          .post('/ServerFood/F_UserInfo/UpdateShopName', {
            ShopInfoId: shopData.Id
          })
          .then(resJson => {
            this.loading = false
            if (resJson.Success) {
              this.SelectValue = value
              this.showPicker = false
            } else {
              this.$message.error(resJson.Msg)
            }
          })
      } else {
        this.showPicker = false
      }
    },
    OpenShopNameWin() {
      this.showPicker = true
      this.defaultIndex = this.columns.findIndex(a => a === this.SelectValue)
    }
  }
}
</script>
