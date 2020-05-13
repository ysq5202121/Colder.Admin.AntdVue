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
        <a-form-model-item label="会议室ID" prop="ConferenceRoomId">
          <a-input v-model="entity.ConferenceRoomId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议主题" prop="MeetingTheme">
          <a-input v-model="entity.MeetingTheme" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议说明" prop="MeetingNotes">
          <a-input v-model="entity.MeetingNotes" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="预约会议状态" prop="STATUS">
          <a-input v-model="entity.STATUS" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="预约时间" prop="AppointmentTime">
          <a-input v-model="entity.AppointmentTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议通知" prop="IsNotice">
          <a-input v-model="entity.IsNotice" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议附件" prop="Attachment">
          <a-input v-model="entity.Attachment" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="创建人姓名" prop="CreateName">
          <a-input v-model="entity.CreateName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人编号" prop="UpdateId">
          <a-input v-model="entity.UpdateId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改时间" prop="UpdateTime">
          <a-input v-model="entity.UpdateTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="修改人时间" prop="UpdateName">
          <a-input v-model="entity.UpdateName" autocomplete="off" />
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
        this.$http.post('/ServerRoom/C_MakeConferenceRoom/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerRoom/C_MakeConferenceRoom/SaveData', this.entity).then(resJson => {
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
