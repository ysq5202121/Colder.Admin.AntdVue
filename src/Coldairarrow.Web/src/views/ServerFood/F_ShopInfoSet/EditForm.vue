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
          <a-date-picker v-model="entity.OrderBeginDate" type="date" showTime />
        </a-form-model-item>
        <a-form-model-item label="点餐结束时间" prop="OrderBeginEnd">
          <a-date-picker v-model="entity.OrderBeginEnd" type="date" showTime />
        </a-form-model-item>
        <a-form-model-item label="单用户可下订单数量" prop="UserOrderNum">
          <a-input-number v-model="entity.UserOrderNum" :min="1" :max="999" />
        </a-form-model-item>
        <a-form-model-item label="订单可含商品SKU数" prop="OrderFoodTypeNum">
          <a-input-number v-model="entity.OrderFoodTypeNum" :min="1" :max="9999" />
        </a-form-model-item>
        <a-form-model-item label="开始点餐提醒" prop="OrderBeginRemind">
          <a-input v-model="entity.OrderBeginRemind" autocomplete="off" type="textarea" />
        </a-form-model-item>
        <a-form-model-item label="结束点餐提醒" prop="OrderEndRemind">
          <a-input v-model="entity.OrderEndRemind" autocomplete="off" type="textarea" />
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
          if (this.entity['OrderBeginDate']) {
            this.entity['OrderBeginDate'] = moment(this.entity['OrderBeginDate'])
          }
          if (this.entity['OrderBeginEnd']) {
            this.entity['OrderBeginEnd'] = moment(this.entity['OrderBeginEnd'])
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
    }
  }
}
</script>
