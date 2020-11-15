<template>
  <div>
    <el-dialog
      title="详细信息"
      :visible="dialogVisible"
      :show-close="false"
      width="50%"
    >
      <div v-for="(value,key,index) in currentRow" :key="index">{{ key }}:{{ value }}</div>
      <!--      <div>文件名：{{ currentRow.name }}</div>-->
      <!--      <div>文件大小：{{ currentRow.size }}</div>-->
      <!--      <div>修改时间：{{ currentRow.lastModified }}</div>-->
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="handleExit()">确 定</el-button>
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
      default: () => {
        return {
          name: '',
          size: 0,
          date: '0'
        }
      }
    }
  },
  data() {
    return {
    }
  },
  methods: {
    handleExit() {
      console.log('handleExit')
      this.$emit('closeDialog', false)
    }
  }
}
</script>

<style scoped>

</style>
