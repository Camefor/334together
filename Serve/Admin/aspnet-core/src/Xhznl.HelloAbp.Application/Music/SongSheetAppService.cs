using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Xhznl.HelloAbp.Music.SignalR;

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

        private readonly IDistributedCache<OnlineUser> _cache;
        private static string _cacheKey = "k123";

        public SongSheetAppService(IRepository<SongSheet, Guid> repository, IDistributedCache<OnlineUser> cache)
            : base(repository)
        {
            _cache = cache;
        }

        public OnlineUser TestGet(string key)
        {
            return _cache.Get(key);
        }


        public override Task<PagedResultDto<SongSheetDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var onlineUserInCache = _cache.Get(_cacheKey);

            var onlineUser = new OnlineUser {
                ConnectedId = "sdjklwrje34",
                NickName = "小红" + DateTime.Now.ToString()
            };
            _cache.Set(_cacheKey, onlineUser);

            var onlineUserInCache2 = _cache.Get(_cacheKey);

            return base.GetListAsync(input);
        }

        public void TestSet(string key, OnlineUser onlineUser)
        {
            _cache.Set(key, onlineUser);
        }

        private OnlineUser GetBookFromDatabaseAsync()
        {
            return new OnlineUser {
                ConnectedId = "sdjklwrje34",
                NickName = "小红"
            };
        }
    }
}
