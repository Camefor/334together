<template>
  <div class="hello">
    <h3>hello world</h3>
    <div id="message" v-html="remsg"></div>
    <input class="inputTxt" type="text" placeholder="请输入用户名" v-model="user" />
    <input class="inputTxt" type="text" placeholder="请输入内容" v-model="msg" />
    <button class="inputTxt" @click="handle">发送消息</button>
  </div>
</template>

<script>
import * as signalR from "@microsoft/signalr";

let token =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6ImFkbWluIiwiSUQiOiIyIiwiZXhwIjoxNTk5NjM3NjIxLCJpc3MiOiJuZXRsb2NrIiwiYXVkIjoibmV0bG9ja3MifQ.9T1zw2LaCx4enZLj5RCfxhJ85a169NPMqmW0n5OlzgI";
  
let hubUrl = "http://localhost:44370/signalr-hubs/test";

var connection = new signalR.HubConnectionBuilder().withUrl(hubUrl).build();


//.net core 版本中默认不会自动重连，需手动调用 withAutomaticReconnect
// const connection = new signalR.HubConnectionBuilder()
//   .withAutomaticReconnect() //断线自动重连
//   .withUrl(hubUrl) //传递参数Query["access_token"]
//   .build();


//启动
connection.start().catch((err) => {
  console.log(err);
});

//自动重连成功后的处理
connection.onreconnected((connectionId) => {
    debugger;
  console.log(connectionId);
});

export default {
  name: "First",
  mounted() {
    var _this = this;

    //调用后端方法 SendMessageResponse 接收定时数据
    connection.on("ReceiveMessage", function (data) {
      console.log(data);
                _this.remsg = _this.remsg + "<br>" + "接收到的消息:" + data;
    });
  
  },
  data() {
    return {
      user: "",
      msg: "",
      remsg: "",
    };
  },

  methods: {
    handle: function () {
      if (this.msg.trim() == "") {
        alert("不能发送空白消息");
        return;
      }
      //调用后端方法 SendMessage 传入参数
      connection.invoke("SendMessage", this.user, this.msg);
    //   this.msg = "";
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
.inputTxt{
    height:40px;
    width: 100px;
}
</style>