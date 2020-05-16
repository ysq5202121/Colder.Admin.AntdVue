<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="门店名称" prop="ShopInfoId">
          <a-select v-model="entity.ShopInfoId" style="width: 200px">
            <a-select-option
              v-for="shopinfo in ShopInfoList"
              :key="shopinfo.Id"
            >{{ shopinfo.ShopName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="商家名称" prop="SupplierName">
          <a-input v-model="entity.SupplierName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="菜品名称" prop="FoodName">
          <a-input v-model="entity.FoodName" autocomplete="off" />
        </a-form-model-item>
        <a-form-item label="图片上传" >
          <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
          <c-upload-img ref="upload" v-model="entity.ImgUrl" :maxCount="1" ></c-upload-img>
        </a-form-item>
        <a-form-model-item label="菜品描述" >
          <a-input v-model="entity.FoodDesc" autocomplete="off" type="textarea" />
        </a-form-model-item>
        <a-form-model-item label="限购数量" >
          <a-input-number v-model="entity.Limit" :min="1" :max="9999" />
        </a-form-model-item>
        <a-form-model-item label="价格" prop="Price">
          <a-input-number v-model="entity.Price" :min="0" :max="999" :step="1" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import CUploadImg from '@/components/CUploadImg/CUploadImg'
export default {
  props: {
    parentObj: Object
  },
  components: {
    CUploadImg
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
      rules: {
        ShopInfoId: [{ required: true, message: '必填' }],
        SupplierName: [{ required: true, message: '必填' }],
        FoodName: [{ required: true, message: '必填' }],
        Price: [{ required: true, message: '必填' }]
      },
      title: '',
      ShopInfoList: []
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
        this.$http.post('/ServerFood/F_FoodInfo/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerFood/F_FoodInfo/SaveData', this.entity).then(resJson => {
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
    handleCancel() {
      this.visible = false
      this.$refs['upload'].initforceUpdate()
    }
  }
}
</script>
