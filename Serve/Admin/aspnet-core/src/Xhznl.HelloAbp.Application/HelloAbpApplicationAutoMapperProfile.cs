using AutoMapper;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Xhznl.HelloAbp.Music;

namespace Xhznl.HelloAbp
{
    public class HelloAbpApplicationAutoMapperProfile : Profile
    {
        public HelloAbpApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();

            CreateMap<IdentityUserOrgCreateDto, IdentityUserCreateDto>();
            CreateMap<IdentityUserOrgUpdateDto, IdentityUserUpdateDto>();

            CreateMap<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>();

            //AuditLog
            CreateMap<AuditLog, AuditLogDto>()
                .MapExtraProperties();

            CreateMap<EntityChange, EntityChangeDto>()
                .MapExtraProperties();

            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            CreateMap<AuditLogAction, AuditLogActionDto>();

            //Claim
            CreateMap<IdentityClaimType, ClaimTypeDto>().Ignore(x => x.ValueTypeAsString);
            CreateMap<IdentityUserClaim, IdentityUserClaimDto>();
            CreateMap<IdentityUserClaimDto, IdentityUserClaim>().Ignore(x => x.TenantId).Ignore(x => x.Id);
            CreateMap<IdentityRoleClaim, IdentityRoleClaimDto>();
            CreateMap<IdentityRoleClaimDto, IdentityRoleClaim>().Ignore(x => x.TenantId).Ignore(x => x.Id);
            CreateMap<CreateClaimTypeDto, IdentityClaimType>().Ignore(x => x.IsStatic).Ignore(x => x.Id);
            CreateMap<UpdateClaimTypeDto, IdentityClaimType>().Ignore(x => x.IsStatic).Ignore(x => x.Id);

            //my music

            //post data.
            //            //dto to entity
            //            CreateMap<AddressVM, Address>()
            //                .ForMember(dest => dest.Add_Id, opt => opt.MapFrom(f => f.id))
            //                .ForMember(dest => dest.Add_Cus_Id, opt => opt.MapFrom(f => f.cus_id))
            //                .ForMember(dest => dest.Add_Tel, opt => opt.MapFrom(f => AESCrypto.AesEncryptBase(f.phone)))//敏感字段加密
            //                .ForMember(dest => dest.Add_Name, opt => opt.MapFrom(f => AESCrypto.AesEncryptBase(f.name)))
            //                .ForMember(dest => dest.Add_Area, opt => opt.MapFrom(f => f.area))
            //                .ForMember(dest => dest.Add_Detail, opt => opt.MapFrom(f => f.detail))
            //                .ForMember(dest => dest.Add_IsDefault, opt => opt.MapFrom(f => f.isdefault))
            //                ;

            //            //get data
            //            //entity to dto
            //            CreateMap<Address, AddressVM>()
            //                 .BeforeMap((source, dto) =>
            //                 {
            //                     dto.time = Convert.ToDateTime(source.F_LastModifyTime).ToString("yyyy-MM-dd HH:mm:ss");
            //                 })
            //                 .ForMember(dest => dest.id, opt => opt.MapFrom(f => f.Add_Id))
            //                 .ForMember(dest => dest.cus_id, opt => opt.MapFrom(f => f.Add_Cus_Id))
            //                 .ForMember(dest => dest.phone, opt => opt.MapFrom(f => AESCrypto.AesDecryptBase(f.Add_Tel)))
            //                 .ForMember(dest => dest.name, opt => opt.MapFrom(f => AESCrypto.AesDecryptBase(f.Add_Name)))
            //                 .ForMember(dest => dest.area, opt => opt.MapFrom(f => f.Add_Area))
            //                 .ForMember(dest => dest.detail, opt => opt.MapFrom(f => f.Add_Detail))
            //                 .ForMember(dest => dest.isdefault, opt => opt.MapFrom(f => f.Add_IsDefault))
            //                 ;
            //获取 SongSheet 歌单时使用
            CreateMap<SongSheet, SongSheetDto>()
                    .ForMember(dest => dest.dissid, opt => opt.MapFrom(f => f.Dissid))
                    .ForMember(dest => dest.createtime, opt => opt.MapFrom(f => f.CreateTime))
                    .ForMember(dest => dest.commit_time, opt => opt.MapFrom(f => f.CommitTime))
                    .ForMember(dest => dest.dissname, opt => opt.MapFrom(f => f.DissName))
                    .ForMember(dest => dest.imgurl, opt => opt.MapFrom(f => f.ImgUrl))
                 ;

            //创建 SongSheet 歌单时使用
            CreateMap<CreateSongSheetDto, SongSheet>()
              .ForMember(dest => dest.DissName, opt => opt.MapFrom(f => f.dissname))
              .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(f => f.imgurl))
              .ForMember(dest => dest.Introduction, opt => opt.MapFrom(f => f.introduction))
              .ForMember(dest => dest.ListenNum, opt => opt.MapFrom(f => f.listennum))
           ;

        }
    }
}
