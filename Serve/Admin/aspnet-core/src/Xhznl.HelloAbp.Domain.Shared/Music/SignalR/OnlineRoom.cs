using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.HelloAbp.Music.SignalR
{
    public class OnlineRoom
    {
        /// <summary>
        /// 房间创建者的连接id
        /// </summary>
        public string CreateConnectedId { get; set; }
        public string RoomId { get; set; }
        public string InviteId { get; set; }

        public List<OnlineUser> OnlineUsers { get; set; }

    }
}
