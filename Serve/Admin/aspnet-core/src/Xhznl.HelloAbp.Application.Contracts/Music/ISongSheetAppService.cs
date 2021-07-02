using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Xhznl.HelloAbp.Music
{
    public interface ISongSheetAppService: ICrudAppService< //Defines CRUD methods
            SongSheetDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateSongSheetDto>
    {

    }
}
