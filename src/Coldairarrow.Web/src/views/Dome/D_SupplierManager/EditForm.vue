<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-tabs :default-active-key="1" v-model="selectTabKey">
      <a-tab-pane key="1" tab="基础信息">
        <a-spin :spinning="loading">
          <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
            <a-row>
              <a-col :span="12">
                <a-form-model-item label="供应商全称" prop="SupplierName">
                  <a-input v-model="entity.SupplierName" autocomplete="off" />
                </a-form-model-item>
              </a-col>
              <a-col :span="12">
                <a-form-model-item label="供应商英文名称" prop="SupplierEnName">
                  <a-input v-model="entity.SupplierEnName" autocomplete="off" />
                </a-form-model-item>
              </a-col>
            </a-row>
            <a-row>
              <a-col :span="12">
                <a-form-model-item label="供应商状态" prop="STATUS">
                  <a-input v-model="entity.STATUS" autocomplete="off" />
                </a-form-model-item>
              </a-col>
              <a-col :span="12">
                <a-form-model-item label="供应商类型" prop="SupplierType">
                  <a-select
                    v-model="entity.SupplierType"
                    style="width: 210px"
                    :options="AllStatusList.SupplierType"
                  ></a-select>
                </a-form-model-item>
              </a-col>
            </a-row>
            <a-row>
              <a-col :span="12">
                <a-form-model-item label="所属区域" prop="Region">
                  <a-select
                    v-model="entity.Region"
                    style="width: 210px"
                    :options="AllStatusList.Region"
                  ></a-select>
                </a-form-model-item>
              </a-col>
              <a-col :span="12">
                <a-form-model-item label="城市" prop="City">
                  <a-select
                    v-model="entity.City"
                    style="width: 210px"
                    :options="AllStatusList.City"
                  ></a-select>
                </a-form-model-item>
              </a-col>
            </a-row>
            <a-row>
              <a-col :span="12">
                <a-form-model-item label="供应商代码" prop="SupplierCode">
                  <a-input v-model="entity.SupplierCode" autocomplete="off" />
                </a-form-model-item>
              </a-col>
            </a-row>
            <a-row>
              <a-col :span="12">
                <a-form-model-item label="供应商地址" prop="SupplierAddress">
                  <a-input v-model="entity.SupplierAddress" autocomplete="off" type="textarea" />
                </a-form-model-item>
              </a-col>
            </a-row>
          </a-form-model>
        </a-spin>
      </a-tab-pane>
      <a-tab-pane key="2" tab="联系人" force-render>
        <a-form-model ref="form1" :model="contacts" :rules="contactsRules" v-bind="layout">
          <div v-for="item in contacts" :key="item.key">
            <a-form-model-item label="联系人地址" prop="contacts">
              <a-input v-model="item.contacts" autocomplete="off" />
            </a-form-model-item>
          </div>
        </a-form-model>
      </a-tab-pane>
    </a-tabs>
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
        labelCol: { span: 10 },
        wrapperCol: { span: 14 }
      },
      visible: false,
      loading: false,
      entity: {},
      contacts: {},
      newContacts: {},
      rules: {
        SupplierName: [{ required: true, message: '必填' }],
        SupplierEnName: [{ required: true, message: '必填' }],
        STATUS: [{ required: true, message: '必填' }],
        SupplierType: [{ required: true, message: '必填' }],
        Region: [{ required: true, message: '必填' }],
        City: [{ required: true, message: '必填' }]
      },
      contactsRules: {
        contacts: [{ required: true, message: '必填' }]
      },
      title: '',
      AllStatusList: [],
      selectTabKey: '2',
      SupplierId: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$http.post('/Dome/D_SupplierManager/GetStatusList', {}).then(resJson => {
        if (resJson.Success) {
          this.AllStatusList = resJson.Data
        }
      })
      this.$nextTick(() => {
        if (this.$refs['form']) this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Dome/D_SupplierManager/GetTheData', { id: id }).then(resJson => {
          this.loading = false
          this.entity = resJson.Data
          this.entity.SupplierType = this.entity.SupplierType.toString()
        })
        this.getContactsList(id)
        this.SupplierId = id
      }
    },
    getContactsList(id) {
      this.loading = true
      this.$http.post('/Dome/D_SupplierContacts/GetDataListById', { Keyword: id }).then(resJson => {
        this.loading = false
        this.contacts = { dataList: resJson.Data }
      })
    },
    handleSubmit() {
      if (this.selectTabKey === '1') {
        this.$refs['form'].validate(valid => {
          if (!valid) {
            return
          }
          this.loading = true
          this.$http.post('/Dome/D_SupplierManager/SaveData', this.entity).then(resJson => {
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
      if (this.selectTabKey === '2') {
        this.$refs['form1'].validate(valid => {
          if (!valid) {
            return
          }
          this.loading = true
          this.newContacts.SupplierId = this.SupplierId

          this.$http.post('/Dome/D_SupplierContacts/SaveData', this.newContacts).then(resJson => {
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
}
</script>
