<template>
    <div >
        <van-notice-bar color="#1989fa" background="#ecf9ff" left-icon="volume-o">
             点餐时间为:
        </van-notice-bar>  
        <div v-for="item in data">   
          <van-card
            :price="item.Price"
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
            <van-stepper v-model="item.Num" min="0" :max="item.FoodQty" @change="onChange(item)"/>
            </template>
            </van-card>
        </div>
        <div>
            <van-submit-bar :price="total" button-text="提交订单" @submit="onSubmit" tip-icon="shop" >
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
    console.log(1111)
    this.getDataList()
    this.getAccesstoken()
  },
  data: function () {
    return {
      data: [],
      greeting: "Hello",
      shopCar:[]
    }
  },
  methods: {

     getAccesstoken () {
      this.loading = true
      this.$http
        .get('/api/cgi-bin/gettoken?corpid=ww5edb01e84de945a4&corpsecret=gNd9XIo_cLjCbHZX5T-fh7kzkyKZogxFTio1wQmUCAA')
        .then(resJson => {
          this.loading = false
          console.log(resJson.Data)
        })
    },
    getDataList () {
      this.loading = true
      this.$http
        .post('/ServerFood/F_PublishFood/GetDataListToMobile', {     
        })
        .then(resJson => {
          this.loading = false
          // 扩展对象集合
          const newData = []
          Object.assign(newData, resJson.Data)
          newData.forEach(a=> {
            this.$set(a, 'Num', 0)
          })
          this.data = newData
       
        })
    },
    onSubmit () {
            
          if(this.total==0)
          {
            this.$message.error('请先选择商品在提交')
            return;
          }
          this.loading = true
          this.$http.post('/ServerFood/F_Order/PlaceOrder', this.shopCar).then(resJson => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false
            this.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
    },
    onChange(item)
    {
         var isOn=true
         this.shopCar=this.shopCar.filter(a=>a.Num>0)
         this.shopCar.some(a=>{
           if(a.Id==item.Id)
           {
             a.Num=item.Num
             isOn=false
             return true
           }
         })
        
        if(isOn){
        this.shopCar.push({
          Id:item.Id,
          Price:item.Price,
          Num:item.Num
        })
        }
    }
   },
  computed: {
    total(){//计算总价的方法
            let sum=0;
            for(let i=0;i<this.shopCar.length;i++){
                sum+=parseFloat(this.shopCar[i].Price)*parseFloat(this.shopCar[i].Num)
           }
            sum=sum*10*10 //转换成分
            return sum;
        }

    }

};
</script>