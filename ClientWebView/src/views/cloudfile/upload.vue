<template>
  <div>
    <el-button @click="changePath">切换目录</el-button>
    <el-table
      id="leftBox"
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
          <span>{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="修改日期"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.lastModifiedDate }}</span>
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
      currentRow: {
        name: '',
        size: 0,
        lastModifiedDate: ''
      },
      uploadTableData: [],
      dirHandle: null
    }
  },
  created() {
    var table = []
    for (var i = 0; i < 10; i++) {
      table[i] = {
        name: '文件' + i,
        size: Math.floor(Math.random() * 1000000),
        lastModifiedDate: '2020-' + (Math.floor(Math.random() * 1000000) % 12 + 1) + '-' +
          (Math.floor(Math.random() * 1000000) % 30 + 1),
        isFile: true
      }
    }
    this.uploadTableData = table
  },
  methods: {
    handleUpload(index, row) {
      console.log(index, row)
    },
    async changePath() {
      var table = []
      this.dirHandle = await window.showDirectoryPicker()
      for await (const entry of this.dirHandle.values()) {
        if (entry.kind === 'file') {
          const file = await entry.getFile()
          file['isFile'] = true
          table.push(file)
        } else {
          entry['size'] = ''
          entry['lastModifiedDate'] = ''
          entry['isFile'] = false
          table.push(entry)
        }
      }
      this.uploadTableData = table
      console.log(table)
    },
    handleDblclick(row) {
      console.log(row)
      this.dialogVisible = true
      this.currentRow = row
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
    box-shadow: 4px 2px 2px 1px rgba(0, 0, 0, 0.2);
    position: relative; z-index: 1;
  }
  #rightBox {
  }
}
</style>
