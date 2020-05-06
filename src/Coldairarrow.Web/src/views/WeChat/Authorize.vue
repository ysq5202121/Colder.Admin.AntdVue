<template>
  <div>
    <van-dialog v-model="show" confirm-button-text="授权登录" :before-close="closeDiag">
      <a-card hoverable>
        <img
          alt="example"
          src="https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png"
          slot="cover"
        />
        <a-card-meta title="获取用户信息" description="需要访问您企业微信基础信息">
          <a-avatar icon="exchange" slot="avatar" />
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
      show: true
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
