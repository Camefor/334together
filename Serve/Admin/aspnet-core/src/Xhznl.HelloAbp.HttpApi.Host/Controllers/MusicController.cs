using Microsoft.AspNetCore.Mvc;
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
