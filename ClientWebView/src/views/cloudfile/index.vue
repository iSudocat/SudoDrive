<template>
  <div>
    <el-row>
      <el-col :xs="{span:24}" :sm="{span:12}">
        <upload
          ref="uploadComponent"
          :cloud-path="cloudPath"
          @changePath="changeLocalPath"
          @changeFile="changeLocalFile"
          @afterUpload="refreshCloud"
        />
      </el-col>
      <el-col id="rightBox" :xs="{span:24}" :sm="{span:12}">
        <download
          ref="downloadComponent"
          :local-path="localPath"
          :local-file="localFile"
          @changePath="changeCloudPath"
          @afterDownload="refreshLocal"
        />
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
      localFile: null,
      cloudPath: ''
    }
  },
  methods: {
    changeLocalPath(newPath) {
      this.localPath = newPath
    },
    changeLocalFile(newFile) {
      this.localFile = newFile
    },
    changeCloudPath(newPath) {
      this.cloudPath = newPath
    },
    refreshCloud() {
      this.$refs.downloadComponent.refreshPath()
    },
    refreshLocal() {
      this.$refs.uploadComponent.refreshPath()
    }
  }
}
</script>

<style scoped>

</style>
