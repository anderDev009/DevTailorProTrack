

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.Payment;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
	public class PaymentService : IPaymentService
	{
		private readonly IPaymentRepository _repository;
		private readonly ILogger logger;
		//repositorio de tipo de pago
		private readonly IPaymentTypeRepository _typeRepository;
		//repositorio de ordenes
		private readonly IOrderRepository _orderRepository;
		//servicio de ordenes
		private readonly IOrderService _orderService;
		//repositorio de bancos
		private readonly IBankRepository _bankRepository;
		//repositorio cuenta de bancos
		private readonly IBankAccountRepository _bankAccRepository;
		//repositorio de clientes
		private readonly IClientRepository _clientRepository;
		private readonly IPreOrderRepository _preOrderRepository;

		private IMapper _mapper;
		//repositorio de pre order
		private readonly IPreOrderProductsRepository _preOrderProdcutRepository;
		public PaymentService(IPaymentRepository repository, ILogger<IPaymentRepository> logger,
							  IPaymentTypeRepository typeRepository, IOrderService orderService,
							  IConfiguration configuration, IOrderRepository orderRepository,
							  IClientRepository clientRepository, IPreOrderRepository preOrderRepository,
							  IBankAccountRepository bankAccRepository, IBankRepository bankRepository, IPreOrderProductsRepository preOrderProductsRepository, IMapper mapper)
		{
			_repository = repository;
			this.logger = logger;
			_typeRepository = typeRepository;
			_orderService = orderService;
			Configuration = configuration;
			_orderRepository = orderRepository;
			_bankAccRepository = bankAccRepository;
			_bankRepository = bankRepository;
			_preOrderProdcutRepository = preOrderProductsRepository;
			_mapper = mapper;
			_clientRepository = clientRepository;
			_preOrderRepository = preOrderRepository;
		}

		private IConfiguration Configuration { get; }
		public ServiceResult Add(PaymentDtoAdd dtoAdd)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				//Validaciones
				dtoAdd.IsValid(this.Configuration, this._typeRepository, _preOrderRepository);
				//construccion obj Payment
				Payment payment = new Payment
				{
					AMOUNT = dtoAdd.Amount,
					FK_ORDER = dtoAdd.FkOrder,
					FK_TYPE_PAYMENT = dtoAdd.FkTypePayment,
					FK_BANK_ACCOUNT = dtoAdd.FkBankAccount,
					USER_CREATED = dtoAdd.User,
					ACCOUNT_PAYMENT = dtoAdd.AccountPayment,
					ACCOUNT_NUMBER = dtoAdd.DocumentNumber
				};

				this._repository.Save(payment);
				result.Message = "Agregado con exito.";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al agregar el pago {ex.Message}";
			}
			return result;
		}

		public ServiceResultWithHeader GetAll(PaginationParams @params)
		{
			ServiceResultWithHeader result = new ServiceResultWithHeader();

			try
			{
				int registerCount = this._repository.GetEntities().Where(d => !d.REMOVED).Count();
				PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

				var payments = this._repository.GetEntities().Where(d => !d.REMOVED).Select(d => new { d.ID, d.FK_ORDER, d.FK_TYPE_PAYMENT, d.AMOUNT, d.ACCOUNT_PAYMENT })
																		   .Join
																		   (
																			this._typeRepository.GetEntities().Select(d => new { d.ID, d.TYPE_PAYMENT }),
																			payment => payment.FK_TYPE_PAYMENT,
																			type => type.ID,
																			(payment, type) => new { payment, type }
																		   )
																		   .GroupBy(data => data.payment.FK_ORDER)
																		   .Select(d => new PaymentDtoGet
																		   {
																			   IdOrder = d.Key,
																			   //Client = d
																			   AccountPayment = d
																			   .Select(x => x.payment.ACCOUNT_PAYMENT).First(),
																			   Amount = d.Sum(d => d.payment.AMOUNT),
																			   PaymentNumbers = d.Select(d => d.type.ID).Count()
																		   }).Skip((@params.Page - 1) * @params.ItemsPerPage)
																		   .Take(@params.ItemsPerPage).ToList();
				result.Data = payments;
				result.Header = header;
				result.Message = "Obtenidos con exito.";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al obtener los pagos: {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetById(int id)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				var payments = this._repository.GetEntities().Where(d => !d.REMOVED && d.ID == id).Select(d => new { d.ID, d.FK_ORDER, d.FK_TYPE_PAYMENT, d.AMOUNT, d.ACCOUNT_PAYMENT })
																		   .Join
																		   (
																			this._typeRepository.GetEntities().Select(d => new { d.ID, d.TYPE_PAYMENT }),
																			payment => payment.FK_TYPE_PAYMENT,
																			type => type.ID,
																			(payment, type) => new { payment, type }
																		   )
																		   .GroupBy(data => data.payment.ID)
																		   .Select(d => new PaymentDtoGet
																		   {
																			   IdOrder = d.Key,
																			   AccountPayment = d.Select(x => x.payment.ACCOUNT_PAYMENT).First(),
																			   Amount = d.Select(data => data.payment.AMOUNT).First(),
																		   }); ;
				if (payments.IsNullOrEmpty()) throw new Exception("No se encontraron registros");
				result.Data = payments;
				result.Message = "Obtenido con exito.";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al obtener el pago {ex.Message}";
			}
			return result;
		}

		public ServiceResult GetPaymentsByOrderId(int orderId)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				var payments = this._repository.GetEntities().Where(d => !d.REMOVED && d.FK_ORDER == orderId)
															 .Join
																		   (
																			 this._typeRepository.GetEntities().Select(d => new { d.ID, d.TYPE_PAYMENT }),
																			payment => payment.FK_TYPE_PAYMENT,
																			type => type.ID,
																			(payment, type) => new { payment, type }
																		   )
											   .Select(d => new
											   {
												   Id = d.payment.ID,
												   Amount = d.payment.AMOUNT,
												   Type = d.type.TYPE_PAYMENT,
												   Bank = d.payment.FK_BANK_ACCOUNT != null ? _bankRepository.GetEntity(_bankAccRepository.GetEntity((int)d.payment.FK_BANK_ACCOUNT).FK_BANK).NAME : "NA",
												   Account = d.payment.FK_BANK_ACCOUNT != null ? _bankAccRepository.GetEntity((int)d.payment.FK_BANK_ACCOUNT).BANK_ACCOUNT : "Caja",
												   DocumentNumber = d.payment.ACCOUNT_NUMBER,
												   Client = _mapper.Map<ClientDtoGet>(_clientRepository.GetEntity(_preOrderRepository.GetEntity(d.payment.FK_ORDER).FK_CLIENT))
											   }).ToList();
				if (payments.IsNullOrEmpty()) throw new Exception("No se encontraron registros");
				decimal amountPending = _repository.GetAmountPendingByIdPreOrder(orderId);
				var orderPayments = new
				{
					payments,
					AmountPending = amountPending > 0 ? amountPending : 0

				};

				result.Data = orderPayments;
				result.Message = "Obtenidos con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al obtener el registro: {ex.Message}";
			}

			return result;
		}

		public decimal GetAmountByIdOrder(int orderId)
		{
			return _repository.GetAmountPendingByIdPreOrder(orderId);
		}

		public ServiceResult AddPaymentUsingNoteCredits(PaymentDtoAddWithNoteCredit dtoAdd)
		{
			ServiceResult result = new();
			try
			{
				//Validaciones
				dtoAdd.IsValid(this.Configuration, this._typeRepository, _preOrderRepository);
				//construccion obj Payment
				Payment payment = new Payment
				{
					AMOUNT = dtoAdd.Amount,
					FK_ORDER = dtoAdd.FkOrder,
					FK_TYPE_PAYMENT = dtoAdd.FkTypePayment,
					FK_BANK_ACCOUNT = dtoAdd.FkBankAccount,
					USER_CREATED = dtoAdd.User,
					ACCOUNT_PAYMENT = dtoAdd.AccountPayment,
					ACCOUNT_NUMBER = dtoAdd.DocumentNumber
				};

				this._repository.SaveWithNoteCredit(payment);
				result.Message = "Agregado con exito.";
			}
			catch (Exception e)
			{
				result.Success = false;
				result.Message = $"Error: {e.Message}";
			}
			return result;
		}

		public ServiceResult Remove(PaymentDtoRemove dtoRemove)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				Payment payment = new Payment
				{
					ID = dtoRemove.Id,
					USER_MOD = dtoRemove.User
				};
				this._repository.Remove(payment);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al remover el pago {ex.Message}";
			}
			return result;
		}

		public ServiceResult Update(PaymentDtoUpdate dtoUpdate)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				//validaciones 
				dtoUpdate.IsValid(this.Configuration, this._typeRepository, _preOrderRepository);
				//creacion de obj
				Payment payment = new Payment
				{
					ID = dtoUpdate.Id,
					USER_MOD = dtoUpdate.User,
					AMOUNT = dtoUpdate.Amount,
					ACCOUNT_PAYMENT = dtoUpdate.AccountPayment,
					FK_ORDER = dtoUpdate.FkOrder,
					FK_TYPE_PAYMENT = dtoUpdate.FkTypePayment,
					FK_BANK_ACCOUNT = dtoUpdate.FkBankAccount,
					ACCOUNT_NUMBER = dtoUpdate.DocumentNumber
				};

				this._repository.Update(payment);
				result.Message = "Actualizado con exito.";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al agregar el pago {ex.Message}";
			}
			return result;
		}
	}
}
