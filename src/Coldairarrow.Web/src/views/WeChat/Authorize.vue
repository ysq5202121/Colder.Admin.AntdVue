<template>
    <div>
           {{this.$route.path}}
           {{this.$route.query}}
          {{this.data}}
          {{this.aaa}}
  </div>
</template>

<script>
module.exports = {
  mounted () {
    this.getAccesstoken()
  },
  data: function () {
    return {
      data: {},
      greeting: 'Hello',
      aaa: { a:1 }
    }
  },
  methods: {
    getAccesstoken () {
      this.loading = true
      this.$http
        .get('https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=ww5edb01e84de945a4&corpsecret=gNd9XIo_cLjCbHZX5T-fh7kzkyKZogxFTio1wQmUCAA')
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          console.log(resJson.Data)
        })
    },
    getWeChatUserInfo () {

    }
  }
}
</script>
