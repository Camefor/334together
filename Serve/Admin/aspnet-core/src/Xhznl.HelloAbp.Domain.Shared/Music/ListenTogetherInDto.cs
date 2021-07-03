using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.HelloAbp.Music
{
   public  class ListenTogetherDto
    {

        /// <summary>
        /// 指令类型
        /// </summary>
        public string actionType { get; set; }

        /// <summary>
        /// 客户端接收的方法
        /// </summary>
        public string funcName { get; set; }

        /// <summary>
        /// 客户端连接id
        /// </summary>
        public string connectionId { get; set; }

        /// <summary>
        /// 指示是否正在播放音乐 变量
        /// </summary>
        public bool isPlay { get; set; }

        /// <summary>
        /// 当前进度
        /// </summary>
        public double currentTime { get; set; }

    }

    public enum actionTypeEnum
    {

        onpause,

        onplay,

        onplaying,
        /// <summary>
        /// 拖拽进度条，改变播放进度
        /// </summary>
        onProgressChange
    }
}
