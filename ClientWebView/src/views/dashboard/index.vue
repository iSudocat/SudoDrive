<template>
  <div class="dashboard-container">
    <el-button @click="testFolder">Folder</el-button>
    <el-button @click="testFolderInfo">FolderInfo</el-button>
    <el-button @click="testFileInfo">FileInfo</el-button>
    <el-button @click="testDriver">DriverInfo</el-button>
    <el-button @click="testList">List</el-button>
    <el-button @click="testCloud">Cloud</el-button>
    <el-button @click="testTaskList">Task</el-button>
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
  created() {
    if (typeof (CefSharp) === 'undefined') {
      return
    } else {
      /*
      window.cloudFileFunction.login(String('sudodog'), String('ssss11111')).then(function(ret) {
        console.log(ret)
      })

       */
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
    },
    async testList() {
      window.cloudFileFunction.getFileList().then(function(ret) {
        console.log(ret)
      })
    },
    async testCloud() {
      window.cloudFileFunction.test().then(function(ret) {
        console.log(ret)
      })
    },
    async testTaskList() {
      window.cloudFileFunction.getUploadList().then(function(ret) {
        console.log('upload')
        console.log(ret)
      })
      window.cloudFileFunction.getDownloadList().then(function(ret) {
        console.log('download')
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
