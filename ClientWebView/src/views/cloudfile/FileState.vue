<template>
  <div>
    <el-tabs v-model="activeName" @tab-click="handleClick">
      <el-tab-pane label="" name=""></el-tab-pane>
      <el-tab-pane label="上传" name="upload">上传</el-tab-pane>
      <el-tab-pane label="下载" name="download">下载</el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
export default {
  name: 'FileState',
  data() {
    return {
      activeName: 'download'
    }
  },
  methods: {
    handleClick(tab, event) {
      console.log(tab, event)
    }
  }
}
</script>

<style scoped>

</style>
