<template>
  <div>
    <el-dialog
      title="详细信息"
      :visible.sync="dialogVisible"
      width="30%"
    >
      <span>{{ currentRow }}</span>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'InfoDialog',
  props: {
    dialogVisible: {
      type: Boolean,
      required: true,
      default: false
    },
    currentRow: {
      type: Object,
      required: true,
      default: null
    }
  },
  data() {
    return {
    }
  }
}
</script>

<style scoped>

</style>
