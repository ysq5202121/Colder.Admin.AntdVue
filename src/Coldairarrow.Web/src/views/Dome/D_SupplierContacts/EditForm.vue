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
        <a-form-model-item label="供应商ID" prop="SupplierId">
          <a-input v-model="entity.SupplierId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="联系人" prop="Contacts">
          <a-input v-model="entity.Contacts" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="职位" prop="POSITION">
          <a-input v-model="entity.POSITION" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="手机" prop="MobilePhone">
          <a-input v-model="entity.MobilePhone" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="座机" prop="Landline">
          <a-input v-model="entity.Landline" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="邮箱" prop="Email">
          <a-input v-model="entity.Email" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="是否默认" prop="IsDefault">
          <a-input v-model="entity.IsDefault" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建人" prop="CreatorName">
          <a-input v-model="entity.CreatorName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建日期" prop="CreateDate">
          <a-input v-model="entity.CreateDate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人编号" prop="UpdateId">
          <a-input v-model="entity.UpdateId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人" prop="UpdateName">
          <a-input v-model="entity.UpdateName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改时间" prop="UpdateTime">
          <a-input v-model="entity.UpdateTime" autocomplete="off" />
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
        this.$http.post('/Dome/D_SupplierContacts/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/Dome/D_SupplierContacts/SaveData', this.entity).then(resJson => {
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
