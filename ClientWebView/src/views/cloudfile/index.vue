<template>
  <div>
    <el-row>
      <el-col id="leftBox" :xs="{span:24}" :sm="{span:12}">
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
@media screen and (min-width: 768px) {
  #leftBox {
    border-right: 1px solid rgb(235,238,235);
  }
  #rightBox {
  }
}

</style>
