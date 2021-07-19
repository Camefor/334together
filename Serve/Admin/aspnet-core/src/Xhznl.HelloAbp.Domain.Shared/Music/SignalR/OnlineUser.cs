using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.HelloAbp.Music.SignalR
{
    public class OnlineUser
    {
        public string ConnectedId { get; set; }

        public string UserAgent { get; set; }

        public string NickName { get; set; }

        public string RoomId { get; set; }
        public string InviteId { get; set; }

    }
}
