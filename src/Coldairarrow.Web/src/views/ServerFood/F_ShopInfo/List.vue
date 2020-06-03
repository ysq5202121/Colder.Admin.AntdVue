<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
        v-if="hasPerm('F_ShopInfo.Delete')"
      >删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="ShopName">门店名称</a-select-option>
                <a-select-option key="ShopDesc">门店描述</a-select-option>
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
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange ,columnWidth:50 }"
      :bordered="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a download="true" :href="getBarCodeUrl+'&id='+record.Id">二维码</a>
          <a-divider type="vertical" v-if="hasPerm('F_ShopInfo.Delete')" />
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" v-if="hasPerm('F_ShopInfo.Delete')" />
          <a @click="handleDelete([record.Id])" v-if="hasPerm('F_ShopInfo.Delete')">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'
import defaultSettings from '@/config/defaultSettings'
import ProcessHelper from '@/utils/helper/ProcessHelper'
const columns = [
  { title: '门店名称', dataIndex: 'ShopName', width: 200 },
  { title: '门店描述', dataIndex: 'ShopDesc' },
  { title: '创建人', dataIndex: 'CreatorName', width: 100 },
  { title: '创建时间', dataIndex: 'CreateTime', width: 200 },
  { title: '修改人', dataIndex: 'UpdateName', width: 100 },
  { title: '修改时间', dataIndex: 'UpdateTime', width: 200 },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' }, fixed: 'right', width: 150 }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
  },
  computed: {
    getBarCodeUrl() {
      if (ProcessHelper.isProduction() || ProcessHelper.isPreview()) {
        return (
          defaultSettings.publishRootUrl + '/ServerFood/F_ShopInfo/GetBarCode?url=' + defaultSettings.publishRootUrl
        )
      } else {
        return defaultSettings.localRootUrl + '/ServerFood/F_ShopInfo/GetBarCode?url=' + defaultSettings.localRootUrl
      }
    }
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
      selectedRowKeys: []
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
        .post('/ServerFood/F_ShopInfo/GetDataList', {
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
            thisObj.$http.post('/ServerFood/F_ShopInfo/DeleteData', ids).then(resJson => {
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
    }
  }
}
</script>
