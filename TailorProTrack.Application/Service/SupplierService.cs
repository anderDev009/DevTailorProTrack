

using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Suppliers;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class SupplierService : GenericService<SuppliersDtoAdd,SuppliersDtoUpdate,SuppliersDtoGet,Supplier>, ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(IMapper mapper, ISupplierRepository supplierRepository) : base(mapper, supplierRepository)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }

        public override ServiceResult Add(SuppliersDtoAdd dtoAdd)
        {
            //comprobando si hay algun suplidor con el RNC indicado.
            if(dtoAdd.Rnc != null)
            {
                if (_supplierRepository.ExistSupplierByRnc(dtoAdd.Rnc))
                {
                    return new ServiceResult
                    {
                        Data = null,
                        Message = "Ya ay un suplidor con el RNC indicado",
                        Success = false
                    };
                }
            }
          
            return base.Add(dtoAdd);
        }
    }
}
