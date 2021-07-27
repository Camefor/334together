using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Xhznl.HelloAbp.Music
{

    /// <summary>
    /// 表示层 和 应用层 传递数据
    /// Dto对象 前端呈现使用的模型类
    /// </summary>
    public class SongSheetDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 和Song 的  AlbumId 关联
        /// </summary>
        public string Dissid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CommitTime { get; set; }
        public string DissName { get; set; }
        public string ImgUrl { get; set; }
        public string Introduction { get; set; }
        public string ListenNum { get; set; }
        public double Score { get; set; }
        public double Version { get; set; }

        public Creator creator { get; set; }
    }

    public class Creator
    {
        public int type { get; set; }

        public int qq { get; set; }

        public string encrypt_uin { get; set; }

        public string name { get; set; }

        public int isVip { get; set; }
        public string avatarUrl { get; set; }
        public int followflag { get; set; }

    }
}
