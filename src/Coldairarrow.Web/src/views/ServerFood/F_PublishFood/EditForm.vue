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
        <a-form-model-item label="门店名称" prop="ShopInfoId">
          <a-select v-model="entity.ShopInfoId" disabled="true">
            <a-select-option v-for="shopinfo in ShopInfoList" :key="shopinfo.Id">{{ shopinfo.ShopName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="商家名称" prop="SupplierName">
          <a-input v-model="entity.SupplierName" autocomplete="off" disabled="true" />
        </a-form-model-item>
        <a-form-model-item label="菜品名称" prop="FoodName">
          <a-input v-model="entity.FoodName" autocomplete="off" disabled="true" />
        </a-form-model-item>
        <a-form-model-item label="菜品描述" prop="FoodDesc">
          <a-input v-model="entity.FoodDesc" autocomplete="off" disabled="true" />
        </a-form-model-item>
        <a-form-model-item label="价格" prop="Price">
          <a-input v-model="entity.Price" autocomplete="off" disabled="true" />
        </a-form-model-item>
        <a-form-model-item label="图片" prop="ImgUrl" >
          <img :src="entity.ImgUrl" width="100" height="100" @click="handleOpenImg" style="cursor:pointer">
          <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
            <img alt="example" style="width: 100%" :src="entity.ImgUrl" />
          </a-modal>
        </a-form-model-item>
        <a-form-model-item label="发布时间" prop="PublishDate">
          <a-input v-model="entity.PublishDate" autocomplete="off" disabled="true" />
        </a-form-model-item>
        <a-form-model-item label="菜品数量" prop="FoodQty">
          <a-input v-model="entity.FoodQty" autocomplete="off" />
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
      title: '',
      ShopInfoList: [],
      previewVisible: false
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$http.post('/ServerFood/F_ShopInfo/GetDataListAll', {}).then(resJson => {
        if (resJson.Success) {
          this.ShopInfoList = resJson.Data
        }
      })
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
    },
    handleCancel () {
      this.previewVisible = false
    },
    handleOpenImg () {
      this.previewVisible = true
    }
  }
}
</script>
