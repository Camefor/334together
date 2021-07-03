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
        }
    }
}
