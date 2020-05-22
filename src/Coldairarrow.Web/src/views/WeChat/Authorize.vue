<template>
  <div>
    <p>{{this.$route.query}}</p>
    <a-spin :spinning="loading">
      <van-dialog v-model="show" confirm-button-text="授权登录" :before-close="closeDiag">
        <a-card hoverable>
          <img alt="example" :src="dinnerUrl" slot="cover" />
          <a-card-meta title="由 佰思威 提供" description="申请访问您企业微信基础信息">
            <a-avatar :src="logoUrl" slot="avatar" />
          </a-card-meta>
        </a-card>
      </van-dialog>
    </a-spin>
  </div>
</template>

<script>
export default {
  mounted() {
    this.code = this.$route.query.code
    //this.CheckLogin(this.code)
  },
  data: function() {
    return {
      data: {},
      code: '',
      loading: false,
      show: false,
      logoUrl: require('@/assets/logo.png'),
      dinnerUrl: require('@/assets/image/dinner.jpg')
    }
  },
  methods: {
    closeDiag(action, done) {
      this.$http.get('/Wechat/WeChatAuth/Login?code=' + this.code).then(resJson => {
        if (resJson.Success) {
          localStorage.setItem('jwtToken', resJson.Data)
          done()
          this.$router.push({ path: '/ClientFood/Order' })
        } else {
          this.$notify(resJson.Msg)
          done(false)
        }
      })
    },
    CheckLogin() {
      this.loading = true
      this.$http.get('/Wechat/WeChatAuth/CheckLogin?code=' + this.code).then(resJson => {
        if (resJson.Success) {
          if (!resJson.Data) {
            this.closeDiag('', a => {})
          } else {
            console.log(111)
            this.loading = false
            this.show = true
          }
        } else {
          this.$notify(resJson.Msg)
        }
      })
    }
  }
}
</script>
