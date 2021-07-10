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
                    OnlineUsers = new List<OnlineUser> { new OnlineUser { ConnectedId = user.ConnectedId } }
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

        public IActionResult GetAllRoom()
        {
            var roomsInCache = _cache.Get(CacheKeyCollection._cacheKey_online_room);
            return Json(roomsInCache);
        }

    }
}
