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
        <a-form-model-item label="部门" prop="Department">
          <a-select v-model="entity.Department" mode="tags">
            <a-select-option v-for="item in DepartmentList" :key="item">{{ item }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="对应部门" prop="OldDepartment">
          <a-input v-model="entity.OldDepartment" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="备注" prop="Remark">
          <a-input v-model="entity.Remark" autocomplete="off" />
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
    const validateDepartment = (rule, value, callback) => {
      if (value.length > 1) {
        callback(new Error('不能选择多个'))
      } else {
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
      DepartmentList: {},
      rules: {
        Department: [
          { required: true, message: '必填', trigger: 'change' }
          // { validator: validateDepartment, trigger: 'change' }
        ],
        OldDepartment: [{ required: true, message: '必填' }]
      },
      title: '编辑'
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$http.post('/Base_Manage/Base_DepartmentRelation/GetNewDepartmentList').then(resJson => {
        this.DepartmentList = resJson.Data
      })
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Base_Manage/Base_DepartmentRelation/GetTheData', { id: id }).then(resJson => {
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
        if (!(this.entity.Department instanceof Array)) {
          this.entity.Department = Array.of(this.entity.Department)
        }
        this.$http.post('/Base_Manage/Base_DepartmentRelation/SaveDataList', this.entity).then(resJson => {
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
