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
        <a-form-model-item label="键" prop="DicKey">
          <a-input v-model="entity.DicKey" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="值" prop="DicValue">
          <a-input v-model="entity.DicValue" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="显示值" prop="DicDisplayValue">
          <a-input v-model="entity.DicDisplayValue" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="描述" prop="DicDesc">
          <a-input v-model="entity.DicDesc" autocomplete="off" type="textarea" />
        </a-form-model-item>
        <a-form-model-item label="字典类型" prop="STATUS">
          <a-input v-model="entity.STATUS" autocomplete="off" />
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
      rules: {
        DicKey: [{ required: true, message: '必填' }],
        DicValue: [{ required: true, message: '必填' }],
        DicDesc: [{ required: true, message: '必填' }],
        DicDisplayValue: [{ required: true, message: '必填' }],
        STATUS: [{ required: true, message: '必填' }]
      },
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
        this.$http.post('/Base_Manage/Base_Dictionary/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/Base_Manage/Base_Dictionary/SaveData', this.entity).then(resJson => {
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
