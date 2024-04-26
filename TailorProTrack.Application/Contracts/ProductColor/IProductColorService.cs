
using AutoMapper;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.ProductColor;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Contracts.ProductColor
{
    public interface IProductColorService : IBaseServiceGeneric<ProductColorDtoAdd,
                                                                ProductColorDtoUpdate,
                                                                ProductColorDtoGet,
                                                                domain.Entities.ProductColor>
    {
    }
}
