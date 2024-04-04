
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
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
            #region Order -- OrderProduct
            //CreateMap<Order,OrderDtoGetMapped>
            #region PreOrder
            CreateMap<PreOrder,PreOrderDtoGetMapped>()
                 .ReverseMap()
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #endregion 
            #region preorderproduct
            CreateMap<PreOrderProducts,PreOrderProductDtoGetMapped>()
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
            #region Client 
            CreateMap<Client, ClientDtoGet>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.ID))
                .ForMember(x => x.F_name, opt => opt.MapFrom(m => m.FIRST_NAME))
                .ForMember(x => x.F_surname, opt => opt.MapFrom(m => m.FIRST_SURNAME))
                .ForMember(x => x.L_surname, opt => opt.MapFrom(m => m.LAST_SURNAME))
                .ForMember(x => x.L_surname, opt => opt.MapFrom(m => m.LAST_NAME))
                .ForMember(x => x.Dni, opt => opt.MapFrom(m => m.DNI))
                .ForMember(x => x.RNC, opt => opt.MapFrom(m => m.RNC))
                 .ReverseMap()
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #region Color
            CreateMap<Color,ColorDtoGetMapped>()
                .ReverseMap()
                  .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
        }
    }
}
