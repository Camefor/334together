using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xhznl.HelloAbp
{
    public class CreateSongSheetDto
    {
        [Required]
        [StringLength(128)]
        public string dissname { get; set; }

        public string imgurl { get; set; }
        public string introduction { get; set; }
        public string listennum { get; set; }
        public double score { get; set; }
        public double version { get; set; }
    }
}
