using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.HelloAbp.Music.SignalR
{
    public class OnlineRoom
    {

        public string RoomId { get; set; }
        public string InviteId { get; set; }

        public List<OnlineUser> OnlineUsers { get; set; }

    }
}
