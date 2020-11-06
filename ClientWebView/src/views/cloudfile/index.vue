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
    /*border-right: 1px solid rgb(235,238,235);*/
    box-shadow: 4px -4px 2px 1px rgba(0, 0, 0, 0.2);
    position: relative; z-index: 1;
  }
  #rightBox {
  }
}

</style>
