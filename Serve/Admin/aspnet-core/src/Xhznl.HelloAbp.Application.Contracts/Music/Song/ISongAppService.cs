using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Xhznl.HelloAbp.Music
{
    public interface ISongAppService : ICrudAppService< //Defines CRUD methods
            SongDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto>
    {

          Task<IEnumerable<SongDto>> GetSongsById(string albumId);
    }
}
