﻿<template>
  <a-modal
    :title="title"
    width="50%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-tabs :default-active-key="1" v-model="selectTabKey">
      <a-tab-pane key="1" tab="基础信息">
        <a-spin :spinning="loading">
          <div class="table-page-search-wrapper">
            <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout" layout="inline">
              <a-row :gutter="48">
                <a-col :span="12">
                  <a-form-model-item label="供应商全称" prop="SupplierName">
                    <a-input v-model="entity.SupplierName" autocomplete="off" />
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item label="供英文名称" prop="SupplierEnName">
                    <a-input v-model="entity.SupplierEnName" autocomplete="off" />
                  </a-form-model-item>
                </a-col>

                <a-col :span="12">
                  <a-form-model-item label="供应商状态" prop="STATUS">
                    <a-select v-model="entity.STATUS" :options="AllStatusList.Status"></a-select>
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item label="供应商类型" prop="SupplierType">
                    <a-select v-model="entity.SupplierType" :options="AllStatusList.SupplierType"></a-select>
                  </a-form-model-item>
                </a-col>

                <a-col :span="12">
                  <a-form-model-item label="所属区域" prop="Region">
                    <a-select v-model="entity.Region" :options="AllStatusList.Region"></a-select>
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item label="城市" prop="City">
                    <a-select v-model="entity.City" :options="AllStatusList.City"></a-select>
                  </a-form-model-item>
                </a-col>

                <a-col :span="24">
                  <a-form-model-item prop="SupplierCode">
                    <span slot="label">
                      供应商代码
                      <a-tooltip title="显示什么好啊?">
                        <a-icon type="question-circle-o" />
                      </a-tooltip>
                    </span>
                    <a-input v-model="entity.SupplierCode" autocomplete="off" />
                  </a-form-model-item>
                </a-col>

                <a-col :span="24">
                  <a-form-model-item prop="SupplierAddress" label="供应商地址">
                    <a-input v-model="entity.SupplierAddress" autocomplete="off" type="textarea" />
                  </a-form-model-item>
                </a-col>
              </a-row>
            </a-form-model>
          </div>
        </a-spin>
      </a-tab-pane>
      <a-tab-pane key="2" tab="联系人" force-render>
        <a-form-model ref="form1" :model="contacts" v-bind="layout">
          <div v-for="(item,index) in contacts.dataList" :key="item.key">
            <a-card size="small">
              <template #title>
                <a-icon type="heart" theme="twoTone" two-tone-color="#eb2f96" />联系信息
              </template>
              <template slot="extra">
                <a-button
                  type="link"
                  @click="SetDefault(item,index)"
                  v-if="item.Id"
                >{{ item.IsDefault==true?'取消默认':'设置默认' }}</a-button>
                <a-button type="link" @click="addOrDelContacts(item,index)" v-if="index===0">新增</a-button>
                <a-popconfirm
                  v-if="index>0"
                  title="确定要删除吗?"
                  ok-text="确定"
                  cancel-text="取消"
                  @confirm="addOrDelContacts(item,index)"
                >
                  <a href="#">删除</a>
                </a-popconfirm>
              </template>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item
                    label="联系人地址"
                    :prop="'dataList.' + index + '.Contacts'"
                    :rules="contactsRules.Contacts"
                  >
                    <a-input v-model="item.Contacts" autocomplete="off" />
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item
                    label="部门"
                    :prop="'dataList.' + index + '.POSITION'"
                    :rules="contactsRules.Contacts"
                  >
                    <a-input v-model="item.POSITION" autocomplete="off">
                      <a-tooltip slot="suffix" title="注视下">
                        <a-icon type="info-circle" style="color: rgba(0,0,0,.45)" />
                      </a-tooltip>
                    </a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>

              <a-row>
                <a-col :span="12">
                  <a-form-model-item
                    label="联系座机"
                    :prop="'dataList.' + index + '.Landline'"
                    :rules="contactsRules.Contacts"
                  >
                    <a-input v-model="item.Landline" autocomplete="off" />
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item
                    label="联系手机"
                    :prop="'dataList.' + index + '.MobilePhone'"
                    :rules="contactsRules.Contacts"
                  >
                    <a-input v-model="item.MobilePhone" autocomplete="off">
                      <a-select slot="addonBefore" default-value="+86" style="width: 90px">
                        <a-select-option value="+86">+86</a-select-option>
                        <a-select-option value="+87">+87</a-select-option>
                      </a-select>
                    </a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item
                    label="联系人邮箱"
                    :prop="'dataList.' + index + '.Email'"
                    :rules="contactsRules.Email"
                  >
                    <a-input v-model="item.Email" autocomplete="off" />
                  </a-form-model-item>
                </a-col>
              </a-row>
            </a-card>
            <p></p>
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
        labelCol: { span: 7 }
      },
      layoutContacts: {
        labelCol: { span: 7 },
        wrapperCol: { span: 14 }
      },
      layout1: {
        labelCol: { span: 4 },
        wrapperCol: { span: 19 }
      },
      visible: false,
      loading: false,
      entity: {},
      contacts: {},
      rules: {
        SupplierName: [{ required: true, message: '必填' }],
        SupplierEnName: [{ required: true, message: '必填' }],
        STATUS: [{ required: true, message: '必填' }],
        SupplierType: [{ required: true, message: '必填' }],
        Region: [{ required: true, message: '必填' }],
        City: [{ required: true, message: '必填' }]
      },
      contactsRules: {
        Contacts: [{ required: true, message: '必填' }],
        Email: [
          { required: true, message: '必填' },
          { message: '请输入邮箱', type: 'email' }
        ]
      },
      title: '',
      AllStatusList: [],
      selectTabKey: '1',
      SupplierId: ''
    }
  },
  methods: {
    init(id) {
      this.visible = true
      this.selectTabKey = '1'
      this.entity = {}
      this.contacts = {}
      this.$http.post('/Dome/D_SupplierManager/GetStatusList', {}).then(resJson => {
        if (resJson.Success) {
          this.AllStatusList = resJson.Data
        }
      })
      this.$nextTick(() => {
        if (this.$refs['form']) this.$refs['form'].clearValidate()
        if (this.$refs['form1']) this.$refs['form1'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init(id)

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
        if (resJson.Data.length === 0) {
          this.contacts = {
            dataList: [{}]
          }
        } else {
          this.contacts.dataList.sort((a, b) => {
            return b.IsDefault - a.IsDefault
          })
        }
      })
    },
    addOrDelContacts(item, index) {
      if (index === 0) {
        this.contacts.dataList.push({ SupplierId: this.SupplierId })
      } else {
        this.contacts.dataList.splice(index, 1)
      }
    },
    SetDefault(item, index) {
      this.loading = true
      this.$http.post('/Dome/D_SupplierContacts/SetDefault', item).then(resJson => {
        this.loading = false

        if (resJson.Success) {
          this.$message.success('操作成功!')
          this.contacts.dataList
            .filter(a => a.IsDefault === true)
            .forEach(a => {
              a.IsDefault = false
            })
          item.IsDefault = true
          this.contacts.dataList.sort((a, b) => {
            return b.IsDefault - a.IsDefault
          })
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleSubmit() {
      if (this.selectTabKey === '1') {
        this.$refs['form'].validate((valid, obj) => {
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
        this.$refs['form1'].validate((valid, obj) => {
          if (!valid) {
            return
          }
          this.loading = true
          this.contacts.dataList.forEach(a => {
            if (!a.SupplierId) a.SupplierId = this.SupplierId
          })
          this.$http.post('/Dome/D_SupplierContacts/SaveDataList', this.contacts.dataList).then(resJson => {
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
