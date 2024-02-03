

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
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
        public PaymentService(IPaymentRepository repository, ILogger<IPaymentRepository> logger,
                              IPaymentTypeRepository typeRepository, IOrderService orderService,
                              IConfiguration configuration, IOrderRepository orderRepository,
                              IBankAccountRepository bankAccRepository, IBankRepository bankRepository)
        {
            _repository = repository;
            this.logger = logger;
            _typeRepository = typeRepository;
            _orderService = orderService;
            Configuration = configuration;
            _orderRepository = orderRepository;
            _bankAccRepository = bankAccRepository;
            _bankRepository = bankRepository;
        }

        private IConfiguration Configuration { get; }
        public ServiceResult Add(PaymentDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Validaciones
                dtoAdd.IsValid(this.Configuration, this._typeRepository, this._orderRepository);
                //construccion obj Payment
                Payment payment = new Payment
                {
                    AMOUNT = dtoAdd.Amount,
                    FK_ORDER = dtoAdd.FkOrder,
                    FK_TYPE_PAYMENT = dtoAdd.FkTypePayment,
                    FK_BANK_ACCOUNT = dtoAdd.FkBankAccount,
                    USER_CREATED = dtoAdd.User
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
                int registerCount = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Where(d => !d.REMOVED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var payments = this._repository.GetEntities().Where(d => !d.REMOVED).Select(d => new { d.ID, d.FK_ORDER, d.FK_TYPE_PAYMENT, d.AMOUNT })
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
                                                                               Amount = d.Sum(d => d.payment.AMOUNT),
                                                                               PaymentNumbers = d.Select(d => d.type.ID).Count()
                                                                           }).ToList();
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
                var payments = this._repository.GetEntities().Where(d => !d.REMOVED && d.ID == id).Select(d => new { d.ID, d.FK_ORDER, d.FK_TYPE_PAYMENT, d.AMOUNT })
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
                                                                               Amount = d.Sum(d => d.payment.AMOUNT),
                                                                               PaymentNumbers = d.Select(d => d.type.ID).Count()
                                                                           });
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
                                                            .Join
                                                             (this._bankAccRepository.GetEntities()
                                                                                     .Join(this._bankRepository.GetEntities(),
                                                                                           bankAcc => bankAcc.FK_BANK,
                                                                                           bank => bank.ID,
                                                                                           (bankAcc, bank) => new {bankAcc,bank}
                                                                                     ).Select(data => new {data.bankAcc.ID,data.bankAcc.BANK_ACCOUNT,data.bank.NAME}),
                                                            group => group.payment.FK_BANK_ACCOUNT,
                                                            bankAcc => bankAcc.ID,
                                                            (group, bankAcc) => new {group.payment,group.type, bankAcc}
                                                            )
                                               .Select(d => new
                                               {
                                                   Id = d.payment.ID,
                                                   Amount = d.payment.AMOUNT,
                                                   Type = d.type.TYPE_PAYMENT,
                                                   Bank = d.bankAcc.NAME,
                                                   Account = d.bankAcc.BANK_ACCOUNT 
                                               });
                if (payments.IsNullOrEmpty()) throw new Exception("No se encontraron registros");
                var orderPayments = new
                {
                    payments,
                    order = this._orderService.GetAmountTotalById(orderId).Data
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
                dtoUpdate.IsValid(this.Configuration, this._typeRepository, this._orderRepository);
                //creacion de obj
                Payment payment = new Payment
                {
                    ID = dtoUpdate.Id,
                    USER_MOD = dtoUpdate.User,
                    AMOUNT = dtoUpdate.Amount,
                    FK_ORDER = dtoUpdate.FkOrder,
                    FK_TYPE_PAYMENT = dtoUpdate.FkTypePayment,
                    FK_BANK_ACCOUNT = dtoUpdate.FkBankAccount,
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
