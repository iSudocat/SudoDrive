<template>
  <div>
    <el-tabs v-model="firstActiveName" @tab-click="handleFirstClick">
      <el-tab-pane label="" name="" />
      <el-tab-pane label="上传" name="upload">
        <el-tabs v-model="uploadActiveName" @tab-click="handleUploadClick">
          <el-tab-pane label="" name="" />
          <el-tab-pane label="传输中" name="running">
            <div
              v-for="(item, i) in uploads.running"
              :key="i"
            >
              <span>{{ item.name }}</span>
              <el-progress
                :text-inside="true"
                :stroke-width="20"
                :percentage="item.percentage"
              />
            </div>
          </el-tab-pane>
          <el-tab-pane label="成功" name="success">
            <div
              v-for="(item, i) in uploads.success"
              :key="i"
            >
              <span>{{ item.name }}</span>
              <el-progress
                :text-inside="true"
                :stroke-width="20"
                :percentage="item.percentage"
                status="success"
              />
            </div>
          </el-tab-pane>
          <el-tab-pane label="失败" name="fail">
            <div
              v-for="(item, i) in uploads.fail"
              :key="i"
            >
              <span>{{ item.name }}</span>
              <el-progress
                :text-inside="true"
                :stroke-width="20"
                :percentage="item.percentage"
                status="exception"
              />
            </div>
          </el-tab-pane>
        </el-tabs>
      </el-tab-pane>
      <el-tab-pane label="下载" name="download">
        <el-tabs v-model="downloadActiveName" @tab-click="handleDownloadClick">
          <el-tab-pane label="" name="" />
          <el-tab-pane label="传输中" name="running" />
          <el-tab-pane label="成功" name="success" />
          <el-tab-pane label="失败" name="fail" />
        </el-tabs>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>

export default {
  name: 'FileState',
  data() {
    return {
      uploads: {
        running: [],
        success: [],
        fail: []
      },
      downloads: {
        running: [],
        success: [],
        fail: []
      },
      firstActiveName: 'download',
      uploadActiveName: 'running',
      downloadActiveName: 'running'
    }
  },
  created() {
    for (let i = 0; i < 5; i++) {
      this.uploads.running.push({
        name: '传输中文件' + i,
        percentage: i * 10
      })
      this.uploads.fail.push({
        name: '失败文件' + i,
        percentage: 90 - i * 10
      })
    }
    this.uploads.success.push({
      name: '成功文件',
      percentage: 100
    })
  },
  methods: {
    handleFirstClick(tab, event) {
      console.log(tab, event)
    },
    handleUploadClick(tab, event) {
      console.log(tab, event)
    },
    handleDownloadClick(tab, event) {
      console.log(tab, event)
    }
  }
}
</script>

<style scoped>

</style>
