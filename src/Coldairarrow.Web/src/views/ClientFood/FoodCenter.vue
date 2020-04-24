<template>
    <div>
    菜品管理中心
    <a-row type="flex" justify="center" style="margin-top:10px">
       <a-col :span="1"> <a-button type="primary" @click="goFoodAdd">添加菜品</a-button></a-col>
       <a-col :span="1"> <a-button type="primary" @click="goFoodAdd">发布菜品</a-button></a-col>
    </a-row>
    显示菜品信息
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
        goFoodAdd() {
              this.$router.push('FoodAdd');
        }

   }
};
</script>
<style scoped>
     
</style>