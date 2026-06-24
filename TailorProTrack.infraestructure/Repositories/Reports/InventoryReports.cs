

using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.domain.Reports;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Interfaces.Reports;

namespace TailorProTrack.infraestructure.Repositories.Reports
{
    public class InventoryReports : IInventoryReports
    {
        private readonly TailorProTrackContext _ctx;
        public InventoryReports(TailorProTrackContext ctx)
        {
            _ctx = ctx;
        }

        public List<MissedInventory> GetMissedInventory()
        {
            var requestedProducts = _ctx.Set<PreOrderProducts>()
                .AsNoTracking()
                .Where(product => !product.REMOVED
                    && product.PreOrder != null
                    && !product.PreOrder.REMOVED
                    && product.PreOrder.COMPLETED == false)
                .GroupBy(product => new
                {
                    product.FK_PRODUCT,
                    product.FK_SIZE,
                    product.COLOR_PRIMARY,
                    product.COLOR_SECONDARY
                })
                .Select(group => new
                {
                    group.Key.FK_PRODUCT,
                    group.Key.FK_SIZE,
                    group.Key.COLOR_PRIMARY,
                    group.Key.COLOR_SECONDARY,
                    RequestedQuantity = group.Sum(product => product.QUANTITY)
                })
                .ToList();

            var availableInventory = _ctx.Set<InventoryColor>()
                .AsNoTracking()
                .Where(color => !color.REMOVED
                    && color.Inventory != null
                    && !color.Inventory.REMOVED)
                .GroupBy(color => new
                {
                    color.Inventory.FK_PRODUCT,
                    color.Inventory.FK_SIZE,
                    COLOR_PRIMARY = color.FK_COLOR_PRIMARY,
                    COLOR_SECONDARY = color.FK_COLOR_SECONDARY
                })
                .Select(group => new
                {
                    group.Key.FK_PRODUCT,
                    group.Key.FK_SIZE,
                    group.Key.COLOR_PRIMARY,
                    group.Key.COLOR_SECONDARY,
                    AvailableQuantity = group.Sum(color => color.QUANTITY)
                })
                .ToList();

            var availableByProductDetail = availableInventory
                .ToDictionary(
                    item => (item.FK_PRODUCT, item.FK_SIZE, item.COLOR_PRIMARY, item.COLOR_SECONDARY),
                    item => item.AvailableQuantity);

            var productNames = _ctx.Set<Product>()
                .AsNoTracking()
                .ToDictionary(product => product.ID, product => product.NAME_PRODUCT);

            var sizeNames = _ctx.Set<Size>()
                .AsNoTracking()
                .ToDictionary(size => size.ID, size => size.SIZE);

            var colorNames = _ctx.Set<Color>()
                .AsNoTracking()
                .ToDictionary(color => color.ID, color => color.COLORNAME);

            var missedInv = requestedProducts
                .Select(item =>
                {
                    availableByProductDetail.TryGetValue(
                        (item.FK_PRODUCT, item.FK_SIZE, item.COLOR_PRIMARY, item.COLOR_SECONDARY),
                        out int availableQuantity);

                    return new
                    {
                        item.FK_PRODUCT,
                        item.FK_SIZE,
                        item.COLOR_PRIMARY,
                        item.COLOR_SECONDARY,
                        MissingQuantity = item.RequestedQuantity - availableQuantity
                    };
                })
                .Where(item => item.MissingQuantity > 0)
                .Select(item => new MissedInventory
                {
                    NombreProducto = productNames.GetValueOrDefault(item.FK_PRODUCT),
                    Size = sizeNames.GetValueOrDefault(item.FK_SIZE),
                    ColorPrimary = colorNames.GetValueOrDefault(item.COLOR_PRIMARY),
                    ColorSecondary = item.COLOR_SECONDARY.HasValue
                        ? colorNames.GetValueOrDefault(item.COLOR_SECONDARY.Value)
                        : null,
                    CantidadFaltante = item.MissingQuantity
                })
                .OrderBy(item => item.NombreProducto)
                .ThenBy(item => item.Size)
                .ToList();

            return missedInv;
        }
    }
}
