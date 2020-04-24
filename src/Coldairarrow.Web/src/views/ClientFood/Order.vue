<template>
    <div >
        <van-notice-bar color="#1989fa" background="#ecf9ff" left-icon="volume-o">
             点餐时间为:
        </van-notice-bar>  
        <div v-for="item in data">   
          <van-card
            :price="item.Prcie"
            :desc="item.FoodDesc"
            :title="item.FoodName"
            thumb="https://img.yzcdn.cn/vant/ipad.jpeg"
            >
             <template #tags>
               <div> <van-tag plain type="danger">{{item.SupplierName}}</van-tag></div>
            </template>
            <template #num>
               剩余:{{item.FoodQty}}份
            </template>
            <template #bottom>
             <van-rate v-model="item.FoodQty" readonly />
            </template>
            <template #footer>
            <van-stepper v-model="value" min="0" :max="item.FoodQty" @change="onChange"/>
            </template>
            </van-card>
        </div>
        <div>
            <van-submit-bar :price="3050" button-text="提交订单" @submit="onSubmit" tip-icon="shop" >
            <template #default>
               您的收货地址是：大餐厅
            </template>
            <template #tip>
               您的收货地址是：大餐厅
            </template>
            </van-submit-bar>
        </div>
    </div>

</template>

<script>

module.exports = {
  mounted () {
    this.getDataList()
  },
  data: function () {
    return {
      data: [],
      greeting: "Hello",
      value: 2
    }
  },
   methods: {
     getDataList () {
      this.loading = true
      this.$http
        .post('/ServerFood/F_PublishFood/GetDataListToMobile', {
        
        })
        .then(resJson => {
          this.loading = false
          // 扩展对象集合
          //var sss= {...resJson.Data,[{x:0}]}
          //console.log(sss)
          console.log(resJson.Data)
          this.data = resJson.Data.find(a=>{return a.price>3})
        })
    },
    onSubmit () {
        alert('提交订单了!')
    },
    onChange(value){
          console.log(value)
      
    }
   }
};
</script>