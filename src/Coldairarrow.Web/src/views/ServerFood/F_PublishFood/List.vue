<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >删除</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="SupplierName">商家名称</a-select-option>
                <a-select-option key="FoodName">菜品名称</a-select-option>
                <a-select-option key="CreatorName">创建人姓名</a-select-option>
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
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record.Id)">编辑</a>
        </template>
      </span>
      <span slot="ImgUrl" slot-scope="image">
        <a-avatar
          icon="picture"
          :src="image"
          size="large"
          shape="square"
          style="cursor:pointer"
          @click="handleOpenImg(image)"
        />
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
    <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
      <img alt="example" style="width: 100%" :src="previewImage" />
    </a-modal>
  </a-card>
</template>

<script>
import EditForm from './EditForm'

const columns = [
  { title: '门店名称', dataIndex: 'ShopName', width: 100 },
  { title: '商家名称', dataIndex: 'SupplierName', width: 100 },
  { title: '菜品名称', dataIndex: 'FoodName' },
  { title: '菜品数量', dataIndex: 'FoodQty', width: 100 },
  { title: '价格', dataIndex: 'Price', width: 100 },
  { title: '图片', dataIndex: 'ImgUrl', width: 50, scopedSlots: { customRender: 'ImgUrl' } },
  { title: '发布时间', dataIndex: 'PublishDate', width: 150 },
  { title: '创建人', dataIndex: 'CreatorName', width: 100 },
  { title: '创建时间', dataIndex: 'CreateTime', width: 150, sorter: true },
  { title: '修改人', dataIndex: 'UpdateName', width: 100 },
  { title: '修改时间', dataIndex: 'UpdateTime', width: 150 },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' }, fixed: 'right', width: 100 }
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
      sorter: { field: 'CreateTime', order: 'desc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: [],
      previewVisible: false,
      previewImage: ''
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = Object.assign(this.sorter, { ...sorter })
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []
      this.loading = true
      this.$http
        .post('/ServerFood/F_PublishFood/GetDataList', {
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
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/ServerFood/F_PublishFood/DeleteData', ids).then(resJson => {
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
    handleCancel() {
      this.previewVisible = false
    },
    handleOpenImg(image) {
      this.previewVisible = true
      this.previewImage = image
    }
  }
}
</script>
