<template>
  <div>
    <slot name="content"></slot>
    <span class="copyContent" v-show="info" @click="$_copyInfo(info)">复制</span>
  </div>
</template>

<script>
export default {
  name: 'copyDialog',
  props: {
    info: {
      type: [Number, String],
      default: '',
    },
  },
  methods: {
    $_copyInfo(info) {
      this.$copyText(info).then(() => {
        this.$message.success('复制成功');
      }, () => {
        this.$message.error('复制失败');
      });
    },
  },
};
</script>

<style lang="less">
  .copyContent {
    cursor: pointer;
    color: #357ef6;
  }
</style>