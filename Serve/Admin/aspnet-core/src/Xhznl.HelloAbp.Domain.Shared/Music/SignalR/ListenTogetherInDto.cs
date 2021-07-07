using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.HelloAbp.Music
{
    public class ListenTogetherDto
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



        /// <summary>
        /// 切歌 （如果可用）
        /// </summary>
        public SongModel newSong { get; set; }

        /// <summary>
        /// 如果可用 代表 当前播放的播放列表的歌曲索引
        /// </summary>
        public int index { get; set; }


        /// <summary>
        /// 待播放列表 如果可用，初始化建立连接时传递 
        /// </summary>
        public List<SongModel> list { get; set; }


        /// <summary>
        /// 播放模式：
        /// 0：顺序播放
        /// 1:循环播放
        /// 2：随机播放
        /// </summary>
        public int mode { get; set; }
    }

    public class SongModel
    {

        public long id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string singer { get; set; }
        public string url { get; set; }
        public picurlModel picurl { get; set; }


        public string albummid { get; set; }
        public string albumid { get; set; }

        //        {
        //    "id": 285993854,
        //    "mid": "000BjWD738n3mi",
        //    "name": "伯虎说 (feat.唐伯虎Annie)",
        //    "singer": "伯爵Johnny / 唐伯虎Annie",
        //    "url": "http://dl.stream.qqmusic.qq.com/C400003mJ8Wr0bxVjq.m4a?guid=5165714425&vkey=7644ECDE31526859FB072B87501FD77622A0FE7B3DBCC594F18A7047C3D5D9F5E174C5BFCC35D3CD3698E424B3F72E09071FFB66DD7D7279&uin=&fromtag=38",
        //    "picurl": {
        //        "src": "https://y.gtimg.cn/music/photo_new/T002R300x300M000002ESytn2uEU7A.jpg?max_age=2592000",
        //        "error": "https://y.gtimg.cn/music/photo_new/T001R300x300M000001Dd1hY0CGuxZ.jpg"
        //    },
        //    "albummid": "002ESytn2uEU7A",
        //    "albumid": 15746559
        //}
      
    }
    public class picurlModel
    {
        public string src { get; set; }
        public string error { get; set; }
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
