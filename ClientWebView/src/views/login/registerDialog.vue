<template>
  <div>
    <el-dialog
      title="详细信息"
      :visible="dialogVisible"
      :show-close="false"
      width="30%"
    >
      <el-form ref="form" :model="form">
        <el-form-item label="用户名">
          <el-input v-model="form.userName" />
        </el-form-item>
        <el-form-item label="密码">
          <el-input v-model="form.password" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit">注册</el-button>
          <el-button>取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'RegisterDialog',
  props: {
    dialogVisible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      form: {
        userName: null,
        password: null
      }
    }
  },
  methods: {
    onSubmit() {
      return
    }
  }
}
</script>

<style scoped>

</style>
