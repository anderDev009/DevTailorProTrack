
using System.Net.Http.Headers;
using TailorProTrack.Application.Contracts.Report;
using TailorProTrack.Application.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Reports
{
    public class ReportOrderInventoryService : IReportOrderInventoryService
    {
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IInventoryColorRepository _inventoryColorRepository;

        public ReportOrderInventoryService(IOrderProductRepository orderProductRepository,
                                           IInventoryColorRepository inventoryColorRepository)
        {
            _orderProductRepository = orderProductRepository;
            _inventoryColorRepository = inventoryColorRepository;
        }
        public ServiceResult GetDiffItemsByOrderId(int orderId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var report = this._orderProductRepository.GetEntities().Where(d => d.FK_ORDER == orderId)
                                                         .Join
                                                         (
                                                            this._inventoryColorRepository.GetEntities().Select(d => new { d.ID, d.FK_INVENTORY, d.FK_COLOR_PRIMARY, d.FK_COLOR_SECONDARY, d.QUANTITY }),
                                                            orderP => orderP.FK_INVENTORYCOLOR,
                                                            inventoryC => inventoryC.ID,
                                                            (orderP, inventoryC) => new { orderP, inventoryC }
                                                         )
                                                         .GroupBy(d => d.orderP.ID)
                                                         .Select(group => new
                                                         {
                                                             IdOrderP = group.Key,
                                                             QuantityInOrder = group.Select(d => d.orderP.QUANTITY).First(),
                                                             QuantityInInventory = group.Select(d => d.inventoryC.QUANTITY).First(),
                                                             Need = group.Sum(d => d.orderP.QUANTITY - d.inventoryC.QUANTITY) > 0 ? 0 : group.Sum(d => d.orderP.QUANTITY - d.inventoryC.QUANTITY)
                                                         });
                var response = new
                {
                    IdOrder = orderId,
                    report
                };
                result.Data = response;
                result.Message = "Reporte obtenido con exito.";
            }       
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el reporte: {ex.Message}.";
            }
            return result;
        }
    }
}
