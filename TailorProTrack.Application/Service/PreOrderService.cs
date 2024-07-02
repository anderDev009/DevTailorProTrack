using System.Xml.XPath;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
	public class PreOrderService : IPreOrderService
	{

		//repositorios
		private readonly IPreOrderRepository _preOrderRepository;

		private readonly IPaymentRepository _paymentRepository;

		private readonly IProductRepository _productRepository;

		private readonly IPreOrderProductsRepository _preOrderProductRepository;
		private readonly ISizeRepository _sizeRepository;
		private readonly IColorRepository _colorRepository;
		//mapper
		private readonly IMapper _mapper;
		//orderService
		private readonly IOrderService _orderService;
		//servicios 
		private readonly IPreOrderProductService _preOrderProductService;
		//
		private readonly IClientService _clientService;
		public PreOrderService(IPreOrderRepository preOrderRepository,
						IPreOrderProductService preOrderProductService,
						IPaymentRepository paymentRepository,
						IPreOrderProductsRepository preOrderProductRepository,
						IProductRepository productRepository,
						ISizeRepository sizeRepository,
						IColorRepository colorRepository,
						IClientService clientService,
						IOrderService orderService, IMapper mapper)
		{
			_preOrderRepository = preOrderRepository;
			_preOrderProductService = preOrderProductService;
			_clientService = clientService;
			_mapper = mapper;
			_paymentRepository = paymentRepository;
			_preOrderProductRepository = preOrderProductRepository;
			_productRepository = productRepository;
			_sizeRepository = sizeRepository;
			_colorRepository = colorRepository;
			_orderService = orderService;
		}

		public ServiceResult Add(PreOrderDtoAdd dtoAdd)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				PreOrder preOrder = new PreOrder
				{
					FK_CLIENT = dtoAdd.FkClient,
					DATE_DELIVERY = dtoAdd.DateDelivery,
					COMPLETED = false,
					FINISHED = null,
				};

				int id = this._preOrderRepository.Save(preOrder);
				var data = this._preOrderProductService.SaveMany(dtoAdd.productsDtoAdds.Select(data => new PreOrderProductsDtoAdd
				{
					FkColorPrimary = data.FkColorPrimary,
					FkColorSecondary = data.FkColorSecondary,
					FkProduct = data.FkProduct,
					FkSize = data.FkSize,
					Quantity = data.Quantity,
					User = dtoAdd.User,
					customPrice = data.customPrice
				}).ToList(), id);
				result.Message = data.Message;

			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al agregar el pedido: {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetAccountsReceivable()
		{
			ServiceResult result = new();
			try
			{
				var report = _preOrderRepository.GetAccountsReceivable();
				List<PreOrderDtoGetMapped> preOrders = _mapper.Map<List<PreOrderDtoGetMapped>>(report);
				List<PreOrderDtoGetMapped> preOrdersToSend = new();
				foreach (var item in preOrders)
				{
					item.Amount = _paymentRepository.GetAmountPendingByIdPreOrder(item.ID);
					if (item.Amount < 0)
					{
						item.Amount = Math.Abs((decimal)item.Amount);
						preOrdersToSend.Add(item);
					}


				}

				result.Data = preOrdersToSend;
				result.Message = "Obtenidos con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error: {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetPreOrdersNotCompleted()
		{
			ServiceResult result = new ServiceResult();
			try
			{

				var preOrders = this._preOrderRepository.SearchEntities()
					.Include(x => x.PreOrderProducts)
					.ThenInclude(x => x.Size)
					.Include(x => x.PreOrderProducts)
					.ThenInclude(x => x.Product)
					.Include(x => x.PreOrderProducts)
					.ThenInclude(x => x.ColorPrimary)
					.Include(x => x.PreOrderProducts)
					.ThenInclude(x => x.ColorSecondary)
					.Include(x => x.Client)
					.Where(data => (bool)data.COMPLETED == false && !data.REMOVED)
					.ToList();


				var preOrdersMapped = _mapper.Map<List<PreOrderDtoGetMapped>>(preOrders);
				foreach (var item in preOrdersMapped)
				{

					item.AmountBase = _paymentRepository.GetAmountPendingByIdPreOrder(item.ID);
					if (item.AmountBase >= 0)
					{
						_preOrderRepository.Complete(item.ID);
					}
					if (item.AmountBase < 0)
					{
						item.AmountBase = Math.Abs((decimal)item.AmountBase);

					}

					item.IsCompleted = _orderService.ConfirmOrdersIsComplete(item.ID);
				}

				result.Data = preOrdersMapped;
				result.Message = "Obtenidos con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Erro al obtener: {ex.Message}";
			}

			return result;
		}

		public ServiceResultWithHeader GetAll(PaginationParams @params)
		{
			ServiceResultWithHeader result = new ServiceResultWithHeader();
			try
			{
				var countRegister = this._preOrderRepository.CountEntities();
				PaginationMetaData header = new PaginationMetaData(countRegister, @params.Page, @params.ItemsPerPage);

				var preOrders = this._preOrderRepository.SearchEntities()
														.Include(x => x.PreOrderProducts)
														   .ThenInclude(x => x.Size)
														.Include(x => x.PreOrderProducts)
														   .ThenInclude(x => x.Product)
														.Include(x => x.PreOrderProducts)
														   .ThenInclude(x => x.ColorPrimary)
														.Include(x => x.PreOrderProducts)
															.ThenInclude(x => x.ColorSecondary)
														.Include(x => x.Client)
														.Skip((@params.Page - 1) * @params.ItemsPerPage)
														.OrderBy(x => x.FINISHED == null)
														.Take(@params.ItemsPerPage).Where(data => !data.REMOVED && !(bool)data.COMPLETED).ToList();


				var preOrdersMapped = _mapper.Map<List<PreOrderDtoGetMapped>>(preOrders);
				//recorriendo las ordenes y confirmando si estan completas
				foreach (var preOrder in preOrdersMapped)
				{
					if (preOrder.Finished == null || preOrder.Finished == false)
					{
						preOrder.IsCompleted = _orderService.ConfirmOrdersIsComplete(preOrder.ID);
					}
				}
				//enviando data
				result.Data = preOrdersMapped;
				result.Header = header;
				result.Message = "Obtenidos con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Erro al obtener: {ex.Message}";
			}

			return result;
		}

		public ServiceResult GetById(int id)
		{
			ServiceResult result = new ServiceResult();

			try
			{
				bool isEditable = _preOrderRepository.PreOrderIsEditable(id);
				var preOrder = this._preOrderRepository.GetEntityToJoin(id)
										.Select(data => new
										{
											Id = data.ID,
											//Quantity = data.QUANTITY,
											Client = this._clientService.GetById(data.FK_CLIENT).Data,
											Items = this._preOrderProductService.GetByPreOrder(data.ID).Data,
											DateCreated = data.CREATED_AT,
											DateDelivery = data.DATE_DELIVERY,
											IsEditable = isEditable,
										});

				result.Data = preOrder;
				result.Message = "Obtenido con exito";
			}
			catch (Exception ex)
			{

				result.Success = false;
				result.Message = $"Error al remover el pedido: {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetPreOrdersByRecentDate()
		{
			ServiceResult result = new();
			try
			{
				var report = _preOrderRepository.GetPreOrdersByRecentDate();
				result.Data = _mapper.Map<List<PreOrder>>(report);
				result.Message = "Obtenidos con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error: {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetPreOrderInProgress(int id)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				var preOrder = _preOrderProductRepository.GetPreOrderWithOrders(id).Select(preOrderProducts => new
				{

					id = preOrderProducts.ID,
					//QuantityPreOrder
					Quantity = preOrderProducts.QUANTITY,
					ProductId = preOrderProducts.FK_PRODUCT,
					ProductName = _productRepository.GetEntity(preOrderProducts.FK_PRODUCT).NAME_PRODUCT,
					Size = _sizeRepository.GetEntity(preOrderProducts.FK_SIZE).SIZE,
					SizeId = preOrderProducts.FK_SIZE,
					colorPrimary = _colorRepository.GetEntity(preOrderProducts.COLOR_PRIMARY).COLORNAME,
					ColorPrimaryId = preOrderProducts.COLOR_PRIMARY,
					ColorSecondary = preOrderProducts.COLOR_SECONDARY,
				});
				result.Data = preOrder;
			}
			catch (Exception e)
			{
				result.Success = false;
				result.Data = e.Message;
			}
		
			return result;
		}

		public bool ChangeStatusPreOrder(int idPreOrder, bool status)
		{
			var change = _preOrderRepository.ChangeStatusPreOrder(idPreOrder, status);
			return change;
		}

		public ServiceResult Remove(PreOrderDtoRemove dtoRemove)
		{
			ServiceResult result = new ServiceResult();

			try
			{
				PreOrder preOrder = new PreOrder
				{
					ID = dtoRemove.Id,
					USER_MOD = dtoRemove.User
				};

				this._preOrderRepository.Remove(preOrder);
			}
			catch (Exception ex)
			{

				result.Success = false;
				result.Message = $"Error al remover el pedido: {ex.Message}";
			}
			return result;
		}

		public ServiceResult Update(PreOrderDtoUpdate dtoUpdate)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				PreOrder preOrder = new PreOrder
				{
					ID = dtoUpdate.Id,
					FK_CLIENT = dtoUpdate.FkClient,
					USER_MOD = dtoUpdate.User,
					COMPLETED = dtoUpdate.Completed
				};

				this._preOrderRepository.Update(preOrder);
				result.Message = "Actualizado con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al actualizar el pedido: {ex.Message}";
			}
			return result;
		}
	}
}
