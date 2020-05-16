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
        <a-form-model-item label="办公楼" prop="OfficeId">
          <a-select v-model="entity.OfficeId" style="width: 200px">
            <a-select-option v-for="office in OfficeList" :key="office.Id">{{ office.OfficeName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="会议室名称" prop="ConferenceRoomName">
          <a-input v-model="entity.ConferenceRoomName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="会议室说明" prop="Description">
          <a-input v-model="entity.Description" autocomplete="off" type="textarea" />
        </a-form-model-item>
        <a-form-model-item label="预约地点" prop="Place">
          <a-input v-model="entity.Place" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="时段">
          <a-time-picker format="HH:mm" :minute-step="30" v-model="BeginTimeSlot" />-
          <a-time-picker format="HH:mm" :minute-step="30" v-model="EndTimeSlot" />
        </a-form-model-item>
        <a-form-model-item label="容纳人数" prop="Capacity">
          <a-input-number v-model="entity.Capacity" :min="1" :max="9999" />
        </a-form-model-item>
        <a-form-model-item label="会议室属性" prop="RoomAttribute">
          <template v-for="(tag, index) in tags">
            <a-tooltip v-if="tag.length > 20" :key="tag" :title="tag">
              <a-tag
                :key="tag"
                :closable="index !== 0"
                @close="() => handleClose(tag)"
              >{{ `${tag.slice(0, 20)}...` }}</a-tag>
            </a-tooltip>
            <a-tag
              v-else
              :key="tag"
              :color="color[index>color.length?color.length-1:index]"
              :closable="true"
              @close="() => handleClose(tag)"
            >{{ tag }}</a-tag>
          </template>
          <a-input
            v-if="inputVisible"
            ref="input"
            type="text"
            size="small"
            :style="{ width: '78px' }"
            :value="inputValue"
            @change="handleInputChange"
            @blur="handleInputConfirm"
            @keyup.enter="handleInputConfirm"
          />
          <a-tag v-else style="background: #fff; borderStyle: dashed;" @click="showInput">
            <a-icon type="plus" />添加
          </a-tag>
        </a-form-model-item>
        <a-form-model-item label="会议室图片" prop="RommImage">
          <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
          <c-upload-img ref="upload" v-model="Rommimg" :maxCount="1"></c-upload-img>
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment'
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
        OfficeId: [{ required: true, message: '必填' }],
        ConferenceRoomName: [{ required: true, message: '必填' }],
        TimeInterval: [{ required: true, message: '必填' }],
        Capacity: [{ required: true, message: '必填' }]
      },
      title: '',
      OfficeList: [],
      Rommimg: '',
      tags: ['电视机', '投影仪', '笔记本'],
      inputVisible: false,
      inputValue: '',
      BeginTimeSlot: undefined,
      EndTimeSlot: undefined,
      color: ['pink', 'red', 'orange', 'green', 'cyan', 'purple', 'blue']
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.Rommimg = ''
      this.$http.post('/ServerRoom/C_Office/GetDataListAll', {}).then(resJson => {
        if (resJson.Success) {
          this.OfficeList = resJson.Data
        }
      })
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    moment,
    getcolor() {
      return '#' + Math.round(Math.random() * 0x1000000).toString(16)
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/ServerRoom/C_ConferenceRoom/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
          if (this.entity['RommImage']) {
            this.Rommimg = this.entity.RommImage
          }
          this.tags = this.entity.RoomAttribute.split(',')
          if (this.entity['BeginTimeSlot']) {
            this.BeginTimeSlot = moment(this.entity['BeginTimeSlot'], 'HH:mm')
            console.log(this.BeginTimeSlot)
          }
          if (this.entity['EndTimeSlot']) {
            this.EndTimeSlot = moment(this.entity['EndTimeSlot'], 'HH:mm')
          }
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.entity.RommImage = this.Rommimg
        this.entity.RoomAttribute = this.tags.join()
        this.entity.BeginTimeSlot = moment(this.BeginTimeSlot).format('HH:mm')
        this.entity.EndTimeSlot = moment(this.EndTimeSlot).format('HH:mm')
        console.log(this.entity.BeginTimeSlot)
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
    },
    handleCancel() {
      this.visible = false
      this.$refs['upload'].initforceUpdate()
    },
    handleClose(removedTag) {
      const tags = this.tags.filter(tag => tag !== removedTag)
      console.log(tags)
      this.tags = tags
    },

    showInput() {
      this.inputVisible = true
      this.$nextTick(function() {
        this.$refs.input.focus()
      })
    },

    handleInputChange(e) {
      this.inputValue = e.target.value
    },

    handleInputConfirm() {
      const inputValue = this.inputValue
      let tags = this.tags
      if (inputValue && tags.indexOf(inputValue) === -1) {
        tags = [...tags, inputValue]
      }
      Object.assign(this, {
        tags,
        inputVisible: false,
        inputValue: ''
      })
    }
  }
}
</script>
