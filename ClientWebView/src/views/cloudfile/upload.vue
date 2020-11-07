<template>
  <div>
    <el-button @click="changePath">切换目录</el-button>
    <el-table
      :data="uploadTableData"
      style="width: 100%"
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
          <i class="el-icon-folder" v-if="!scope.row.isFile"></i>
          <i class="el-icon-tickets" v-if="scope.row.isFile"></i>
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
  </div>
</template>

<script>
export default {
  name: 'Upload',
  data() {
    return {
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
    }
  }
}
</script>

<style scoped>

</style>
