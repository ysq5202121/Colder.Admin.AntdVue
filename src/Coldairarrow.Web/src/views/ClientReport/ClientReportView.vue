<template>
  <div>
    <a-row :gutter="24">
      <a-col :xl="12" :lg="24" :md="24" :sm="24" :xs="24">
        <div style="margin-top:10px;margin-left:10px">
          <a-radio-group button-style="solid">
            <a-radio-button value="a">平台</a-radio-button>
            <a-radio-button value="b">团队</a-radio-button>
          </a-radio-group>
        </div>
      </a-col>
      <a-col :xl="12" :lg="24" :md="24" :sm="24" :xs="24">
        <div style="width:50%;margin-top:10px;margin-left:10px">
          <a-slider :marks="marks" :step="1" :default-value="15" :min="15" :max="30" />
        </div>
      </a-col>
    </a-row>
    <a-row :gutter="24" type="flex">
      <a-col :xl="12" :lg="24" :md="24" :sm="24" :xs="24">
        <a-card
          :loading="OrderTrendLoading"
          :bordered="false"
          title="订单趋势"
          :style="{ height: '100%' }"
        >
          <BrokeLine :dataSource="orderTrendData"></BrokeLine>
        </a-card>
      </a-col>
      <a-col :xl="12" :lg="24" :md="24" :sm="24" :xs="24">
        <a-card
          class="antd-pro-pages-dashboard-analysis-salesCard"
          :loading="orderProportionLoading"
          :bordered="false"
          title="订单占比"
          :style="{ height: '100%' }"
        >
          <Pancake :dataSource="orderProportionData"></Pancake>
        </a-card>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :xl="24" :lg="24" :md="24" :sm="24" :xs="24">
        <TableView></TableView>
      </a-col>
    </a-row>
  </div>
</template>

<script>
import Pancake from './Pancake'
import BrokeLine from './BrokeLine'
import TableView from './TableView'

export default {
  mounted() {
    // 鉴权,如果没有权限跳转到错误提示页面
    
    this.getOrderProportionDataList()
    this.getOrderTrendDataList()
  },
  components: {
    Pancake,
    BrokeLine,
    TableView
  },
  data: function() {
    return {
      loading: true,
      orderProportionLoading: true,
      OrderTrendLoading: true,
      orderTrendData: [],
      orderProportionData: [],
      marks: {
        15: '15天',
        30: {
          style: {
            color: '#f50'
          },
          label: '30天'
        }
      }
    }
  },
  methods: {
    getOrderProportionDataList() {
      this.orderProportionLoading = true
      this.$http
        .post('/ServerReport/RP_ReportView/GetOrderProportionList', {
          SearchType: '',
          Day: 1
        })
        .then(resJson => {
          this.orderProportionLoading = false
          this.orderProportionData = resJson.Data
        })
    },
    getOrderTrendDataList() {
      this.OrderTrendLoading = true
      this.$http
        .post('/ServerReport/RP_ReportView/GetOrderTrendList', {
          SearchType: '',
          Day: 1
        })
        .then(resJson => {
          this.OrderTrendLoading = false
          this.orderTrendData = resJson.Data
        })
    }
  }
}
</script>

<style lang="less" scoped>
.extra-wrapper {
  line-height: 55px;
  padding-right: 24px;
  .extra-item {
    display: inline-block;
    margin-right: 24px;
    a {
      margin-left: 24px;
    }
  }
}
.antd-pro-pages-dashboard-analysis-twoColLayout {
  position: relative;
  display: flex;
  display: block;
  flex-flow: row wrap;
}
.antd-pro-pages-dashboard-analysis-salesCard {
  height: calc(100% - 24px);
  /deep/ .ant-card-head {
    position: relative;
  }
}
.dashboard-analysis-iconGroup {
  i {
    margin-left: 16px;
    color: rgba(0, 0, 0, 0.45);
    cursor: pointer;
    transition: color 0.32s;
    color: black;
  }
}
.analysis-salesTypeRadio {
  position: absolute;
  right: 54px;
  bottom: 12px;
}
</style>
