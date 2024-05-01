
using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.ProductColor;
using TailorProTrack.Application.Dtos.ProductColor;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class ProductColorService : IGenericService<ProductColorDtoAdd,
                                                        ProductColorDtoUpdate,
                                                        ProductColorDtoGet,
                                                        ProductColor>, IProductColorService
    {
        private readonly IMapper _mapper;
        private readonly IProductColorRepository _productColorRepository;
        public ProductColorService(IMapper mapper,IProductColorRepository productColorRepository) : base(mapper,productColorRepository) 
        {
            _mapper = mapper;
            _productColorRepository = productColorRepository;   
        }   
    }
}
