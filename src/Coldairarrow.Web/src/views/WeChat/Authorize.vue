<template>
  <div>
    <van-row justify="center" align="center" type="flex">
      <van-col span="24">
        <div style="height:100px"></div>
      </van-col>
    </van-row>
    <van-row justify="center" align="center" type="flex">
      <van-col span="8">
        <a-card hoverable style="width: 250px">
          <img
            alt="example"
            src="https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png"
            slot="cover"
          />
          <template class="ant-card-actions" slot="actions">
            <van-button type="info" icon="manager-o" @click="getAccesstoken">授权登录</van-button>
          </template>
          <a-card-meta title="授权登录" description="需要访问您的基础信息">
            <a-avatar icon="exchange" slot="avatar" />
          </a-card-meta>
        </a-card>
      </van-col>
    </van-row>
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
      greeting: 'Hello'
    }
  },
  methods: {
    getAccesstoken() {
      this.loading = true
      this.$http.get('/Wechat/WeChatAuth/Login?code=' + this.code).then(resJson => {
        this.loading = false
        if (resJson.Success) {
          localStorage.setItem('jwtToken', resJson.Data)
          this.$router.push({ path: '/ClientFood/Order' })
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    }
  }
}
</script>
