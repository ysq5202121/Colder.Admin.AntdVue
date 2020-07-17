import Vue from 'vue'
import VueStorage from 'vue-ls'
import config from '@/config/defaultSettings'

// base library
import Antd from 'ant-design-vue'
// import Viser from 'viser-vue'
import VueCropper from 'vue-cropper'
import 'ant-design-vue/dist/antd.less'

// ext library
import VueClipboard from 'vue-clipboard2'
// import PermissionHelper from '@/utils/helper/permission'
// import '@/components/use'
import './directives/action'
// 导入移动端组件modify by ysq 2020-04-24
import Vant from 'vant'
import viservue from 'viser-vue'
import 'vant/lib/index.css'

VueClipboard.config.autoSetContainer = true

Vue.use(Antd)
Vue.use(Vant)
Vue.use(viservue)
// Vue.use(Viser)

Vue.use(VueStorage, config.storageOptions)
Vue.use(VueClipboard)
// Vue.use(PermissionHelper)
Vue.use(VueCropper)
