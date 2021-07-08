using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Infrastructure.Extensions;
using Xhznl.HelloAbp.Music;
using Xhznl.HelloAbp.Music.SignalR;

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


        private readonly IDistributedCache<List<OnlineUser>> _cache;


        public ListenTogetherHub(IDistributedCache<List<OnlineUser>> cache)
        {
            _cache = cache;
        }

        public override Task OnConnectedAsync()
        {

            CreateOnlineUser();

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            DeleteOnlineUser();
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
            var response = new ListenTogetherDto
            {
                funcName = requestParameter.funcName,
                actionType = requestParameter.actionType,
                currentTime = requestParameter.currentTime,
                newSong = requestParameter.newSong,
                index = requestParameter.index,
                list = requestParameter.list
            };//copy object


            //to do:


            var clientProxy = Clients.AllExcept(requestParameter.connectionId);//不给自己发

            await clientProxy.SendAsync("SignalRSendForSongPlayIndex", System.Text.Json.JsonSerializer.Serialize(response));


        }

        public async Task SendMessageInPlayList(string ajsonParameter)
        {
            var requestParameter = ajsonParameter.ToObject<ListenTogetherDto>();
            //记录第一次请求的id 返回的响应不给他了
            var response = new ListenTogetherDto
            {
                funcName = requestParameter.funcName,
                actionType = requestParameter.actionType,
                currentTime = requestParameter.currentTime,
                newSong = requestParameter.newSong,
                index = requestParameter.index,
                list = requestParameter.list,
                mode = requestParameter.mode
            };//copy object
            var clientProxy = Clients.AllExcept(requestParameter.connectionId);//不给自己发
            await clientProxy.SendAsync("SignalRSendForPlayList", System.Text.Json.JsonSerializer.Serialize(response));
        }



        #region "提取方法"

        private void DeleteOnlineUser()
        {
            var disconnectId = Context.ConnectionId;
            var onlineUserInCache = _cache.Get(CacheKeyCollection._cacheKey_online_user);
            onlineUserInCache?.Remove(onlineUserInCache.Where(_ => _.ConnectedId == disconnectId).FirstOrDefault());
            _cache.Set(CacheKeyCollection._cacheKey_online_user, onlineUserInCache);
        }

        /// <summary>
        /// 客户端连接服务器
        /// </summary>
        private void CreateOnlineUser()
        {
            var onlineUserInCache = _cache.Get(CacheKeyCollection._cacheKey_online_user);
            if (onlineUserInCache == null)
            {
                onlineUserInCache = new List<OnlineUser>();
            }
            onlineUserInCache.Add(new OnlineUser
            {
                ConnectedId = Context.ConnectionId,
            });
            _cache.Set(CacheKeyCollection._cacheKey_online_user, onlineUserInCache);
        }

        #endregion "提取方法"
    }
}
