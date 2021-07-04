//添加signalr js库
import * as signalR from "@microsoft/signalr";

//初始化signalR
let token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6ImFkbWluIiwiSUQiOiIyIiwiZXhwIjoxNTk5NjM3NjIxLCJpc3MiOiJuZXRsb2NrIiwiYXVkIjoibmV0bG9ja3MifQ.9T1zw2LaCx4enZLj5RCfxhJ85a169NPMqmW0n5OlzgI";

// let hubUrl = "http://localhost:44370/signalr-hubs/test";
let hubUrl = "http://localhost:44370/hubs/listenTogether";
export default {

    initSignalR() {
        var _this = this;
        _this.connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .configureLogging(signalR.LogLevel.Information)
            .build();

        //.net core 版本中默认不会自动重连，需手动调用 withAutomaticReconnect
        // const connection = new signalR.HubConnectionBuilder()
        //   .withAutomaticReconnect() //断线自动重连
        //   .withUrl(hubUrl) //传递参数Query["access_token"]
        //   .build();

        //自动重连成功后的处理
        // connection.onreconnected((connectionId) => {
        //   debugger;
        //   console.log(connectionId);
        // });

        //signalR接收Serve端的数据
        _this.connection.on("SignalR_ReceiveData", function (data) {
            var res = JSON.parse(data);
            switch (res.actionType) {
                case "onpause": //暂停指令
                    //不能调用外层 把  方法 onpause 里的拿出来调用
                    _this.setPlayingState(false);
                    _this.lyricStop();
                    break;

                case "onplay": //播放指令
                    _this.setPlayingState(true);
                    break;

                case "onProgressChange": //改变播放进度指令
                    var currentTime = res.currentTime;
                    _this.draging = false;
                    _this.currentTime = currentTime;
                    _this.seek();
                    _this.isBuffered = _this.curRange.range > currentTime;
                    _this.audio.currentTime = currentTime;
                    _this.play();
                    break;

                case "togglePrev": //切歌指令(上一首)。要确保播放列表也同步一致,歌曲索引一致
                    if (!_this.songReady) {
                        return;
                    }
                    let prevIndex = _this.currentIndex - 1;
                    if (prevIndex == -1) {
                        prevIndex = _this.playlist.length - 1;
                    }
                    _this.setCurrentIndex(prevIndex);
                    _this.songReady = false;
                    break;

                case "toggleNext": //切歌指令(下一首)。要确保播放列表也同步一致,歌曲索引一致
                    if (!_this.songReady) {
                        return;
                    }
                    let nextIndex = _this.currentIndex + 1;
                    if (nextIndex == _this.playlist.length) {
                        nextIndex = 0;
                    }
                    _this.setCurrentIndex(nextIndex);
                    _this.songReady = false;
                    break;
            }
        });

        _this.connection.start().catch((err) => {
            console.log(err);
        });
    },

    /*调用后端方法 SignalR Serve 传入参数*/
    invokeSignalRServe(object) {
        var _this = this;
        object.connectionId = _this.connection.connectionId;
        object.val = true;
        //object为对象类型,如果可用。再序列化为json 字符串。
        //object对象其中要包括actionType属性，代表指令标识。 比如开始、暂停当前播放，切歌 等等
        var jsonPar = JSON.stringify(object);
        console.log(object);
        console.log(jsonPar);
        _this.connection.invoke("SendMessage", jsonPar);
        //当发起方已经完成相应指令后，serve无需再给发起方自己发送指令。或者发送指令，发起方这边不要再重复调用，否则会形成死循环
        //解决方案：那就把Client的 “状态” 都给serve,让serve决定要不要下发指令
    }

}