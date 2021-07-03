using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Xhznl.HelloAbp.SignalR
{
    [HubRoute("/hubs/listenTogetherHub")]
    public class ListenTogetherHub : AbpHub
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


            //to do:
            //下面的 “主动” 给客户端发消息就去参考api吧
            //await Clients
            //   .User("testid0001")
            //   .SendAsync("ReceiveMessage", message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
