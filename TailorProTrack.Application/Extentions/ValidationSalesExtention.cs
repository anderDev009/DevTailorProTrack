

using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationSalesExtention
    {
        public static void IsValid(this SaleDtoAdd dtoBase, ISalesRepository salesRepository)
        {
            if (salesRepository.Exists(sale => sale.FK_PREORDER == dtoBase.FkOrder))
            {
                throw new SaleServiceException("Factura registrada");
            }
        }
    }
}
