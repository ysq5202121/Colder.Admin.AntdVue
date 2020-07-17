<template>
  <v-chart :force-fit="true" :height="405" :data="pieData" :scale="pieScale" padding="auto">
    <v-tooltip :showTitle="false" data-key="Item*percent" />
    <v-axis />
    <!-- position="right" :offsetX="-140" -->
    <v-legend data-key="Item" position="bottom-center" />
    <v-pie position="percent" color="Item" :vStyle="pieStyle" :label="labelConfig" />
    <v-coord type="theta" :radius="0.75" :innerRadius="0.6" />
  </v-chart>
</template>

<script>
const DataSet = require('@antv/data-set')
const pieScale = [
  {
    dataKey: 'percent',
    min: 0,
    formatter: '.0%'
  }
]
export default {
  mounted() {
    const dv = new DataSet.View().source(this.dataSource)
    dv.transform({
      type: 'percent',
      field: 'Count',
      dimension: 'Item',
      as: 'percent'
    })
    this.pieData = dv.rows
  },
  props: {
    // eslint-disable-next-line vue/require-default-prop
    dataSource: Array
  },
  data: function() {
    return {
      pieScale,
      pieData: null,
      pieStyle: {
        stroke: '#fff',
        lineWidth: 1
      },
      labelConfig: [
        'percent',
        {
          formatter: (val, item) => {
            return item.point.Item + ': ' + val
          }
        }
      ]
    }
  },
  methods: {}
}
</script>
