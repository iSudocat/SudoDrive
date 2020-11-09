<template>
  <div>
    <el-dialog
      title="警告"
      :visible.sync="InvalidInputDialogVisible"
      width="30%"
      :show-close="false"
    >
      <span>输入无效</span>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="InvalidInputDialogVisible = false">确 定</el-button>
      </span>
    </el-dialog>
    <el-dialog
      title="修改"
      :visible.sync="UpdatedialogVisible"
      width="30%"
    >
      <el-form :model="upDateData" :inline="true">
        <el-form-item label="序号">
          <el-input v-model="upDateData.id" :disabled="true" size="small" />
        </el-form-item>
        <el-form-item label="姓名">
          <el-input v-model="upDateData.name" size="small" placeholder="姓名" />
        </el-form-item>
        <el-form-item label="头像链接">
          <el-input v-model="upDateData.headimg_url" size="small" placeholder="头像链接" />
        </el-form-item>
        <el-form-item label="学院">
          <el-input v-model="upDateData.faculty" size="small" placeholder="学院" />
        </el-form-item>
        <el-form-item label="学号">
          <el-input v-model="upDateData.school_num" size="small" placeholder="学号" />
        </el-form-item>
        <el-form-item label="权限">
          <el-select v-model="upDateData.author_level" size="small">
            <el-option v-for="(item, index) in author_levels" :key="index" :label="item" :value="item" />
          </el-select>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="commitEdit">确 定</el-button>
      </span>
    </el-dialog>
    <el-divider />
    <el-row>
      <el-col :span="7"><br></el-col>
      <el-col :span="17">
        <el-form :inline="true" :model="addData">
          <el-form-item label="用户名">
            <el-input v-model="addData.name" size="small" placeholder="用户名" />
          </el-form-item>
          <el-form-item label="权限">
            <el-select v-model="addData.author_level" size="small" placeholder="权限">
              <el-option v-for="(item, index) in author_levels" :key="index" :label="item" :value="item" />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="success" size="mini" @click="AddUser">添加</el-button>
            <el-button type="primary" size="mini" @click="Search">搜索</el-button>
            <el-button type="plain" size="mini" @click="getAllUser">刷新</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <el-table
      id="TableTop"
      :data="tableData.slice((currentPage-1)*pageSize,currentPage*pageSize)"
      style="width: 100%"
    >
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-form label-position="left" inline class="demo-table-expand">
            <el-form-item label="id">
              <span>{{ props.row.id }}</span>
            </el-form-item>
            <el-form-item label="用户名">
              <span>{{ props.row.name }}</span>
            </el-form-item>
            <el-form-item label="权限">
              <span>{{ props.row.author_level }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        prop="id"
        label="id"
        sortable="custom"
      >
        <template slot-scope="scope">
          <span style="margin-left: 10px">{{ scope.row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="用户名"
      >
        <template slot-scope="scope">
          <span style="margin-left: 10px">{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="权限"
      >
        <template slot-scope="scope">
          <span style="margin-left: 10px">{{ scope.row.author_level }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="操作"
      >
        <template slot-scope="scope">
          <el-button
            size="mini"
            type="warning"
            @click="handleEdit(scope.row.id, scope.row)"
          >编辑</el-button>
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.row.id, scope.row)"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <div style="text-align: center;margin-top: 30px;">
      <el-pagination
        :current-page="currentPage"
        :page-sizes="[5,6,7,8,9,10,11,12]"
        :page-size="pageSize"
        :pager-count="pageCount"
        layout="total, sizes, pager"
        :total="tableData.length"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div>
  </div>
</template>

<script>

export default {
  name: 'Users',
  data() {
    return {
      currentPage: 1,
      pageSize: 6,
      pageCount: 5,
      tableData: [],
      allData: [],
      InvalidInputDialogVisible: false,
      UpdatedialogVisible: false,
      author_levels: ['', 1, 2, 3, 4],
      addData: {
        'id': -1,
        'name': '',
        'headimg_url': '',
        'faculty': '',
        'school_num': '',
        'author_level': 4
      },
      upDateData: {
        'id': -1,
        'name': '',
        'headimg_url': '',
        'faculty': '',
        'school_num': '',
        'author_level': 4
      },
      sortState: 0
    }
  },
  created() {
    var that = this
    that.getAllUser()
  },
  methods: {
    getAllUser() {
      var that = this
      var table = []
      for (var i = 0; i < 50; i++) {
        table.push({
          'id': i,
          'name': '' + Math.floor(Math.random() * 10000),
          'author_level': Math.floor(Math.random() * 10000) % 5 + 1
        })
      }
      that.allData = table
      that.tableData = table
    },
    handleSizeChange(val) {
      this.pageSize = val
    },
    handleCurrentChange(currentPage) {
      this.currentPage = currentPage
      // location.href = '#TableTop'
    }
  }
}
</script>

<style scoped>
.demo-table-expand {
  font-size: 0;
}
.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
}
.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
</style>
