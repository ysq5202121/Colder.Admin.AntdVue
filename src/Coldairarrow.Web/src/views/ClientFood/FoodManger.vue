<template>
    <div>
        第一界面
    <a-row type="flex" justify="center" style="margin-top:10px">
      <a-col :span="1"> <a-button type="primary" @click="goFoodCenter" >菜品管理</a-button></a-col>
    </a-row>
   <a-row type="flex" justify="center" style="margin-top:10px">
      <a-col :span="1"> <a-button type="primary">点餐</a-button></a-col>
    </a-row>
    <a-row type="flex" justify="center" style="margin-top:10px" >
      <a-col :span="1"> <a-button type="primary">我的订单</a-button></a-col>
    </a-row>
   <a-row type="flex" justify="center" style="margin-top:10px">
      <a-col :span="1"> <a-button type="primary">报餐统计</a-button></a-col>
    </a-row>
     <a-row type="flex" justify="center" style="margin-top:10px">
      <a-col :span="1"> <a-button type="primary">设置</a-button></a-col>
    </a-row>
  </div>
</template>

<script>
module.exports = {
  data: function() {
    return {
      greeting: "Hello"
    };
  },
   methods: {
        goFoodCenter() {
      this.$router.push('FoodCenter')
        }

   }
};
</script>