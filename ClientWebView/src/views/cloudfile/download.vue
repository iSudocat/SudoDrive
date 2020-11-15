<template>
  <div>
    <el-row style="margin: 15px 10px 15px 20px;">
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
          @click="handleMultipleDownload"
        >
          <svg-icon icon-class="zzdownload" />
        </el-button>
      </el-col>
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
        >
          <svg-icon icon-class="zznewfolder" />
        </el-button>
      </el-col>
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
        >
          <svg-icon icon-class="zzdelete" />
        </el-button>
      </el-col>
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
        >
          <svg-icon icon-class="zzmore" />
        </el-button>
      </el-col>
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
        >
          <svg-icon icon-class="zzshare" />
        </el-button>
      </el-col>
    </el-row>
    <el-row style="margin: 15px 10px 15px 20px;">
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center;"
          @click="parentPath"
        >
          <svg-icon icon-class="return" />
        </el-button>
      </el-col>
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center;"
          @click="refreshPath"
        >
          <svg-icon icon-class="refresh" />
        </el-button>
      </el-col>
      <el-col :span="14">
        <el-breadcrumb separator="/" style="margin: 0px 10px 10px 20px;">
          <el-breadcrumb-item
            v-for="(item, i) in (currentPath)"
            :key="i"
            style="margin-right: -10px"
          >
            <el-button type="text" size="mini" @click="handleJump(i)">{{ item }}</el-button>
          </el-breadcrumb-item>
        </el-breadcrumb>
      </el-col>
    </el-row>
    <hr style="border:0; background-color: #f1f1f1; height: 1px">
    <el-table
      :data="downloadTableData"
      style="width: 100%"
      max-height="480"
      highlight-current-row
      @current-change="handleCurrentChange"
      @row-click="handleRowClick"
      @cell-dblclick="handleDblclick"
      @selection-change="handleSelectionChange"
    >
      <el-table-column
        type="selection"
        width="55"
        align="center"
      />
      <el-table-column
        label="文件名"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="修改日期"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.updatedAt }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="大小"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.size }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        align="center"
      >
        <template>
          <el-button
            size="mini"
            @click="handleDownload"
          >下载</el-button>
        </template>
      </el-table-column>
    </el-table>
    <info-dialog :dialog-visible="dialogVisible" :current-row="currentRow" @closeDialog="closeDialog" />
  </div>
</template>

<script>
import InfoDialog from '@/views/cloudfile/infoDialog'
export default {
  name: 'Download',
  components: { InfoDialog },
  props: {
    localPath: {
      type: String,
      default: ''
    },
    localFile: {
      type: Object,
      default: undefined
    }
  },
  data() {
    return {
      buttonConfig: {
        xs: 4,
        sm: 2
      },
      // 是否第一次点击
      isFirstClick: true,
      downloadTableData: [],
      dialogVisible: false,
      // 云端地址
      cloudPath: '',
      // 云端数组
      currentPath: ['xx', 'xxx', 'xxxx'],
      multipleRow: [],
      currentRow: {
        name: '',
        size: 0,
        updatedAt: '0'
      }
    }
  },
  created() {
    const that = this
    if (typeof (CefSharp) === 'undefined') {
      const table = []
      for (let i = 0; i < 5; i++) {
        table[i] = {
          name: '文件' + i,
          size: Math.floor(Math.random() * 1000000),
          date: '2020-' + (Math.floor(Math.random() * 1000000) % 12 + 1) + '-' +
            (Math.floor(Math.random() * 1000000) % 30 + 1)
        }
      }
      this.uploadTableData = table
    } else {
      window.cloudFileFunction.getFileList().then(function(ret) {
        that.handleTableReturn(ret)
      })
    }
  },
  methods: {
    handleDownload() {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        console.log(that.currentRow)
      } else {
        console.log('download')
        console.log(that.localPath)
        console.log(that.currentRow.name)
        console.log(that.currentRow.guid)
        window.cloudFileFunction.download(String(that.localPath), String(that.currentRow.name), String(that.currentRow.guid)).then(function(ret) {
          console.log(ret)
          that.$emit('afterDownload')
        })
      }
    },
    // 处理批量下载
    handleMultipleDownload() {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        console.log(that.currentRow)
      } else {
        that.multipleRow.forEach(row => {
          window.cloudFileFunction.download(String(that.localPath), String(row.name), String(row.guid)).then(function(ret) {
            console.log(ret)
            that.$emit('afterDownload')
          })
        })
      }
    },
    // 多选事件
    handleSelectionChange(val) {
      this.multipleRow = val
    },
    // 第一次单击某行
    handleCurrentChange(row) {
      this.isFirstClick = true
    },
    // 单击某行
    handleRowClick(row) {
      console.log(this.localPath)
      console.log(this.localFile)
      const that = this
      that.currentRow = row
      if (that.isFirstClick) {
        that.isFirstClick = false
      } else {
        that.handleDblclick(row)
      }
    },
    handleDblclick(row) {
      console.log(row)
      this.dialogVisible = true
      this.currentRow = row
    },
    closeDialog(visible) {
      console.log('closeDialog')
      this.dialogVisible = visible
    },
    // 处理跳转
    handleJump(num) {
      console.log(num)
    },
    // 处理服务器返回的文件表信息
    handleTableReturn(ret) {
      const that = this
      const table = []
      const retObject = JSON.parse(ret)
      that.cloudPath = retObject.cloudFileList[0].folder
      this.$emit('changePath', that.cloudPath)
      that.currentPath = that.cloudPath.split('/')
      const cloudFileList = retObject.cloudFileList
      for (let i = 0; i < cloudFileList.length; i++) {
        table.push(cloudFileList[i])
      }
      that.downloadTableData = table
    },
    // 返回父目录
    parentPath() {
      return
    },
    // 刷新目录
    refreshPath() {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        return
      } else {
        window.cloudFileFunction.getFileList().then(function(ret) {
          that.handleTableReturn(ret)
        })
      }
    }
  }
}
</script>

<style scoped>
.el-button {
  height: 24px; line-height: 4px;
}
.el-table {
  user-select:none;
}
</style>
