<template>
  <div>
    <el-row>
      <el-col :xs="{span:24}" :sm="{span:12}">
        <upload />
      </el-col>
      <el-col id="rightBox" :xs="{span:24}" :sm="{span:12}">
        <download />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import upload from '@/views/cloudfile/upload'
import Download from '@/views/cloudfile/download'

export default {
  name: 'Cloudfile',
  components: { Download, upload }
}
</script>

<style scoped>

</style>
