<template>
  <div class="dashboard-container">
    <span>Result:{{result}}</span>
    <el-button @click="testCs">Test</el-button>
    <el-button @click="testFolder">Folder</el-button>
    <el-button @click="testFolderInfo">FolderInfo</el-button>
    <el-button @click="testFileInfo">FileInfo</el-button>
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
    async testCs() {
      var that = this
      window.demoFunction.add(Number(10), Number(12)).then(function(ret) {
        that.result = ret
      })
    },
    async testFolder() {
      window.demoFunction.selectFolder().then(function(ret) {
        console.log(ret)
      })
    },
    async testFolderInfo() {
      window.demoFunction.showFolderInfo().then(function(ret) {
        console.log(ret)
      })
    },
    async testFileInfo() {
      window.demoFunction.showFileInfo().then(function(ret) {
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
