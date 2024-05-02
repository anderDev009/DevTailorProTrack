using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.ProductSize;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts.ProductSize
{
    public interface IProductSizeService : IBaseServiceGeneric<ProductSizeDtoAdd,ProductSizeDtoUpdate,
                                                                 ProductSizeDtoGet,domain.Entities.ProductSize>
    {
    }
}
