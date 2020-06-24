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
        <a-form-model-item label="点餐开始时间" prop="OrderBeginDate">
          <a-date-picker
            v-model="entity.OrderBeginDate"
            type="date"
            showTime
            format="HH:mm"
            @openChange="handleOpenChange1"
            @panelChange="handlePanelChange1"
            :mode="showTime1"
          />
        </a-form-model-item>
        <a-form-model-item label="点餐结束时间" prop="OrderBeginEnd">
          <a-date-picker
            v-model="entity.OrderBeginEnd"
            type="date"
            showTime
            format="HH:mm"
            @openChange="handleOpenChange1"
            @panelChange="handlePanelChange1"
            :mode="showTime1"
          />
        </a-form-model-item>
        <a-form-model-item label="领餐时间" prop="OrderReceiveDate">
          <a-date-picker
            v-model="entity.OrderReceiveDate"
            type="date"
            showTime
            format="HH:mm"
            @openChange="handleOpenChange1"
            @panelChange="handlePanelChange1"
            :mode="showTime1"
          />
        </a-form-model-item>
        <a-form-model-item prop="UserOrderNum">
          <span slot="label">
            订单数量
            <a-tooltip title="每个用户每一天可以提交的订单数量(如果不设置则不限制)">
              <a-icon type="question-circle-o" />
            </a-tooltip>
          </span>
          <a-input-number
            v-model="entity.UserOrderNum"
            :min="1"
            :max="999"
            placeholder="为空不生效"
            style="width:200px"
          />
        </a-form-model-item>
        <a-form-model-item prop="OrderFoodTypeNum">
          <span slot="label">
            订单SKU数
            <a-tooltip title="每个用户每个订单可以包含多少种商品(如果不设置则不限制)">
              <a-icon type="question-circle-o" />
            </a-tooltip>
          </span>
          <a-input-number
            v-model="entity.OrderFoodTypeNum"
            :min="1"
            :max="9999"
            placeholder="为空不生效"
            style="width:200px"
          />
        </a-form-model-item>
        <a-form-model-item prop="IsRandomSendReceiveMsg">
          <span slot="label">
            随机发送领餐信息
            <a-tooltip title="如果开启则设置的领餐信息无效">
              <a-icon type="question-circle-o" />
            </a-tooltip>
          </span>
          <a-switch v-model="entity.IsRandomSendReceiveMsg" />
        </a-form-model-item>
        <a-form-model-item label="开始点餐提醒" prop="OrderBeginRemind">
          <a-input
            v-model="entity.OrderBeginRemind"
            autocomplete="off"
            type="textarea"
            placeholder="为空则不会发送消息"
          />
        </a-form-model-item>
        <a-form-model-item label="结束点餐提醒" prop="OrderEndRemind">
          <a-input
            v-model="entity.OrderEndRemind"
            autocomplete="off"
            type="textarea"
            placeholder="为空则不会发送消息"
          />
        </a-form-model-item>
        <a-form-model-item label="领餐提醒信息" prop="OrderReceiveRemind">
          <a-input
            v-model="entity.OrderReceiveRemind"
            autocomplete="off"
            type="textarea"
            placeholder="为空则不会发送消息"
          />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment'
export default {
  props: {
    parentObj: Object
  },
  data() {
    const validateOrderBeginDate = (rule, value, callback) => {
      if (value === null || value === '') {
        callback(new Error('请选择时间段'))
      } else {
        if (this.entity.OrderBeginEnd !== '') {
          this.$refs.form.validateField('OrderBeginEnd')
        }
        callback()
      }
    }
    const validateOrderBeginEnd = (rule, value, callback) => {
      if (value === null || value === '') {
        callback(new Error('请选择时间段'))
      } else {
        const OrderBeginDate = moment(this.entity.OrderBeginDate).format('HHmm')
        const OrderBeginEnd = moment(this.entity.OrderBeginEnd).format('HHmm')
        if (OrderBeginDate > OrderBeginEnd) {
          callback(new Error('结束时间不能大于起始时间'))
        } else {
          this.$refs.form.validateField('OrderReceiveDate')
        }
        callback()
      }
    }

    const validateOrderReceiveDate = (rule, value, callback) => {
      if (value === null || value === '') {
        callback(new Error('请选择时间段'))
      } else {
        const OrderBeginEnd = moment(this.entity.OrderBeginEnd).format('HHmm')
        const OrderReceiveDate = moment(this.entity.OrderReceiveDate).format('HHmm')
        if (OrderBeginEnd > OrderReceiveDate) {
          callback(new Error('结束时间不能大于领取时间'))
        }
        callback()
      }
    }

    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      showTime1: 'time',
      rules: {
        OrderBeginDate: [
          { required: true, message: '请选择时间段', trigger: 'blur' },
          { validator: validateOrderBeginDate, trigger: 'blur', type: Date }
        ],
        OrderBeginEnd: [
          { required: true, message: '请选择时间段', trigger: 'blur' },
          { validator: validateOrderBeginEnd, trigger: 'blur', type: Date }
        ],
        OrderReceiveDate: [
          { required: true, message: '请选择时间段', trigger: 'blur' },
          { validator: validateOrderReceiveDate, trigger: 'blur', type: Date }
        ]
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
        this.$http.post('/ServerFood/F_ShopInfoSet/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
          if (this.entity['OrderBeginDate']) {
            this.entity['OrderBeginDate'] = moment(this.entity['OrderBeginDate'])
          }
          if (this.entity['OrderBeginEnd']) {
            this.entity['OrderBeginEnd'] = moment(this.entity['OrderBeginEnd'])
          }
          if (this.entity['OrderReceiveDate']) {
            this.entity['OrderReceiveDate'] = moment(this.entity['OrderReceiveDate'])
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
    },
    handleOpenChange1(open) {
      if (open) {
        this.showTime1 = 'time'
      }
    },
    handlePanelChange1(value, mode) {
      this.showTime1 = mode
    }
  }
}
</script>
