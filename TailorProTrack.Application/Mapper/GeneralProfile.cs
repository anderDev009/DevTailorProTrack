
using AutoMapper;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            #region BuyInventory // Compras
            CreateMap<BuyInventory, BuyInventoryDtoAdd>()
                .ForMember(b => b.InventoryDetailDtoAdd, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());

            CreateMap<BuyInventory, BuyInventoryDtoUpdate>()
                .ReverseMap()
                  .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());

            CreateMap<BuyInventory, BuyInventoryDtoGet>()
                .ReverseMap()
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                 .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                 .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                 .ForMember(b => b.REMOVED, opt => opt.Ignore()); 

            #endregion
            #region BuyInventoryDetail // Detalle de compra
            CreateMap<BuyInventoryDetail, InventoryDetailDtoAdd>()
                .ReverseMap()
                .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());

            CreateMap<BuyInventoryDetail, InventoryDetailDtoUpdate>()
                .ReverseMap()
                .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());

            #endregion
            #region Product
            CreateMap<Product, ProductDtoGetMapped>()
                .ReverseMap()
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #region Size
            CreateMap<Size, SizeDtoGetMapped>()
                .ReverseMap()
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                 .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                 .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                 .ForMember(b => b.REMOVED, opt => opt.Ignore()); 
            #endregion
        }
    }
}
