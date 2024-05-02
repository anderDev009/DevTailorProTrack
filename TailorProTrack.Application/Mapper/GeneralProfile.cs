
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.ProductColor;
using TailorProTrack.Application.Dtos.ProductSize;
using TailorProTrack.Application.Dtos.Sale;
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
                .ForMember(b => b.IsEditable, opt => opt.Ignore())
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
            CreateMap<ProductColor, ProductColorDtoUpdate>()
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
            #region Expenses
            CreateMap<Expenses, ExpensesDtoAdd>()
                .ForMember(x => x.IdPaymentType, src => src.MapFrom(x => x.FK_PAYMENT_TYPE))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
                .ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
                .ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
                .ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
                .ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.VOUCHER))
                  .ReverseMap()
                   .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore())
                    .ForMember(b => b.PaymentType, opt => opt.Ignore());

            CreateMap<Expenses, ExpensesDtoUpdate>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
                .ForMember(x => x.IdPaymentType, src => src.MapFrom(x => x.FK_PAYMENT_TYPE))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
                .ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
                .ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
                .ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
                .ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.VOUCHER))
                  .ReverseMap()
                   .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore())
                    .ForMember(b => b.PaymentType, opt => opt.Ignore());

            CreateMap<Expenses, ExpensesDtoGet>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
                .ForMember(x => x.IdPaymentType, src => src.MapFrom(x => x.FK_PAYMENT_TYPE))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
                .ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
                .ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
                .ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
                .ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.VOUCHER))
                .ForMember(x => x.PaymentType, src => src.MapFrom(x => x.PaymentType.TYPE_PAYMENT))
                  .ReverseMap()
                   .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                    .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                    .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                    .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #region Sales
            CreateMap<Sales, SaleDtoAdd>()
                .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
                .ForMember(x => x.FkOrder, opt => opt.MapFrom(x => x.FK_PREORDER))
                .ForMember(x => x.Itbis, opt => opt.MapFrom(x => x.ITBIS))
                .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
                .ReverseMap()
                .ForMember(x => x.TOTAL_AMOUNT, opt => opt.Ignore())
                 .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                 .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                 .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                 .ForMember(b => b.REMOVED, opt => opt.Ignore());

            CreateMap<Sales, SaleDtoUpdate>()
               .ForMember(x => x.Id, opt => opt.MapFrom(x => x.ID))
               .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
               .ForMember(x => x.FkOrder, opt => opt.MapFrom(x => x.FK_PREORDER))
               .ForMember(x => x.Itbis, opt => opt.MapFrom(x => x.ITBIS))
               .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
               .ReverseMap()
               .ForMember(x => x.TOTAL_AMOUNT, opt => opt.Ignore())
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());


            CreateMap<Sales, SaleDtoGet>()
               .ForMember(x => x.Id, opt => opt.MapFrom(x => x.ID))
               .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
               .ForMember(x => x.FkOrder, opt => opt.MapFrom(x => x.FK_PREORDER))
               .ForMember(x => x.Itbis, opt => opt.MapFrom(x => x.ITBIS))
               .ForMember(x => x.CodIsc, opt => opt.MapFrom(x => x.COD_ISC))
               .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.TOTAL_AMOUNT))
               .ReverseMap()
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());
            #endregion
            #region Product - Size
            CreateMap<ProductSize, ProductSizeDtoAdd>()
                .ForMember(x => x.IdProduct, src => src.MapFrom(x => x.FK_PRODUCT))
                .ForMember(x => x.IdSize, src => src.MapFrom(x => x.FK_SIZE))
                .ReverseMap()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Size, opt => opt.Ignore())
                //auditable properties
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());
            
            CreateMap<ProductSize, ProductSizeDtoUpdate>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
                .ForMember(x => x.IdProduct, src => src.MapFrom(x => x.FK_PRODUCT))
                .ForMember(x => x.IdSize, src => src.MapFrom(x => x.FK_SIZE))
                .ReverseMap()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Size, opt => opt.Ignore())
                //auditable properties
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());


            CreateMap<ProductSize, ProductSizeDtoGet>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
                .ForMember(x => x.IdProduct, src => src.MapFrom(x => x.FK_PRODUCT))
                .ForMember(x => x.IdSize, src => src.MapFrom(x => x.FK_SIZE))
                .ReverseMap()
                //auditable properties
                .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
                .ForMember(b => b.USER_MOD, opt => opt.Ignore())
                .ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
                .ForMember(b => b.REMOVED, opt => opt.Ignore());

            #endregion
        }
    }
}
