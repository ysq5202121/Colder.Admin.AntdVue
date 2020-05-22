<template>
  <div></div>
</template>

<script>
import defaultSettings from '@/config/defaultSettings'
export default {
  mounted() {
    window.location.href =
      'https://open.weixin.qq.com/connect/oauth2/authorize?appid=' +
      defaultSettings.weChatAppId +
      '&redirect_uri=' +
      defaultSettings.WeChatUrl +
      '/WeChat/Authorize?appid=1&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect'
  },
  data: function() {
    return {
      greeting: 'Hello'
    }
  }
}
</script>
