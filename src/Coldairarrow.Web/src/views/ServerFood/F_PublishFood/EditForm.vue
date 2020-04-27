<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="门店Id" prop="ShopInfoId">
          <a-input v-model="entity.ShopInfoId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="菜品Id" prop="FoodInfoId">
          <a-input v-model="entity.FoodInfoId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="价格" prop="Prcie">
          <a-input v-model="entity.Prcie" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="商家名称" prop="SupplierName">
          <a-input v-model="entity.SupplierName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="菜品名称" prop="FoodName">
          <a-input v-model="entity.FoodName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="菜品描述信息" prop="FoodDesc">
          <a-input v-model="entity.FoodDesc" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="菜品数量" prop="FoodQty">
          <a-input v-model="entity.FoodQty" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="图片" prop="ImgUrl">
          <a-input v-model="entity.ImgUrl" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="发布时间" prop="PublishDate">
          <a-input v-model="entity.PublishDate" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/ServerFood/F_PublishFood/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/ServerFood/F_PublishFood/SaveData', this.entity).then(resJson => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false

            this.parentObj.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
