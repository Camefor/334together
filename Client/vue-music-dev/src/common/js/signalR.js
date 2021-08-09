import * as signalR from "@microsoft/signalr";
import { getUrlKey } from "@/config/util";
import $ from "zepto";


//服务器地址
const signal = new signalR.HubConnectionBuilder()
    .withUrl('http://140.82.13.152:8888/hubs/listenTogether', {})
    .configureLogging(signalR.LogLevel.Information)
    .build()

const signalr = function () {
    var hub
    if (hub === undefined) {
        hub = signal
    }
    return hub
}

function acceptListen() {
    var _this = this;
    var url = "http://140.82.13.152:8888/music/acceptListen";
    var r = sessionStorage.getItem("roomId");
    var i = sessionStorage.getItem("inviteId");
    console.log(_this.signalr);
    var param = {
        ConnectedId: _this.signalr.connectionId,
        UserAgent: "hahhh",
        NickName: "我是一个用户2",
        RoomId: r,
        InviteId: i,
    };

    $.ajax({
        url,
        method: "post",
        data: param,
        success(res) {
            console.log(res);
        },
        error(xhr, errType, err) {
            console.error(errType);
        },
    });
}

//  自动重连
async function start() {
    try {
        await signal.start()
        console.log(signal);
        console.log('serve connected')
        // console.log(signal)
        console.log('your  connectionId js ', signal.connectionId);

        var roomId = getUrlKey("roomId", window.location.href); //&roomId=12121&inviteId=1211
        var inviteId = getUrlKey("inviteId", window.location.href);
        if (!!roomId) {
            console.log(roomId);
            var url = "http://140.82.13.152:8888/music/acceptListen";
            var param = {
                ConnectedId: signal.connectionId,
                UserAgent: "hahhh",
                NickName: "我是一个用户2",
                RoomId: roomId,
                InviteId: inviteId,
            };

            $.ajax({
                url,
                method: "post",
                data: param,
                success(res) {
                    console.log(res);
                },
                error(xhr, errType, err) {
                    console.error(errType);
                },
            });
        }

    } catch (err) {
        console.log(err)
        setTimeout(() => start(), 5000)
    }
}
start();
signal.onclose(async () => {
    await start()
})
// install方法的第一个参数是 Vue 构造器，第二个参数是一个可选的选项对象。
export default {
    install: function (Vue) {
        Vue.prototype.signalr = signal
    }
}

//https://www.cnblogs.com/zoe-zyq/p/12049307.html