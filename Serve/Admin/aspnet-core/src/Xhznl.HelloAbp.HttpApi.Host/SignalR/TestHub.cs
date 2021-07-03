using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Identity;


namespace Xhznl.HelloAbp
{
    //[HubRoute("/my-test-hub")]
    //默认路由:/signalr-hubs/test
    public class TestHub : AbpHub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string targetUserName, string message)
        {
            var client = Clients;

            var currentUserName = CurrentUser.UserName; //Access to the current user info
            var txt = L["MyText"]; //Localization

            //to do:
            //下面的 “主动” 给客户端发消息就去参考api吧
            //await Clients
            //   .User("testid0001")
            //   .SendAsync("ReceiveMessage", message);
            await Clients.All.SendAsync("ReceiveMessage", message);

            /**
             * 设计思路:
             * 1，一个客户端发起 “一起听” 请求，生成 一个邀请链接。【网易云音乐:https://st.music.163.com/listen-together/share/?songId=4154790&roomId=a198fa597a7cdddb7be53846f45e5bed&inviterId=346539201】
             * 2，参考网易云的链接，有一个roomId和&inviterId，抽象房间，这两个id维护，隔离数据，数据缓存在redis里或写入数据库.
             * 3，使用这两个id来实现 两个人一起听歌 ：
             * 4，A客户端发起一个一起听，B客户端点击链接进入 “房间”，记录两个客户端的id，其他数据，当作一个对象集合【使用roomId和&inviterId作为查询主键】
             * 5，目的是 Hub 接受 和 发送数据 都只对 相应的集合里设置数据 返回给特定的客户端， await Clients.User("userId") 如果没有注册功能，那就使用客户端id当作userId
             * 6，最后就是对象集合里要包含的 业务数据，如当前播放音乐id,进度，暂停、播放。
             * 7，上面实现完成后，可以优化，新增聊天功能，新增上线提示音， etc.
             * **/
        }
    }
}
