using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Xhznl.HelloAbp.Music
{
    public class Song : AuditedAggregateRoot<Guid>
    {


        public string Purl { get; set; }
        public int SongId { get; set; }
        public string SongmId { get; set; }
        public string AlbummId { get; set; }
        public string AlbumId { get; set; }

        //singer: [
        //    {

        // id: 3398615,
        // mid: "0032raW44KlFoY",
        // name: "李志",
        //    },
        //  ],

        /// <summary>
        /// 音频文件地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 歌词文件
        /// </summary>
        public string LyricUrl { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string SongName { get; set; }
    }
}
