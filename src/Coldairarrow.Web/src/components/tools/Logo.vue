<template>
  <div class="logo">
    <router-link :to="{ path: '/' }">
      <LogoSvg alt="logo" />
      <h1 v-if="showTitle" style="margin:0 0 0 0px">{{ title }}</h1>
    </router-link>
  </div>
</template>

<script>
import LogoSvg from '@/assets/logo.svg?inline'

export default {
  name: 'Logo',
  components: {
    LogoSvg
  },
  props: {
    title: {
      type: String,
      default: process.env.VUE_APP_ProjectName,
      required: false
    },
    showTitle: {
      type: Boolean,
      default: true,
      required: false
    }
  }
}
</script>
