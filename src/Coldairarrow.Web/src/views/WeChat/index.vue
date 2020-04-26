<template>
    <div>
       {{this.$route.path}}
       {{this.$route.query}}
  </div>
</template>

<script>
module.exports = {
  mounted () {
    window.location.href = 'https://open.weixin.qq.com/connect/oauth2/authorize?appid=ww5edb01e84de945a4&redirect_uri=http://www.ysq.com:5001/WeChat/Authorize?get=1&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect'
  },
  data: function () {
    return {
      greeting: "Hello"
    }
  }
}
</script>