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
        <a-form-model-item label="办公楼ID" prop="OfficeId">
          <a-input v-model="entity.OfficeId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议室名称" prop="ConferenceRoomName">
          <a-input v-model="entity.ConferenceRoomName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议室说明" prop="Description">
          <a-input v-model="entity.Description" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="预约会议地点" prop="Place">
          <a-input v-model="entity.Place" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="时段" prop="TimeInterval">
          <a-input v-model="entity.TimeInterval" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="容纳人数" prop="Capacity">
          <a-input v-model="entity.Capacity" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议标签属性" prop="RoomAttribute">
          <a-input v-model="entity.RoomAttribute" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议图片" prop="RommImage">
          <a-input v-model="entity.RommImage" autocomplete="off" />
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
        this.$http.post('/ServerRoom/C_ConferenceRoom/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ServerRoom/C_ConferenceRoom/SaveData', this.entity).then(resJson => {
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
