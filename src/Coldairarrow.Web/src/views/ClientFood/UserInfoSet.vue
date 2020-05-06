<template>
  <div>
   
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  mounted() {

  },
  data: function() {
    return {
      data: []
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
    }
  }
}
</script>
