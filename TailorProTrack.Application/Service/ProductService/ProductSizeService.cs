

using AutoMapper;
using TailorProTrack.Application.Contracts.ProductSize;
using TailorProTrack.Application.Dtos.ProductSize;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.ProductSizeService
{
    public class ProductSizeService : GenericService<ProductSizeDtoAdd, ProductSizeDtoUpdate,
                                                    ProductSizeDtoGet, domain.Entities.ProductSize>, IProductSizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IMapper _mapper;
        public ProductSizeService(IProductSizeRepository repository, IMapper mapper): base(mapper,repository)
        {
            _mapper = mapper; 
            _productSizeRepository = repository;
        }
    }
}
