using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Xhznl.HelloAbp.Music
{
    public class SongAppService : CrudAppService<
          Song, //The Book entity
          SongDto, //Used to show books
          Guid, //Primary key of the book entity
          PagedAndSortedResultRequestDto //Used for paging/sorting
          >, //Used to create/update a book
      ISongAppService //implement the IBookAppService
    {

        public SongAppService(IRepository<Song, Guid> repository)
         : base(repository)
        {
        }

        public async override Task<PagedResultDto<SongDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var res = await base.GetListAsync(input);
            res.Items.ToList().ForEach(c =>
            {
                c.songname = c.SongName;

                c.singer = new List<Singer> {
                    new Singer
                    {
                        id = 111,
                        mid = "0032raW44KlFoY",
                        name = "李志"
                    }
                };
                c.picurl = new PicUrl
                {
                    src = "https://2019334.xyz/share/cover/1.jpg",
                    error = "https://y.gtimg.cn/music/photo_new/T001R300x300M0000032raW44KlFoY.jpg",
                };
            });
            return res;
        }


    }
}
