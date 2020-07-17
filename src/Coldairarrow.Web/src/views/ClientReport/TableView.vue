<template>
  <a-card :loading="loading" :bordered="false" title="订单数量详情" :style="{ height: '100%' }">
    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :loading="loading"
      :bordered="true"
      :pagination="false"
      size="small"
    ></a-table>
  </a-card>
</template>

<script>
const columns = []
export default {
  mounted() {
    console.log('444')
    this.getDataList()
  },
  data() {
    return {
      data: [],
      filters: {},
      sorter: { field: 'Item', order: 'asc' },
      columns,
      loading: true,
      queryParam: {},
      selectedRowKeys: []
    }
  },
  methods: {
    getDataList() {
      this.selectedRowKeys = []
      this.loading = true
      this.$http
        .post('/ServerReport/RP_ReportView/GetOrderDetailList', {
          SearchType: '',
          Day: 1
        })
        .then(resJson => {
          this.loading = false
          // 动态生成列
          for (var key in resJson.Data[0]) {
            var cl = { title: key, dataIndex: key, width: '10%' }
            columns.push(cl)
          }
          this.data = resJson.Data
        })
    }
  }
}
</script>
