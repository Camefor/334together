using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

/// <summary>
/// 包含你的实体, 领域服务和其他核心域对象
/// </summary>
/// 

namespace Xhznl.HelloAbp
{
    /// <summary>
    /// http://localhost:8080/api/getSongSheetList?sin=0&ein=9
    /// </summary>
    public class SongSheet: AuditedAggregateRoot<Guid>
    {
        public string Dissid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CommitTime { get; set; }
        public string DissName { get; set; }
        public string ImgUrl { get; set; }
        public string Introduction { get; set; }
        public string ListenNum { get; set; }
        public double Score { get; set; }
        public double Version { get; set; }


        //public string dissid { get; set; }

        //public DateTime createtime { get; set; }

        //public DateTime commit_time { get; set; }

        //public string dissname { get; set; }

        //public string imgurl { get; set; }
        //public string introduction { get; set; }
        //public string listennum { get; set; }
        //public double score { get; set; }
        //public double version { get; set; }

    }
}
