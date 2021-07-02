using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xhznl.HelloAbp.Music
{
    public class CreateUpdateSongSheetDto
    {
        public string Dissid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CommitTime { get; set; }

        [Required]
        public string DissName { get; set; }
        public string ImgUrl { get; set; }
        public string Introduction { get; set; }
        public string ListenNum { get; set; }
        public double Score { get; set; }
        public double Version { get; set; }
    }
}
