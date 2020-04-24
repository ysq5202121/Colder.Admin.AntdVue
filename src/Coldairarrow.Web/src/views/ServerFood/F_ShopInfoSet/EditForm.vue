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
        <a-form-model-item label="点餐开始时间" prop="OrderBeginDate">
          <a-input v-model="entity.OrderBeginDate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="点餐结束时间" prop="OrderBeginEnd">
          <a-input v-model="entity.OrderBeginEnd" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="开始点餐提醒信息" prop="OrderBeginRemind">
          <a-input v-model="entity.OrderBeginRemind" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="结束点餐提醒信息" prop="OrderEndRemind">
          <a-input v-model="entity.OrderEndRemind" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建人姓名" prop="CreatorName">
          <a-input v-model="entity.CreatorName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建日期 默认为当前时间" prop="CreateDate">
          <a-input v-model="entity.CreateDate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人编号" prop="UpdateId">
          <a-input v-model="entity.UpdateId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人时间" prop="UpdateName">
          <a-input v-model="entity.UpdateName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改时间" prop="UpdateDate">
          <a-input v-model="entity.UpdateDate" autocomplete="off" />
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
        this.$http.post('/ServerFood/F_ShopInfoSet/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerFood/F_ShopInfoSet/SaveData', this.entity).then(resJson => {
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
