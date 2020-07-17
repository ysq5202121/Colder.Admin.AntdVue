<template>
  <div>
    <div>平台|团队</div>
    <div>
      <v-chart :force-fit="true" :height="405" :data="pieData" :scale="pieScale" padding="auto">
        <v-tooltip :showTitle="false" data-key="item*percent" />
        <v-axis />
        <!-- position="right" :offsetX="-140" -->
        <v-legend data-key="item" position="right-center" :offsetX="-300"/>
        <v-pie position="percent" color="item" :vStyle="pieStyle" />
        <v-coord type="theta" :radius="0.75" :innerRadius="0.6" />
      </v-chart>
    </div>
  </div>
</template>

<script>
const DataSet = require('@antv/data-set')
const sourceData = [
  { item: '家用电器', count: 32.2 },
  { item: '食用酒水', count: 21 },
  { item: '个护健康', count: 17 },
  { item: '服饰箱包', count: 13 },
  { item: '母婴产品', count: 119 },
  { item: '其他', count: 7.8 },
  { item: '瞎了', count: 7.8 }
]
const pieScale = [
  {
    dataKey: 'percent',
    min: 0,
    formatter: '.0%'
  }
]
const dv = new DataSet.View().source(sourceData)
dv.transform({
  type: 'percent',
  field: 'count',
  dimension: 'item',
  as: 'percent'
})
const pieData = dv.rows
export default {
  data: function() {
    return {
      pieScale,
      pieData,
      sourceData,
      pieStyle: {
        stroke: '#fff',
        lineWidth: 1
      }
    }
  },
  methods: {
    goToOrderView() {
      this.$router.push({ path: '/ClientFood/Order' })
    },
    goToOrderListView() {
      this.$router.push({ path: '/ClientFood/OrderList' })
    }
  }
}
</script>
