<template>
  <div>
    <el-row style="margin: 15px 10px 15px 20px;">
      <el-col :span="21" />
      <el-col :span="3">
        <el-button size="small" type="primary" style="height: 24px; line-height: 4px;">下载</el-button>
      </el-col>
    </el-row>
    <el-breadcrumb separator="/" style="margin: 15px 10px 15px 20px;">
      <el-breadcrumb-item
        v-for="(item,i) in (currentPath.split('\\'))"
        :key="i"
      >
        <el-button type="text" size="mini" @click="handleJump(i)">{{ item }}</el-button>
      </el-breadcrumb-item>
    </el-breadcrumb>
    <hr style="border:0; background-color: #f1f1f1; height: 1px">
    <el-table
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
    <info-dialog :dialog-visible="dialogVisible" :current-row="currentRow" @closeDialog="closeDialog" />
  </div>
</template>

<script>
import InfoDialog from '@/views/cloudfile/infoDialog'
export default {
  name: 'Download',
  components: { InfoDialog },
  data() {
    return {
      uploadTableData: [],
      dialogVisible: false,
      currentPath: '.\\xxx\\xxxxx\\xxxxxxx',
      currentRow: {
        name: '',
        size: 0,
        date: '0'
      }
    }
  },
  created() {
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
  },
  methods: {
    handleUpload(index, row) {
      console.log(index, row)
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
    handleJump(num) {
      console.log(num)
    }
  }
}
</script>

<style scoped>

</style>
