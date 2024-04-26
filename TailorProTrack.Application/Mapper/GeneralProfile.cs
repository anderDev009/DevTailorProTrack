
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.ProductColor;
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
            CreateMap<OrderProduct, OrderProductDtoGetMapped>()
                .ReverseMap()
                .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore())
                    .ForMember(x => x.InventoryColor, opt => opt.Ignore());
            CreateMap<Order, OrderDtoGetMapped>()
                .ReverseMap()
                   .ForMember(b => b.CREATED_AT, opt => opt.Ignore())
                    .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());


            #region PreOrder
            CreateMap<PreOrder, PreOrderDtoGetMapped>()
                 .ReverseMap()
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #endregion 
            #region PreOrder
            CreateMap<PreOrderProducts, PreOrderProductDtoGetMapped>()
                .ReverseMap()
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());

            CreateMap<PreOrder, PreOrderDtoGetMapped>()
                .ForMember(b => b.DateCreated, opt => opt.MapFrom(src => src.CREATED_AT))
                .ForMember(b => b.DateDelivery, opt => opt.MapFrom(src => src.DATE_DELIVERY))
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
            CreateMap<Color, ColorDtoGetMapped>()
                .ReverseMap()
                  .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #region Product-Color
            //dto get
            CreateMap<ProductColor, ProductColorDtoGet>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(x => x.FkProduct, opt => opt.MapFrom(src => src.FK_PRODUCT))
                .ForMember(x => x.FkColor, opt => opt.MapFrom(src => src.FK_COLOR))
                .ReverseMap()
                   .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            //dto add
            CreateMap<ProductColor, ProductColorDtoAdd>()
                .ForMember(x => x.FkProduct, opt => opt.MapFrom(src => src.FK_PRODUCT))
                .ForMember(x => x.FkColor, opt => opt.MapFrom(src => src.FK_COLOR))
                .ReverseMap()
                  .ForMember(b => b.ID, opt => opt.Ignore())
                  .ForMember(b => b.Product, opt => opt.Ignore())
                  .ForMember(b => b.Color, opt => opt.Ignore())
                  .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                  .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                  .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                  .ForMember(b => b.REMOVED, opt => opt.Ignore());
            //dto remove
            CreateMap<ProductColor, ProductColorDtoRemove>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ID))
                .ReverseMap()
                  .ForMember(b => b.Product, opt => opt.Ignore())
                  .ForMember(b => b.Color, opt => opt.Ignore())
                  .ForMember(b => b.FK_COLOR, opt => opt.Ignore())
                  .ForMember(b => b.FK_PRODUCT, opt => opt.Ignore())
                  .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                  .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                  .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                  .ForMember(b => b.REMOVED, opt => opt.Ignore()); 

            //dto update
            CreateMap<ProductColor,ProductColorDtoUpdate>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(x => x.FkProduct, opt => opt.MapFrom(src => src.FK_PRODUCT))
                .ForMember(x => x.FkColor, opt => opt.MapFrom(src => src.FK_COLOR))
                .ReverseMap()
                  .ForMember(b => b.Product, opt => opt.Ignore())
                  .ForMember(b => b.Color, opt => opt.Ignore())
                  .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                  .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                  .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                  .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
        }
    }
}
