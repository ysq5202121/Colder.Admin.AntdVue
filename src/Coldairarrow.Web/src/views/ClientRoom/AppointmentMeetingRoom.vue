<template>
  <div>
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
      loading: false
    }
  },
  components: {
    RoomTabbar
  },
  methods: {}
}
</script>
