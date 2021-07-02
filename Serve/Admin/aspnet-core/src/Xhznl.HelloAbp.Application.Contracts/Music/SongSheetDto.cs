using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Xhznl.HelloAbp
{

    /// <summary>
    /// 表示层 和 应用层 传递数据
    /// Dto对象 前端呈现使用的模型类
    /// </summary>
    public class SongSheetDto : AuditedEntityDto<Guid>
    {
        public string dissid { get; set; }
        public DateTime createtime { get; set; }

        public DateTime commit_time { get; set; }

        public string dissname { get; set; }

        public string imgurl { get; set; }
        public string introduction { get; set; }
        public string listennum { get; set; }
        public double score { get; set; }
        public double version { get; set; }
    }
}
