<template>
  <div>
    <el-tabs v-model="firstActiveName" @tab-click="handleFirstClick">
      <el-tab-pane label="" name="" />
      <el-tab-pane label="上传" name="upload">
        <el-tabs v-model="uploadActiveName" @tab-click="handleUploadClick">
          <el-tab-pane label="等待" name="waiting">
            <el-table
              :data="uploads.waiting"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="传输" name="running">
            <el-table
              :data="uploads.running"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="成功" name="success">
            <el-table
              :data="uploads.success"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    status="success"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="失败" name="fail">
            <el-table
              :data="uploads.fail"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    status="exception"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
        </el-tabs>
      </el-tab-pane>
      <el-tab-pane label="下载" name="download">
        <el-tabs v-model="downloadActiveName" @tab-click="handleDownloadClick">
          <el-tab-pane label="等待" name="waiting">
            <el-table
              :data="downloads.waiting"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="传输" name="running">
            <el-table
              :data="downloads.running"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="成功" name="success">
            <el-table
              :data="downloads.success"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    status="success"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="失败" name="fail">
            <el-table
              :data="downloads.fail"
              style="width: 100%;height: 78vh;overflow:auto;"
              :border="true"
            >
              <el-table-column
                label="文件名"
                align="center"
              >
                <template slot-scope="scope">
                  <span style="margin-left: 10px">{{ scope.row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="进度"
                align="center"
              >
                <template slot-scope="scope">
                  <el-progress
                    :text-inside="true"
                    :stroke-width="16"
                    status="exception"
                    :percentage="scope.row.percentage"
                  />
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
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
        waiting: [],
        running: [],
        success: [],
        fail: []
      },
      downloads: {
        waiting: [],
        running: [],
        success: [],
        fail: []
      },
      firstActiveName: 'download',
      uploadActiveName: 'waiting',
      downloadActiveName: 'waiting'
    }
  },
  created() {
    for (let i = 0; i < 9; i++) {
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
