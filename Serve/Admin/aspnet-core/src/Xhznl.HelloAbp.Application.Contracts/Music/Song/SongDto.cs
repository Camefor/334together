using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Xhznl.HelloAbp.Music
{
    public class SongDto : AuditedEntityDto<Guid>
    {
        public string Purl { get; set; }
        public int songid { get; set; }
        public string songmid { get; set; }
        public string albummid { get; set; }

        /// <summary>
        /// 和SongSheet 的  Dissid 关联
        /// </summary>
        public string albumid { get; set; }

        /// <summary>
        /// 音频文件地址
        /// </summary>
        public string Url { get; set; }
        public string LyricUrl { get; set; }


        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string SongName { get; set; }
        public string songname { get; set; }

        public PicUrl picurl { get; set; }
        public List<Singer> singer { get; set; }
        //picurl: {
        //    src: "https://2019334.xyz/share/cover/1.jpg",
        //    error:
        //      "https://y.gtimg.cn/music/photo_new/T001R300x300M0000032raW44KlFoY.jpg",
        //  },
    }

    public class Singer
    {
        public int id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }

    }
    public class PicUrl
    {
        public string src { get; set; }
        public string error { get; set; }
    }
}

