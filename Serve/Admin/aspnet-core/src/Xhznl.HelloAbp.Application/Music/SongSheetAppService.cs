using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Xhznl.HelloAbp.Music
{
    public class SongSheetAppService : CrudAppService<
            SongSheet, //The Book entity
            SongSheetDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateSongSheetDto>, //Used to create/update a book
        ISongSheetAppService //implement the IBookAppService
    {
        public SongSheetAppService(IRepository<SongSheet, Guid> repository)
            : base(repository)
        {

        }
    }
}
