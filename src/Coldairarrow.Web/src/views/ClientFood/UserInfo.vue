<template>
  <div>
      我是用户新消息
    <FoodTabbar></FoodTabbar>
  </div>
</template>

<script>
import FoodTabbar from './FoodTabbar'
export default {
  data: function() {
    return {
      greeting: 'Hello'
    }
  },
  components: {
    FoodTabbar
  }
}
</script>
