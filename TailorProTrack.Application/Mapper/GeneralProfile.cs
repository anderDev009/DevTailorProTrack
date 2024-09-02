
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.Application.Dtos.BankAccount;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.CodeDgi;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.Application.Dtos.Expenses.PaymentExpense;
using TailorProTrack.Application.Dtos.NoteCredit;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Dtos.PaymentType;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.ProductColor;
using TailorProTrack.Application.Dtos.ProductSize;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.Application.Dtos.Suppliers;
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

			CreateMap<BuyInventoryDetail, inventoryDetailDtoGet>()
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

			//preorderproduct dto add
			CreateMap<PreOrderProducts, PreOrderProductsDtoAdd>()
				.ForMember(x => x.FkColorPrimary, opt => opt.MapFrom(src => src.COLOR_PRIMARY))
				.ForMember(x => x.FkColorSecondary, opt => opt.MapFrom(src => src.COLOR_SECONDARY))
				.ForMember(x => x.FkProduct, opt => opt.MapFrom(src => src.FK_PRODUCT))
				.ForMember(x => x.FkSize, opt => opt.MapFrom(src => src.FK_SIZE))
				.ForMember(x => x.Quantity, opt => opt.MapFrom(src => src.QUANTITY))
				.ForMember(x => x.customPrice, opt => opt.MapFrom(src => src.CUSTOM_PRICE))
				.ForMember(x => x.User, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());





			CreateMap<PreOrder, PreOrderDtoGetMapped>()
				.ForMember(b => b.IsEditable, opt => opt.Ignore())
				.ForMember(b => b.DateCreated, opt => opt.MapFrom(src => src.CREATED_AT))
				.ForMember(b => b.DateDelivery, opt => opt.MapFrom(src => src.DATE_DELIVERY))
				.ForMember(x => x.Finished, src => src.MapFrom(x => x.FINISHED))
				.ReverseMap()
					.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
					.ForMember(b => b.USER_MOD, opt => opt.Ignore())
					.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
					.ForMember(b => b.REMOVED, opt => opt.Ignore())
					.ForMember(b => b.ITBIS, opt => opt.Ignore());

			#endregion
			#region Size
			CreateMap<Size, SizeDtoGetMapped>()
				.ForMember(x => x.Category, src => src.MapFrom(data => data.categorySize.CATEGORY))
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
				.ForMember(x => x.L_name, opt => opt.MapFrom(m => m.LAST_SURNAME))
				.ForMember(x => x.F_surname, opt => opt.MapFrom(m => m.FIRST_SURNAME))
				.ForMember(x => x.L_surname, opt => opt.MapFrom(m => m.LAST_SURNAME))
				.ForMember(x => x.L_surname, opt => opt.MapFrom(m => m.LAST_NAME))
				.ForMember(x => x.Dni, opt => opt.MapFrom(m => m.DNI))
				.ForMember(x => x.RNC, opt => opt.MapFrom(m => m.RNC))
				.ForMember(x => x.HasNoteCredit, opt => opt.MapFrom(x => x.NoteCredit.FirstOrDefault().AMOUNT == null ? false: x.NoteCredit.FirstOrDefault().AMOUNT > 0))
				.ForMember(x => x.AmountNoteCredit, opt => opt.MapFrom(x => x.NoteCredit.FirstOrDefault().AMOUNT  == null? 0 : x.NoteCredit.First().AMOUNT))
				 .ReverseMap()
					.ForMember(x => x.Order, opt => opt.Ignore())
					.ForMember(x => x.PreOrder, opt => opt.Ignore())
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
			#region PaymentType

			CreateMap<PaymentType, PaymentTypeDtoGet>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID));
			#endregion
			#region BankAccount

			CreateMap<BankAccount, BankAccountDtoGet>();
			#endregion
			#region Expenses

			CreateMap<Expenses, ExpensesDtoAdd>()

				.ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
				.ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
				.ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.DOCUMENT_NUMBER))
				.ForMember(x => x.Completed, src => src.MapFrom(x => x.COMPLETED))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());

			CreateMap<Expenses, ExpensesDtoUpdate>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
				.ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
				.ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
				.ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.VOUCHER))
				.ForMember(x => x.Completed, src => src.MapFrom(x => x.COMPLETED))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());


			CreateMap<Expenses, ExpensesDtoGet>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
				.ForMember(x => x.Name, src => src.MapFrom(x => x.NAME))
				.ForMember(x => x.Description, src => src.MapFrom(x => x.DESCR))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.Voucher, src => src.MapFrom(x => x.VOUCHER))
				.ForMember(x => x.DocumentNumber, src => src.MapFrom(x => x.VOUCHER))
				.ForMember(x => x.Completed, src => src.MapFrom(x => x.COMPLETED))
				  .ReverseMap()
				   .ForMember(b => b.USER_CREATED, opt => opt.Ignore())
					.ForMember(b => b.USER_MOD, opt => opt.Ignore())
					.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
					.ForMember(b => b.REMOVED, opt => opt.Ignore());
			#endregion
			#region PaymentExpenses

			CreateMap<PaymentExpenses, PaymentExpenseDtoAdd>()
				.ForMember(x => x.IdExpense, src => src.MapFrom(x => x.FK_EXPENSE))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))

				.ForMember(x => x.IdPaymentType, src => src.MapFrom(x => x.FK_PAYMENT_TYPE
				))
				.ForMember(x => x.IdBankAccount, src => src.MapFrom(x => x.FK_BANK_ACCOUNT))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore())
				.ForMember(x => x.PaymentType, opt => opt.Ignore())
				.ForMember(x => x.BankAccount, opt => opt.Ignore())
				.ForMember(x => x.Expense
					, opt => opt.Ignore());

			CreateMap<PaymentExpenses, PaymentExpenseDtoUpdate>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID
				))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.IdExpense, src => src.MapFrom(x => x.FK_EXPENSE))
				.ForMember(x => x.IdPaymentType, src => src.MapFrom(x => x.FK_PAYMENT_TYPE
				))
				.ForMember(x => x.IdBankAccount, src => src.MapFrom(x => x.FK_BANK_ACCOUNT))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore())
				.ForMember(x => x.PaymentType, opt => opt.Ignore())
				.ForMember(x => x.BankAccount, opt => opt.Ignore())
				.ForMember(x => x.Expense
					, opt => opt.Ignore());

			CreateMap<PaymentExpenses, PaymentExpenseDtoGet>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
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
			   .ForMember(x => x.Fecha, opt => opt.MapFrom(x => x.CREATED_AT))
			   .ForMember(x => x.ClientName, opt => opt.MapFrom(x => x.PreOrder.Client.FIRST_NAME + " "
			   + x.PreOrder.Client.LAST_NAME + " " + x.PreOrder.Client.FIRST_SURNAME + " " + x.PreOrder.Client.LAST_SURNAME))
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

			#region Suppliers
			CreateMap<Supplier, SuppliersDtoAdd>()
				.ForMember(x => x.Nombre, opt => opt.MapFrom(x => x.NAME_SUPPLIER))
				.ReverseMap()
				.ForMember(x => x.ID, opt => opt.Ignore());

			CreateMap<Supplier, SuppliersDtoGet>()
                .ForMember(x => x.Nombre, opt => opt.MapFrom(x => x.NAME_SUPPLIER))
                .ReverseMap()
				.ForMember(x => x.REMOVED, opt => opt.Ignore())
				.ForMember(x => x.USER_CREATED, opt => opt.Ignore())
				.ForMember(x => x.USER_MOD, opt => opt.Ignore())
				.ForMember(x => x.MODIFIED_AT, opt => opt.Ignore());


            CreateMap<Supplier, SuppliersDtoUpdate>()
                .ForMember(x => x.Nombre, opt => opt.MapFrom(x => x.NAME_SUPPLIER))
                .ReverseMap()
                .ForMember(x => x.REMOVED, opt => opt.Ignore())
                .ForMember(x => x.USER_CREATED, opt => opt.Ignore())
                .ForMember(x => x.USER_MOD, opt => opt.Ignore())
                .ForMember(x => x.MODIFIED_AT, opt => opt.Ignore());
            #endregion
            #region CodeDgi

            CreateMap<CodesDgi, CodeDgiDtoAdd>()
				.ReverseMap()
				.ForMember(x => x.ID, opt => opt.Ignore())
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());

			CreateMap<CodesDgi, CodeDgiDtoUpdate>()
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());

			CreateMap<CodesDgi, CodeDgiDtoGet>()
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());
			#endregion
			#region NoteCredit
			CreateMap<NoteCredit, NoteCreditDtoGet>()
				.ForMember(x => x.Id, src => src.MapFrom(x => x.ID))
				.ForMember(x => x.Amount, src => src.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.DateCreated, src => src.MapFrom(x => x.CREATED_AT))
				.ForMember(x => x.Client, src => src.MapFrom(x => x.Client))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());

			CreateMap<NoteCredit, NoteCreditDtoAdd>()
				.ForMember(x => x.Amount, opt => opt.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.FkClient, opt => opt.MapFrom(x => x.FK_CLIENT))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());

			CreateMap<NoteCredit, NoteCreditDtoUpdate>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.ID))
				.ForMember(x => x.Amount, opt => opt.MapFrom(x => x.AMOUNT))
				.ForMember(x => x.FkClient, opt => opt.MapFrom(x => x.FK_CLIENT))
				.ReverseMap()
				.ForMember(b => b.USER_CREATED, opt => opt.Ignore())
				.ForMember(b => b.USER_MOD, opt => opt.Ignore())
				.ForMember(b => b.MODIFIED_AT, opt => opt.Ignore())
				.ForMember(b => b.REMOVED, opt => opt.Ignore());
			#endregion
		}
	}
}
