<template>
  <div>
    <van-dialog v-model="show" confirm-button-text="授权登录" :before-close="closeDiag">
      <a-card hoverable>
        <img
          alt="example"
          :src="dinnerUrl"
          slot="cover"
        />
        <a-card-meta title="由 佰思威 提供" description="申请访问您企业微信基础信息">
          <a-avatar :src="logoUrl" slot="avatar" />
        </a-card-meta>
      </a-card>
    </van-dialog>
  </div>
</template>

<script>
export default {
  mounted() {
    this.code = this.$route.query.code
  },
  data: function() {
    return {
      data: {},
      code: '',
      show: true,
      logoUrl: require('@/assets/logo.png'),
      dinnerUrl: require('@/assets/image/dinner.jpg')
    }
  },
  methods: {
    closeDiag(action, done) {
      this.loading = true
      this.$http.get('/Wechat/WeChatAuth/Login?code=' + this.code).then(resJson => {
        this.loading = false
        if (resJson.Success) {
          localStorage.setItem('jwtToken', resJson.Data)
          done()
          this.$router.push({ path: '/ClientFood/Order' })
        } else {
          this.$notify(resJson.Msg)
          done(false)
        }
      })
    }
  }
}
</script>
