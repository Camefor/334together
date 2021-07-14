import * as signalR from "@microsoft/signalr";
//服务器地址
const signal = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:44370/hubs/listenTogether', {})
    .configureLogging(signalR.LogLevel.Information)
    .build()

const signalr = function () {
    var hub
    if (hub === undefined) {
        hub = signal
    }
    return hub
}
//  自动重连
async function start() {
    try {
        await signal.start()
        console.log(signal);
        console.log('serve connected')
        // console.log(signal)
        console.log('your  connectionId js ', signal.connectionId);
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