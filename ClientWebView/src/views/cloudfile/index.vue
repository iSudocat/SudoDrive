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
    <RightPanel>
      <span>helloworld</span>
    </RightPanel>
  </div>
</template>

<script>
import upload from '@/views/cloudfile/upload'
import Download from '@/views/cloudfile/download'
import RightPanel from '@/views/cloudfile/RightPanel'

export default {
  name: 'Cloudfile',
  components: { RightPanel, Download, upload },
  data() {
    return {
      // isPanelVisible: false
    }
  }
}
</script>

<style scoped>

</style>
