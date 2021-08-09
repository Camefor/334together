using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Xhznl.HelloAbp.Music;
using Xhznl.HelloAbp.Music.SignalR;

namespace Xhznl.HelloAbp.Controllers
{
    public class MusicController : Controller
    {


        private readonly IDistributedCache<List<OnlineRoom>> _cache;

        public MusicController(IDistributedCache<List<OnlineRoom>> cache)
        {
            _cache = cache;
        }

//        public IActionResult CreateLink()
//        {
//            var client = new RestClient("https://www.helingqi.com/url.php");
//            var request = new RestRequest(Method.POST);
//            //request.AddHeader("postman-token", "71821cd8-d83f-5003-28dc-8f6c6094cb08");
//            request.AddHeader("cache-control", "no-cache");
//            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
//            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW",
//@$"------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=

//\"url\"\r\n\r\nhttp://www.camefor.top/songList?dissid=111&createTime=2020-10-18T00%3A00%3A00&commitTime=2020-10-18T00%3A00%3A00&dissName=%E8%A2%AB%E7%A6%81%E5%BF%8C%E7%9A%84%E6%B8%B8%E6%88%8F%282004%29&imgUrl=https%3A%2F%2F2019334.xyz%2Fshare%2Fcover%2F1.jpg&introduction=&listenNum=2783074&score=0&version=0&creator=%5Bobject+Object%5D&lastModificationTime=&lastModifierId=&creationTime=2021-07-03T01%3A14%3A15.2375798&creatorId=&id=111&roomId=dafd58bd-e66&inviteId=17c4417c-0db\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data;" +
//"    name=\"type\"\r\n\r\ndwz\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
//            IRestResponse response = client.Execute(request);
//        }


        /// <summary>
        /// 发起一起听请求
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult CreateRoom(OnlineUser user)
        {
            try
            {
                if (user?.ConnectedId is null)
                {
                    //return NotFound();
                    return Json(new { code = 500 });
                }

                var onlineRoomsInCache = _cache.Get(CacheKeyCollection._cacheKey_online_room);
                if (onlineRoomsInCache == null)
                {
                    onlineRoomsInCache = new List<OnlineRoom>();
                }
                var roomId = Guid.NewGuid().ToString().Substring(0, 12);
                var inviteId = Guid.NewGuid().ToString().Substring(0, 12);
                var room = new OnlineRoom
                {
                    CreateConnectedId = user.ConnectedId,
                    RoomId = roomId,
                    InviteId = inviteId,
                    OnlineUsers = new List<OnlineUser> {
                        new OnlineUser { ConnectedId = user.ConnectedId ,NickName = user.NickName} }
                };
                onlineRoomsInCache.Add(room);
                _cache.Set(CacheKeyCollection._cacheKey_online_room, onlineRoomsInCache);
                return Json(new { code = 200, roomId, inviteId });
            }
            catch (Exception)
            {
                return Json(new { code = 500 });
            }
        }


        /// <summary>
        /// 接受一起听请求,
        /// </summary>
        /// <returns></returns>
        public IActionResult AcceptListen(OnlineUser user)
        {
            try
            {
                var onlineRoomsInCache = _cache.Get(CacheKeyCollection._cacheKey_online_room);
                var targetRoom = onlineRoomsInCache.Where(c => c.RoomId == user.RoomId).FirstOrDefault();
                if (targetRoom == null)
                {
                    return Json(new { code = 500 });
                }
                targetRoom.OnlineUsers.Add(new OnlineUser
                {
                    ConnectedId = user.ConnectedId,
                    NickName = user.NickName,
                    UserAgent = user.UserAgent
                });
                //onlineRoomsInCache.ReplaceOne(c => c.RoomId == user.RoomId, targetRoom);
                //update room data
                _cache.Set(CacheKeyCollection._cacheKey_online_room, onlineRoomsInCache);
                return Json(new { code = 200, msg = "加入房间成功", targetRoom });
            }
            catch (Exception)
            {
                return Json(new { code = 500 });
            }
        }


        public IActionResult GetAllRoom()
        {
            var roomsInCache = _cache.Get(CacheKeyCollection._cacheKey_online_room);
            return Json(roomsInCache);
        }

        public IActionResult ClearAllRoom()
        {
            _cache.Remove(CacheKeyCollection._cacheKey_online_room);
            return Content("ok");
        }

    }
}
