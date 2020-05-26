<template>
  <div>
    <van-panel style="border: none">
      <template #header>
        <van-dropdown-menu style="text-align:center">
          <van-dropdown-item :value="1" :options="option1" style="text-align:center" />
        </van-dropdown-menu>
      </template>
      <template #default>
        <div>
          <div style="float:left">{{ moment().add(7, 'd').format('MMMM') }}</div>
          <div style="float:left" v-for="item in 7" :key="item">
            <van-button
              type="default"
              style="width: 50px; height: 50px; border-radius:50%;border: none"
            >{{ moment().add(item-1, 'd').format('DD E') }}</van-button>
          </div>
        </div>
      </template>
    </van-panel>
    <!-- <van-pull-refresh>
      <van-list v-model="listLoading" :finished="true" finished-text="没有更多了">
        <van-button type="default">方形按钮</van-button>
        <van-button
          type="default"
          style="width: 50px; height: 50px; border-radius:50%;border: none"
        >27</van-button>
        <van-button
          type="default"
          style="width: 50px; height: 50px; border-radius:50%;border: none"
        >28</van-button>
      </van-list>
    </van-pull-refresh>-->
    <RoomTabbar></RoomTabbar>
    <van-popup v-model="showPicker" round position="bottom">
      <van-picker show-toolbar :columns="columns" />
    </van-popup>
  </div>
</template>

<script>
import moment from 'moment'
import RoomTabbar from './RoomTabbar'
export default {
  mounted() {},
  data: function() {
    return {
      moment,
      data: [],
      loading: false,
      listLoading: true,
      showPicker: false,
      option1: [
        { text: '全部商品', value: 0 },
        { text: '新款商品', value: 1 },
        { text: '活动商品', value: 2 }
      ],
      columns: ['杭州', '宁波', '温州', '嘉兴', '湖州']
    }
  },
  components: {
    RoomTabbar
  },
  methods: {}
}
</script>
