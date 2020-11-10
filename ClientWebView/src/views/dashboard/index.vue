<template>
  <div class="dashboard-container">
    <el-button @click="testFolder">Folder</el-button>
    <el-button @click="testFolderInfo">FolderInfo</el-button>
    <el-button @click="testFileInfo">FileInfo</el-button>
    <el-button @click="testDriver">DriverInfo</el-button>
  </div>
</template>

<script>
export default {
  name: 'Dashboard',
  data() {
    return {
      result: 0
    }
  },
  methods: {
    async testFolder() {
      window.fileFunction.showAllInfo().then(function(ret) {
        console.log(ret)
      })
    },
    async testFolderInfo() {
      window.fileFunction.showFolderInfo().then(function(ret) {
        console.log(ret)
      })
    },
    async testFileInfo() {
      window.fileFunction.showFileInfo().then(function(ret) {
        console.log(ret)
      })
    },
    async testDriver() {
      window.fileFunction.showAllDrives().then(function(ret) {
        console.log(ret)
      })
      window.fileFunction.switchDriver('D:\\').then(function(ret) {
        console.log(ret)
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }
  &-text {
    font-size: 30px;
    line-height: 46px;
  }
}
</style>
