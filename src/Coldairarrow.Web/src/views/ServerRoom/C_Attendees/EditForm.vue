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
        <a-form-model-item label="会议ID" prop="MakeConferenceRoomId">
          <a-input v-model="entity.MakeConferenceRoomId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="用户ID" prop="UserWeChatID">
          <a-input v-model="entity.UserWeChatID" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="用户名称" prop="UserWeChatName">
          <a-input v-model="entity.UserWeChatName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="用户头像" prop="UserWeChatImg">
          <a-input v-model="entity.UserWeChatImg" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="用户部门信息" prop="UserWeChatDepartment">
          <a-input v-model="entity.UserWeChatDepartment" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="与会人状态" prop="STATUS">
          <a-input v-model="entity.STATUS" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="与会人员类型" prop="AttendeesType">
          <a-input v-model="entity.AttendeesType" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建人姓名" prop="CreateName">
          <a-input v-model="entity.CreateName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人编号" prop="UpdateId">
          <a-input v-model="entity.UpdateId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人时间" prop="UpdateName">
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
        this.$http.post('/ServerRoom/C_Attendees/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerRoom/C_Attendees/SaveData', this.entity).then(resJson => {
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
