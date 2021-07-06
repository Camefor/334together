using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Infrastructure.Extensions;
using Xhznl.HelloAbp.Music;

namespace Xhznl.HelloAbp.SignalR
{
    //[DisableConventionalRegistration]//如果想要禁用集线器类自动添加依赖注入
    [HubRoute("/hubs/listenTogether")]
    public class ListenTogetherHub : AbpHub
    {
        public static List<string> AllClients = new List<string>();
        public static Dictionary<string, ListenTogetherDto> data_onpause = new Dictionary<string, ListenTogetherDto>();
        public static Dictionary<string, ListenTogetherDto> data_onplay = new Dictionary<string, ListenTogetherDto>();
        public static int Counter = 0;
        public override Task OnConnectedAsync()
        {
            AllClients.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 接收客户端发送的数据
        /// </summary>
        /// <param name="ajsonParameter">统一格式为json</param>
        /// <returns></returns>
        public async Task SendMessageInSongPlayIndex(string ajsonParameter)
        {
            var requestParameter = ajsonParameter.ToObject<ListenTogetherDto>();

            //记录第一次请求的id 返回的响应不给他了
            var response = new ListenTogetherDto {
                funcName = requestParameter.funcName,
                actionType = requestParameter.actionType,
                currentTime = requestParameter.currentTime,
                newSong = requestParameter.newSong,
                index  =requestParameter.index,
                list = requestParameter.list
            };//copy object


            //switch (requestParameter.actionType)
            //{
            //    //暂停播放
            //    case "onpause":
            //        if (data_onpause.ContainsKey(requestParameter.connectionId)) data_onpause.Remove(requestParameter.connectionId);
            //        data_onpause.Add(requestParameter.connectionId, new ListenTogetherDto {
            //            connectionId = requestParameter.connectionId,
            //            isPlay = requestParameter.isPlay
            //        });
            //        break;
            //    case "onplay":
            //        if (data_onplay.ContainsKey(requestParameter.connectionId)) data_onplay.Remove(requestParameter.connectionId);
            //        data_onplay.Add(requestParameter.connectionId, new ListenTogetherDto {
            //            connectionId = requestParameter.connectionId,
            //            isPlay = requestParameter.isPlay
            //        });
            //        break;
            //    default:
            //        break;
            //}

            //to do:
            //如果部分客户端的 "值" 都一样了 就不下发指令了
            //确定要给谁发送
            //下面的 “主动” 给客户端发消息就去参考api吧
            //var s = data_onpause.Where(c => c.Value.isPlay).Select(c => c.Value.connectionId).ToList();
            //var s2 = data_onplay.Where(c => !c.Value.isPlay).Select(c => c.Value.connectionId).ToList();
            //if (s.Count == 0 && s2.Count == 0)
            //{
            //    s.AddRange(AllClients);
            //}
            ////var clientProxy = Clients.Clients(s);//发送暂停音乐时，只给那些正在播放音乐的发送指令


            var clientProxy = Clients.AllExcept(requestParameter.connectionId);//不给自己发

            await clientProxy.SendAsync("SignalRSendForSongPlayIndex", System.Text.Json.JsonSerializer.Serialize(response));


        }     
        
        public async Task SendMessageInPlayList(string ajsonParameter)
        {
            var requestParameter = ajsonParameter.ToObject<ListenTogetherDto>();
            //记录第一次请求的id 返回的响应不给他了
            var response = new ListenTogetherDto {
                funcName = requestParameter.funcName,
                actionType = requestParameter.actionType,
                currentTime = requestParameter.currentTime,
                newSong = requestParameter.newSong,
                index  =requestParameter.index,
                list = requestParameter.list,
                mode =requestParameter.mode
            };//copy object
            var clientProxy = Clients.AllExcept(requestParameter.connectionId);//不给自己发
            await clientProxy.SendAsync("SignalRSendForPlayList", System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}
