
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

		public SaleService(ISalesRepository saleRepository,IMapper mapper) : base(mapper,saleRepository)
        {
            _repository = saleRepository;
			_mapper = mapper;
        }

        public override ServiceResult Add(SaleDtoAdd dtoAdd)
        {
	        dtoAdd.IsValid(_repository);
            return base.Add(dtoAdd);
		}   
    }
}
