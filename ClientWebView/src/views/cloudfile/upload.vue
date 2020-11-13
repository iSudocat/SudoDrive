<template>
  <div id="leftBox">
    <el-row style="margin: 15px 10px 15px 20px;">
      <el-col :sm="buttonConfig.sm" :xs="buttonConfig.xs">
        <el-button
          size="small"
          type="primary"
          style="display: flex;justify-content: center;align-items: center"
        >
          <svg-icon icon-class="zzupload" />
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
      highlight-current-row
      :data="uploadTableData"
      style="width: 100%"
      max-height="480"
      @current-change="handleCurrentChange"
      @row-click="handleRowClick"
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
          <i v-if="!scope.row.isFile" class="el-icon-folder" />
          <i v-if="scope.row.isFile" class="el-icon-tickets" />
          <span>&nbsp;{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="修改日期"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.lastModified }}</span>
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
        <template slot-scope="scope">
          <el-button
            size="mini"
            @click="handleUpload(scope.$index, scope.row)"
          >上传</el-button>
        </template>
      </el-table-column>
    </el-table>
    <info-dialog :dialog-visible="infoDialogVisible" :current-row="currentRow" @closeDialog="closeDialog" />
    <el-dialog
      title="盘符切换"
      :visible.sync="driveDialogVisible"
      width="30%"
    >
      <el-select v-model="currentDrive" size="mini" placeholder="请选择">
        <el-option
          v-for="item in drives"
          :key="item"
          :label="item.label"
          :value="item"
        />
      </el-select>
      <el-button type="primary" style="margin-left: 5px;position: relative;top: 2px" @click="changeDrive">确认</el-button>
    </el-dialog>
  </div>
</template>

<script>
import InfoDialog from '@/views/cloudfile/infoDialog'

export default {
  name: 'Upload',
  components: { InfoDialog },
  data() {
    return {
      // 按钮响应式大小绑定
      buttonConfig: {
        xs: 4,
        sm: 2
      },
      // 是否第一次点击
      isFirstClick: true,
      // 文件信息弹窗
      infoDialogVisible: false,
      // 盘符切换弹窗
      driveDialogVisible: false,
      // 是否第一次切换盘符
      showSwitchDrive: 0,
      // 初始本地路径分割数组
      currentPath: ['C:', 'hel', 'llo', 'iii'],
      // 本地路径
      localPath: '',
      // 本地盘符
      drives: ['A:', 'B:', 'C:', 'D:'],
      // 当前盘符
      currentDrive: '',
      // 当前选择文件的信息
      currentRow: {
        name: '',
        size: 0,
        lastModified: ''
      },
      // 存储所有本地信息
      uploadTableData: [],
      // 浏览器所用窗口（wpf无用
      dirHandle: null
    }
  },
  created() {
    // 浏览器状态下随机生成数据
    if (typeof (CefSharp) === 'undefined') {
      const table = []
      for (let i = 0; i < 20; i++) {
        table[i] = {
          name: '文件' + i,
          size: Math.floor(Math.random() * 1000000),
          lastModified: '2020-' + (Math.floor(Math.random() * 1000000) % 12 + 1) + '-' +
            (Math.floor(Math.random() * 1000000) % 30 + 1),
          isFile: true
        }
      }
      this.uploadTableData = table
      // 初始化本地数据
    } else {
      this.InitPath()
    }
  },
  methods: {
    // 上传方法
    handleUpload(index, row) {
      console.log(index, row)
    },
    // 将C#传来的本地json数据转换为table显示里的数据
    handleTableReturn(ret) {
      const that = this
      const table = []
      const retObject = JSON.parse(ret)
      this.localPath = retObject.currentPath
      this.$emit('changePath', this.localPath)
      this.currentPath = retObject.currentPath.split('\\')
      // 去除不知道哪冒出来的最后一个空白
      const index = this.currentPath.indexOf('')
      if (index > -1) {
        this.currentPath.splice(index, index)
      }
      const fileTable = retObject.files
      const directoryTable = retObject.directories
      for (let j = 0; j < directoryTable.length; j++) {
        directoryTable[j]['isFile'] = false
        table.push(directoryTable[j])
      }
      for (let i = 0; i < fileTable.length; i++) {
        fileTable[i]['isFile'] = true
        table.push(fileTable[i])
      }
      that.uploadTableData = table
    },
    // 初始化初始路径文件信息和盘符
    async InitPath() {
      const that = this
      const table = []
      // 浏览器状态下
      if (typeof (CefSharp) === 'undefined') {
        this.dirHandle = await window.showDirectoryPicker()
        for await (const entry of this.dirHandle.values()) {
          if (entry.kind === 'file') {
            const file = await entry.getFile()
            file['isFile'] = true
            table.push(file)
          } else {
            entry['lastModified'] = ''
            entry['isFile'] = false
            table.push(entry)
          }
        }
        this.uploadTableData = table
        console.log(table)
      } else {
        // 初始化路径为桌面
        window.fileFunction.showAllInfo().then(function(ret) {
          that.handleTableReturn(ret)
        })
        // 初始化盘符
        window.fileFunction.showAllDrives().then(function(ret) {
          that.drives = JSON.parse(ret)
          that.currentDrive = that.drives[0]
        })
      }
    },
    // 返回父目录
    parentPath() {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        return
      } else {
        // 阻止在盘符根目录下回到父目录
        if (that.currentPath.length > 1) {
          window.fileFunction.toParent().then(function(ret) {
            that.handleTableReturn(ret)
          })
        }
      }
    },
    // 刷新本地文件信息
    refreshPath() {
      this.InitPath()
    },
    // 面包屑跳转
    handleJump(num) {
      const that = this
      // 点击当前目录名则啥也不做
      if (num === that.currentPath.length - 1) {
        // 点击盘符则显示盘符信息
        if (num === 0) {
          this.showSwitchDriveMessage()
        }
      } else {
        for (let i = 0; i < that.currentPath.length - 1 - num; i++) {
          that.parentPath()
        }
      }
    },
    // 第一次单击某行
    handleCurrentChange(row) {
      this.isFirstClick = true
    },
    // 单击某行
    handleRowClick(row) {
      const that = this
      if (that.isFirstClick) {
        that.isFirstClick = false
      } else {
        that.handleDblclick(row)
      }
    },
    // 双击table的处理
    handleDblclick(row) {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        if (row.isFile) {
          this.infoDialogVisible = true
          this.currentRow = row
        }
        return
      } else {
        if (row.isFile) {
          this.infoDialogVisible = true
          this.currentRow = row
        } else {
          window.fileFunction.toChild(String(row.name)).then(function(ret) {
            that.handleTableReturn(ret)
          })
        }
      }
    },
    // 关闭对话框
    closeDialog(visible) {
      this.infoDialogVisible = visible
    },
    // 点击盘符面包屑时 第一次显示提示，第二次以及之后直接打开切换对话框
    showSwitchDriveMessage() {
      const that = this
      if (that.showSwitchDrive === 0) {
        that.$notify({
          title: '切换盘符',
          message: '再按一次切换盘符',
          position: 'top-left'
        })
        that.showSwitchDrive++
      } else {
        that.driveDialogVisible = true
      }
    },
    // 切换盘符
    changeDrive() {
      const that = this
      if (typeof (CefSharp) === 'undefined') {
        return
      } else {
        window.fileFunction.switchDriver(that.currentDrive).then(function(ret) {
          that.handleTableReturn(ret)
        })
      }
      that.driveDialogVisible = false
    }
  }
}
</script>

<style scoped>
@media screen and (min-width: 768px) {
  #leftBox {
    /*border-right: 1px solid rgb(235,238,235);*/
    /*box-shadow: 4px 2px 2px 1px rgba(0, 0, 0, 0.2);*/
    position: relative;
  }
  #xxx {
    box-shadow: 4px 2px 2px 1px rgba(0, 0, 0, 0.2);
    position: relative;
  }
  #rightBox {
  }
}
.el-button:has(.svg-icon) {
  display: flex;justify-content: center;align-items: center;
}
.el-button {
  height: 24px; line-height: 4px;
}
.el-table {
  user-select:none;
}
</style>
