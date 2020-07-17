<template>
  <div v-if="dataSource.length">
    <v-chart :forceFit="true" height="400" :data="dataSource" :scale="scale">
      <v-tooltip></v-tooltip>
      <v-legend></v-legend>
      <!-- <v-legend
        :useHtml="true"
        position="right"
        :reactive="true"
        :legendStyle="legendStyle"
        :containerTpl="containerTpl"
        :itemTpl="itemTpl"
      ></v-legend>-->
      <v-line position="OrderDate*OrderNumber" :color="color"></v-line>
      <v-axis data-key="OrderDate" :label="label"></v-axis>
      <v-axis data-key="OrderNumber" :label="labelFormat"></v-axis>
    </v-chart>
  </div>
</template>

<script>

const label = {
  textStyle: {
    fill: '#aaaaaa'
  }
}

const labelFormat = {
  textStyle: {
    fill: '#aaaaaa'
  },
  formatter: function formatter(text) {
    return text.replace(/(\d)(?=(?:\d{3})+$)/g, '$1,')
  }
}

const containerTpl = '<div class="g2-legend">2016 年支出排行<div class="g2-legend-list"></div></div>'

const itemTpl = function itemTpl(value, color, checked, index) {
  var markerDom = '<div class="g2-legend-marker" style="background-color:' + color + '">' + (index + 1) + '</div>'
  var nameDom = '<div class="legend-item-name">' + value + '</div>'
  var percentDom = '<div class="legend-item-percent">' + this.yearPercent[value].percent.toFixed(2) + '</div>'
  var valueDom = '<div class="legend-item-value">' + this.yearPercent[value].value + '</div>'
  return (
    '<div class="g2-legend-list-item" data-value="' +
    value +
    '">' +
    markerDom +
    nameDom +
    percentDom +
    valueDom +
    '</div>'
  )
}
export default {
  props: {
    // eslint-disable-next-line vue/require-default-prop
    dataSource: Array
  },
  mounted() {
    const yearSum = this.getYearsum()
    this.yearPercent = this.getYearPercent(yearSum)
    this.setStyle()
  },
  methods: {
    setStyle() {
      const id = 'legend-html'
      if (document.getElementById(id)) {
        return
      }
      const styleTxt = `
        .g2-legend {
            width: 250px !important;
            height: 100%;
            font-size: 14px !important;
            color: #595959 !important;
            top: 0 !important;
        }

        .g2-legend-title {
            color: #5a5a5a !important;
            font-size: 14px !important;
        }

        .g2-legend-list {
            margin-top: 10px;
        }

        .g2-legend-list-item {
            width: 100%;
            height: 30px;
            margin-bottom: 10px;
        }
        .g2-legend-marker{
            width: 18px !important; 
            height: 18px !important;
            line-height: 18px; 
            text-align: center; 
            margin-top: 3px; 
            color: white; 
            float: left; 
            border-style: solid; 
            border-width: 1px; 
            border-color: rgb(204, 204, 204);
        }

        .legend-item-name {
            min-width:60px;
            height: 100%;
            float: left;
            margin-left: 8px;
            font-size: 10px;
            color: #595959;
            text-align: left;
        }

        .legend-item-name:after {
            display: inline-block;
            width: 0;
            height: 100%;
            vertical-align: middle;
            content: '';
        }

        .legend-item-percent {
            min-width: 30px;
            height: 100%;
            float: left;
            margin-left: 10px;
            font-size: 10px;
            color: #949494;
            text-align: left;
        }

        .legend-item-percent:after {
            display: inline-block;
            width: 0;
            height: 100%;
            vertical-align: middle;
            content: '';
        }

        .legend-item-value {
            min-width: 80px;
            height: 100%;
            float: left;
            margin-left: 10px;
            font-size: 10px;
            color: #595959;
            text-align: left;
        }

        .legend-item-value:after {
            display: inline-block;
            width: 0;
            height: 100%;
            vertical-align: middle;
            content: '';
        }
      `
      const style = document.createElement('style')
      style.setAttribute('id', id)
      style.innerHTML = styleTxt
      document.getElementsByTagName('head')[0].appendChild(style)
    },
    getYearsum() {
      console.log()
      var sum = 0
      for (var i = 0; i < this.dataSource.length; i++) {
        var d = this.dataSource[i]
        if (d.OrderDate === 2016) {
          sum += d.OrderNumber
        }
      }
      return sum
    },
    getYearPercent(sum) {
      var percentage = {}
      for (var i = 0; i < this.dataSource.length; i++) {
        var d = this.dataSource[i]
        if (d.OrderDate === 2016) {
          percentage[d.Item] = {
            percent: d.OrderNumber / sum,
            value: d.OrderNumber
          }
        }
      }
      return percentage
    }
  },
  data() {
    return {
      padding: [20, 210, 30, 50],
      containerTpl: containerTpl,
      itemTpl: itemTpl,
      yearPercent: undefined,
      scale: [
        {
          dataKey: 'year',
          type: 'timeCat',
          range: [0, 1]
        }
      ],
      label: label,
      labelFormat: labelFormat,
      legendStyle: {
        MARKER_CLASS: {
          width: '18px',
          height: '18px',
          lineHeight: '18px',
          borderRadius: '50px',
          display: 'inline-block',
          marginRight: '4px',
          textAlign: 'center',
          fontZize: '10px',
          marginTop: '3px',
          color: 'white',
          float: 'left',
          borderStyle: 'solid',
          borderWidth: '1px',
          borderColor: '#ccc'
        }
      },
      color: ['Item', ['#ff4d4f', '#ff7a45', '#ffa940', '#facc14', '#bae637', '#73d13d', '#36cfc9', '#40a9ff']]
    }
  }
}
</script>
