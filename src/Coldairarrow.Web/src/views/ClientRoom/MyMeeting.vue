<template>
  <div>
    <van-tabs v-model="active">
      <van-tab title="我参会的">内容 1</van-tab>
      <van-tab title="我发布的">内容 2</van-tab>
      <van-tab title="我相关的">内容 3</van-tab>
    </van-tabs>
    <RoomTabbar></RoomTabbar>
  </div>
</template>

<script>
import RoomTabbar from './RoomTabbar'
export default {
  mounted() {},
  data: function() {
    return {
      data: [],
      loading: false,
      active: 1
    }
  },
  components: {
    RoomTabbar
  },
  methods: {}
}
</script>
