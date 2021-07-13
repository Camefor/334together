<template>
  <div class="wxsmallcode-page publicCon">
    <div class="copyBox">
      sysAppId：<span>{{ sysAppIds }}</span>
      <el-button
        class="ml10"
        type="text"
        size="medium"
        v-clipboard:copy="sysAppIds"
        v-clipboard:success="onCopy"
        v-clipboard:error="onError"
        >点击复制</el-button
      >
    </div>
  </div>
</template>

<script>
export default {
  name: "First",
  mounted() {},
  data() {
    return {
      user: "",
      msg: "",
      remsg: "",
      sysAppIds: "xxxxxxxxxxxsx",
    };
  },
  created: {},

  methods: {
    // 复制成功
    onCopy(e) {
      layer.confirm(
        "您是如何看待前端开发？",
        {
          btn: ["重要", "奇葩"], //按钮
        },
        function () {
          layer.msg("的确很重要", { icon: 1 });
        },
        function () {
          layer.msg("也可以这样", {
            time: 20000, //20s后自动关闭
            btn: ["明白了", "知道了"],
          });
        }
      );
      console.log(e);
    },
    // 复制失败
    onError(e) {
      alert("失败");
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#message {
  overflow-y: auto;
  text-align: left;
  border: #42b983 solid 1px;
  height: 200px;
}
.inputTxt {
  height: 40px;
  width: 100px;
}
</style>