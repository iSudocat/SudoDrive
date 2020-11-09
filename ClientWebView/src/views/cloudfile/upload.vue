<template>
  <div id="leftBox">
    <el-row style="margin: 15px 10px 15px 20px;">
      <el-col :span="21">
        <el-page-header style="color:#00abff" @back="parentPath" title="返回上一级"></el-page-header>
      </el-col>
      <el-col :span="3">
        <el-button size="small" type="primary" style="height: 24px; line-height: 4px;">上传</el-button>
      </el-col>
    </el-row>
    <hr style="border:0; background-color: #f1f1f1; height: 1px">

    <el-table
      highlight-current-row
      :data="uploadTableData"
      style="width: 100%"
      @cell-dblclick="handleDblclick"
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
    <info-dialog style="z-index: 2" :dialog-visible="dialogVisible" :current-row="currentRow" @closeDialog="closeDialog" />
  </div>
</template>

<script>
import InfoDialog from '@/views/cloudfile/infoDialog'

export default {
  name: 'Upload',
  components: { InfoDialog },
  data() {
    return {
      dialogVisible: false,
      currentPath: '',
      currentRow: {
        name: '',
        size: 0,
        lastModified: ''
      },
      uploadTableData: [],
      dirHandle: null
    }
  },
  created() {
    if (typeof (CefSharp) === 'undefined') {
      const table = []
      for (let i = 0; i < 10; i++) {
        table[i] = {
          name: '文件' + i,
          size: Math.floor(Math.random() * 1000000),
          lastModified: '2020-' + (Math.floor(Math.random() * 1000000) % 12 + 1) + '-' +
            (Math.floor(Math.random() * 1000000) % 30 + 1),
          isFile: true
        }
      }
      this.uploadTableData = table
    } else {
      this.InitPath()
    }
  },
  methods: {
    handleUpload(index, row) {
      console.log(index, row)
    },
    handleTableReturn(table, ret) {
      const retObject = JSON.parse(ret)
      this.currentPath = retObject.currentPath
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
    },
    async InitPath() {
      const that = this
      const table = []
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
        window.fileFunction.showAllInfo().then(function(ret) {
          that.handleTableReturn(table, ret)
          that.uploadTableData = table
        })
      }
    },
    parentPath() {
      const that = this
      const table = []
      if (typeof (CefSharp) === 'undefined') {
        return
      } else {
        window.fileFunction.toParent().then(function(ret) {
          that.handleTableReturn(table, ret)
          that.uploadTableData = table
        })
      }
    },
    handleDblclick(row) {
      console.log(row)
      const that = this
      const table = []
      if (typeof (CefSharp) === 'undefined') {
        return
      } else {
        if (row.isFile) {
          this.dialogVisible = true
          this.currentRow = row
        } else {
          window.fileFunction.toChild(String(row.name)).then(function(ret) {
            that.handleTableReturn(table, ret)
            that.uploadTableData = table
          })
        }
      }
    },
    closeDialog(visible) {
      console.log('closeDialog')
      this.dialogVisible = visible
    }
  }
}
</script>

<style scoped>
@media screen and (min-width: 768px) {
  #leftBox {
    /*border-right: 1px solid rgb(235,238,235);*/
    /*box-shadow: 4px 2px 2px 1px rgba(0, 0, 0, 0.2);*/
    position: relative; z-index: 1;
  }
  #xxx {
    box-shadow: 4px 2px 2px 1px rgba(0, 0, 0, 0.2);
    position: relative; z-index: 1;
  }
  #rightBox {
  }
}
</style>
