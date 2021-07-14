<template>
  <div class="layui-row layui-col-space10">
    <div class="layui-col-md4">
      <div class="copyBox">
        sysAppId：<span>{{ sysAppIds }}</span>
        <el-button
          class="ml10"
          type="text"
          size="medium"
          v- :copy="sysAppIds"
          v-clipboard:success="onCopy"
          v-clipboard:error="onError"
          >点击复制</el-button
        >
      </div>
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
      sysAppIds: "12s3654xxxx",
    };
  },
  created: {},

  methods: {
    // 复制成功
    onCopy(e) {
      //墨绿深蓝风
      layer.alert(
        "墨绿风格，点击确认看深蓝",
        {
          skin: "layui-layer-molv", //样式类名
          closeBtn: 0,
        },
        function () {
          layer.alert("偶吧深蓝style", {
            skin: "layui-layer-lan",
            closeBtn: 0,
            anim: 4, //动画类型
          });
        }
      );

      // layer.confirm(
      //   "您是如何看待前端开发？",
      //   {
      //     btn: ["重要", "奇葩"], //按钮
      //   },
      //   function () {
      //     layer.msg("的确很重要", { icon: 1 });
      //   },
      //   function () {
      //     layer.msg("也可以这样", {
      //       time: 20000, //20s后自动关闭
      //       btn: ["明白了", "知道了"],
      //     });
      //   }
      // );
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