<template>
  <div>
    <van-tabbar route>
      <van-tabbar-item
        replace
        to="/ClientRoom/ConferenceRoom"
        icon="wap-home-o"
        :badge="badge>0?badge:''"
      >会议室</van-tabbar-item>
      <van-tabbar-item replace to="/ClientRoom/MyMeeting" icon="friends-o">会议管理</van-tabbar-item>
      <van-tabbar-item replace to="/ClientRoom/More" icon="more-o">更多</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
export default {
  date: {},
  props: {
    // eslint-disable-next-line vue/require-default-prop
    badge: Number
  }
}
</script>
