using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Xhznl.HelloAbp.Music;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    public static class HelloAbpDbContextModelCreatingExtensions
    {
        public static void ConfigureHelloAbp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(HelloAbpConsts.DbTablePrefix + "YourEntities", HelloAbpConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            //将实体映射到数据库表
            builder.Entity<SongSheet>(b =>
            {

                b.ToTable(HelloAbpConsts.DbTablePrefix + "SongSheets",
                    HelloAbpConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Dissid).IsRequired().HasMaxLength(128);
            });
        }
    }
}