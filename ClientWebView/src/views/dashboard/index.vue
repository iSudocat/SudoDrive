<template>
  <div class="dashboard-container">
    <span>Result:{{result}}</span>
    <el-button @click="testCs">Test</el-button>
  </div>
</template>

<script>
export default {
  name: 'Dashboard',
  data() {
    return {
      result: 0
    }
  },
  methods: {
    async testCs() {
      var that = this
      window.demoFunction.add(Number(10), Number(12)).then(function(ret) {
        that.result = ret
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }
  &-text {
    font-size: 30px;
    line-height: 46px;
  }
}
</style>
