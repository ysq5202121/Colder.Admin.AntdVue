// 用于配置不同APP调整的地址页面
const WeChatHelper = {
  GetGoToUrl (id) {
    var url = '/ClientFood/Order'
    switch (id) {
      case '1':
        url = '/ClientFood/Order'
        break
      case '2':
        url = '/ClientFood/Order'
        break
      case '3':
        url = '/ClientReport/ClientReportView'
        break
      default:
        url = '/ClientReport/ClientReportView'
    }
    return url
  }
}
export default WeChatHelper
