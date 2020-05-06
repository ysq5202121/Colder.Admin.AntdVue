<template>
  <div>
    <iframe style="border:none;max-height:900px" :src="hangfireUrl" width="100%" height="850"></iframe>
  </div>
</template>

<script>
import defaultSettings from '@/config/defaultSettings'
import ProcessHelper from '@/utils/helper/ProcessHelper'
export default {
  data: function() {
    return {
      greeting: 'Hello'
    }
  },
  computed: {
    hangfireUrl() {
      if (ProcessHelper.isProduction() || ProcessHelper.isPreview()) {
        return defaultSettings.publishRootUrl + '/hangfire'
      } else {
        return defaultSettings.localRootUrl + '/hangfire'
      }
    }
  }
}
</script>
