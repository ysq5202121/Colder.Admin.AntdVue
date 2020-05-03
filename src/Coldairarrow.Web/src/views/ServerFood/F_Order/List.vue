<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
      <a-date-picker
        placeholder="请选择导出日期"
        v-model="toDay"
        :defaultValue="defaultToDay"
        valueFormat="YYYY-MM-DD"
      />
      <a-button type="primary" icon="download" @click="handleExcelExport()">导出</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="OrderCode">订单编号</a-select-option>
                <a-select-option key="UserName">用户ID</a-select-option>
                <a-select-option key="CreatorName">创建人姓名</a-select-option>
                <a-select-option key="UpdateId">修改人编号</a-select-option>
                <a-select-option key="UpdateName">修改人时间</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="getDataList">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.OrderCode"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
      @expand="HandleExpandedRowsChange"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>

      <a-table
        slot="expandedRowRender"
        slot-scope="record"
        :columns="innerColumns"
        :dataSource="innerData.filter(a=>a.OrderCode===record.OrderCode)"
        :pagination="false"
        :rowKey="row => row.Id"
        :bordered="true"
      ></a-table>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'
import moment from 'moment'

const columns = [
  { title: '订单编号', dataIndex: 'OrderCode' },
  { title: '用户', dataIndex: 'UserName', width: 150 },
  { title: '数量', dataIndex: 'OrderCount', width: 150 },
  { title: '订单金额', dataIndex: 'Price', width: 150 },
  { title: '创建人', dataIndex: 'CreatorName', width: 150 },
  { title: '创建时间', dataIndex: 'CreateTime', width: 150 },
  { title: '修改人', dataIndex: 'UpdateName', width: 150 },
  { title: '修改时间', dataIndex: 'UpdateTime', width: 150 },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' }, width: 100 }
]
const innerColumns = [
  { title: '商品名称', dataIndex: 'FoodName' },
  { title: '商品数量', dataIndex: 'OrderInfoQty' }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'Id', order: 'desc' },
      loading: false,
      ChLoading: false,
      columns,
      innerColumns,
      queryParam: {},
      selectedRowKeys: [],
      innerData: [{ OrderCode: '' }],
      defaultToDay: moment(new Date()),
      toDay: moment(new Date())
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []

      this.loading = true
      this.$http
        .post('/ServerFood/F_Order/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    handleExcelExport() {
      if (this.toDay === null) {
        this.toDay = moment(new Date())
      }
      this.$http
        .post(
          '/ServerFood/F_Order/ExcelToExport',
          {
            Condition: 'CreateTime',
            Keyword: this.toDay.format('YYYY-MM-DD')
          },
          { responseType: 'arraybuffer' }
        )
        .then(resJson => {
          const blob = new Blob([resJson], { type: 'application/vnd.ms-excel' })
          const downloadElement = document.createElement('a')
          const href = window.URL.createObjectURL(blob) // 创建下载的链接
          downloadElement.href = href
          // downloadElement.download = fileName; //下载后文件名
          downloadElement.download = '下载订单' + this.toDay.format('YYYY年MM月DD日') // 下载后文件名
          document.body.appendChild(downloadElement)
          downloadElement.click() // 点击下载
          document.body.removeChild(downloadElement) // 下载完成移除元素
          window.URL.revokeObjectURL(href) // 释放掉blob对象
        })
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/ServerFood/F_Order/DeleteData', ids).then(resJson => {
              resolve()

              if (resJson.Success) {
                thisObj.$message.success('操作成功!')

                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        }
      })
    },
    HandleExpandedRowsChange(expanded, record) {
      if (expanded) {
        this.ChLoading = true
        if (this.innerData.length === 0 || !this.innerData.some(a => a.OrderCode === record.OrderCode)) {
          this.$http
            .post('/ServerFood/F_OrderInfo/GetDataListNoPage', {
              condition: 'OrderCode',
              keyword: record.OrderCode
            })
            .then(resJson => {
              this.ChLoading = false
              resJson.Data.forEach(a => {
                this.innerData.push(a)
              })
            })
        }
      }
    }
  }
}
</script>
