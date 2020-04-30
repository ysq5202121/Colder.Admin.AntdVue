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
        <a-form-model-item label="归属门店" prop="ShopInfoId">
          <a-input v-model="entity.ShopInfoId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="用户名称" prop="UserName">
          <a-input v-model="entity.UserName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="头像" prop="ImgUrl" >
          <img :src="entity.UserImgUrl" width="100" height="100">
        </a-form-model-item>
        <a-form-model-item label="是否管理员" prop="IsAdmin">
          <a-switch v-model="entity.IsAdmin" />
        </a-form-model-item>
        <a-form-model-item label="部门名称" prop="Department">
          <a-input v-model="entity.Department" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="备注信息" prop="Remark">
          <a-input v-model="entity.Remark" autocomplete="off" type="textarea" />
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
        this.$http.post('/ServerFood/F_UserInfo/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerFood/F_UserInfo/SaveData', this.entity).then(resJson => {
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
