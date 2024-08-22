
using AutoMapper;
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Extentions;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class SaleService : GenericService<SaleDtoAdd,SaleDtoUpdate,SaleDtoGet,Sales>, ISaleService
    {
        private readonly ISalesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPreOrderProductService _preOrderProducts;
		private readonly IPreOrderRepository _preOrderRepository;

		public SaleService(ISalesRepository saleRepository,IMapper mapper, IPreOrderProductService preOrderProduct,IPreOrderRepository preOrderRepository) : base(mapper,saleRepository)
        {
            _repository = saleRepository;
			_mapper = mapper;
			_preOrderProducts = preOrderProduct;
			_preOrderRepository = preOrderRepository;
        }

        public override ServiceResult Add(SaleDtoAdd dtoAdd)
        {
	        var preOrder = _preOrderRepository.GetEntity(dtoAdd.FkOrder);

			dtoAdd.IsValid(_repository);
	        if (dtoAdd.B14 != null)
	        {
				preOrder.ITBIS = false;
			}
	        else
	        {
		        preOrder.ITBIS = false;
	        }
	        _preOrderRepository.Update(preOrder);

			return base.Add(dtoAdd);
		}

        public override ServiceResult GetById(int id)
        {
			ServiceResult result = new();
			try
			{
				Sales entity = _repository.GetEntity(id);
				var saleMapped = _mapper.Map<SaleDtoGet>(entity);
				saleMapped.AmountBase = _preOrderProducts.GetAmountByIdPreOrder(saleMapped.PreOrder.ID);
				result.Data = saleMapped;
				result.Message = "Obtenido con exito";
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = $"Error al intentar obtener la data: {ex.Message}";
			}

			return result;

        }
    }
}
