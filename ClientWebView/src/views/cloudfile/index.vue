<template>
  <div>
    <el-row>
      <el-col :xs="{span:24}" :sm="{span:12}">
        <upload @changePath="changeLocalPath" @changeFile="changeLocalFile"/>
      </el-col>
      <el-col id="rightBox" :xs="{span:24}" :sm="{span:12}">
        <download :local-path="localPath" :local-file="localFile"/>
      </el-col>
    </el-row>
    <RightPanel>
      <FileState />
    </RightPanel>
  </div>
</template>

<script>
import upload from '@/views/cloudfile/upload'
import Download from '@/views/cloudfile/download'
import RightPanel from '@/views/cloudfile/RightPanel'
import FileState from '@/views/cloudfile/FileState'

export default {
  name: 'Cloudfile',
  components: { FileState, RightPanel, Download, upload },
  data() {
    return {
      localPath: '',
      localFile: null
    }
  },
  methods: {
    changeLocalPath(newPath) {
      this.localPath = newPath
    },
    changeLocalFile(newFile) {
      this.localFile = newFile
    }
  }
}
</script>

<style scoped>

</style>
