<template>
  <div>
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
          <span>{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="修改日期"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.date }}</span>
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
          >下载</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  name: 'Download',
  data() {
    return {
      uploadTableData: []
    }
  },
  created() {
    var table = []
    for (var i = 0; i < 5; i++) {
      table[i] = {
        name: '文件' + i,
        size: Math.floor(Math.random() * 1000000),
        date: '2020-' + (Math.floor(Math.random() * 1000000) % 12 + 1) + '-' +
          (Math.floor(Math.random() * 1000000) % 30 + 1)
      }
    }
    this.uploadTableData = table
  },
  methods: {
    handleUpload(index, row) {
      console.log(index, row)
    }
  }
}
</script>

<style scoped>

</style>
