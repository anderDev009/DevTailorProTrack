
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Report;
using TailorProTrack.Application.Core;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Reports
{
    public class ReportOrderInventoryService : IReportOrderInventoryService
    {
        private readonly IInventoryColorRepository _inventoryColorRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPreOrderRepository _preOrderRepository;
        private readonly IPreOrderProductsRepository _preOrderProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISizeRepository _sizeRepository;

        public ReportOrderInventoryService(IInventoryRepository inventoryRepository,
                                           IPreOrderRepository preOrderRepository,
                                           IPreOrderProductsRepository preOrderProductsRepository,
                                           IInventoryColorRepository inventoryColorRepository,
                                           IProductRepository productRepository,
                                           ISizeRepository sizeRepository)
        {
            _inventoryColorRepository = inventoryColorRepository;
            _inventoryRepository = inventoryRepository;
            _preOrderRepository = preOrderRepository;
            _preOrderProductRepository = preOrderProductsRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
        }

        public ServiceResult GetDiffItems()
        {
            ServiceResult result = new ServiceResult();
            try
            {

                var report = _productRepository.SearchEntities()
                    .Join(_inventoryRepository.SearchEntities()
                    , product => product.ID, inventory => inventory.FK_PRODUCT,
                    (product, inventory) => new { product, inventory }
                    )
                    .Join(_preOrderProductRepository.SearchEntities(), group => group.product.ID
                    , preorderProducts => preorderProducts.FK_PRODUCT,
                    (group, preorderProducts) => new { group.inventory, group.product, preorderProducts })
                    .Join(_inventoryColorRepository.SearchEntities(),
                    group => group.inventory.ID,
                    invColor => invColor.FK_INVENTORY,
                    (group, invColor) => new { group.product, group.preorderProducts, group.inventory, invColor })
                    .GroupBy(x => x.preorderProducts.ID)
                    .Select(data => new
                    {
                        idProd = data.Key,
                        productName = data.Select(d => d.product.NAME_PRODUCT).First(),
                        sizes = data.Select(d => d.preorderProducts.FK_SIZE)
                        .Join(_sizeRepository.SearchEntities(), fk => fk, size => size.ID, (fk, size) => new { fk, size }).Select(d => d.size).Distinct().ToList(),
                        availableSizes = data.Select(d => d.inventory.FK_SIZE).Distinct().ToList(),
                        sizesNeeded = data.Select(d => d.preorderProducts.FK_SIZE).Distinct().ToList(),
                        colorPrimaryNeeded = data.Select(d => d.preorderProducts.COLOR_PRIMARY).Distinct().ToList(),
                        colorSecondaryNeeded = data.Select(d => d.preorderProducts.COLOR_SECONDARY).Distinct().ToList(),
                        colorPrimaryAvailable = data.Select(d => d.invColor.FK_COLOR_PRIMARY).Distinct().ToList(),
                        colorSecondaryAvailable = data.Select(d => d.invColor.FK_COLOR_SECONDARY).Distinct().ToList(),
                        quantityDiff = data.Select(d => d.invColor.QUANTITY - d.preorderProducts.QUANTITY).First()
                    })
                    .Select(data => new
                    {
                        data.idProd,
                        data.productName,
                        missingSizes = data.sizesNeeded.Except(data.availableSizes).ToList(),
                        missingColors = new
                        {
                            primary = data.colorPrimaryNeeded.Except(data.colorPrimaryAvailable).ToList(),
                            secondary = data.colorSecondaryNeeded.Except(data.colorSecondaryAvailable).ToList()
                        },
                        completed = data.quantityDiff >= 0,
                        itemsToComplete = data.quantityDiff,
                        data.sizes
                    });
                //var report = _preOrderProductRepository.SearchEntities()
                //    .Join(
                //    _productRepository.SearchEntities(),
                //    preorderProdcut => preorderProdcut.FK_PRODUCT,
                //    product => product.ID,
                //    (preorderProduct, product) => new { preorderProduct, product }
                //    )
                //    .Select(data => new
                //    {
                //        idProd = data.product.ID,
                //        product = data.product.NAME_PRODUCT,
                //        sizeNeeded = _preOrderProductRepository.SearchEntities()
                //                       .Where(pr => pr.FK_PRODUCT == data.preorderProduct.FK_PRODUCT && pr.FK_SIZE != )
                //    }).ToList();
                //.Join(
                //_inventoryRepository.SearchEntities(),
                //preorderProducts => preorderProducts.FK_PRODUCT,
                //inventory => inventory.ID,
                //(preorderProducts, inventory) => new {preorderProducts,inventory})
                //.Join(_inventoryColorRepository.SearchEntities(),
                //group => group.inventory.ID,
                //invColor => invColor.FK_INVENTORY,
                //(group,invColor) => new{ group.preorderProducts,group.inventory, invColor })
                //var report = _preOrderRepository.SearchEntities().Join(
                //    _preOrderProductRepository.SearchEntities(),
                //    preorder=>preorder.ID,
                //    preorderProducts => preorderProducts.FK_PREORDER,
                //    (preorder,preorderProducts)=> new {preorder,preorderProducts})
                //    .Join(
                //    _inventoryRepository.SearchEntities(),
                //    group => group.preorderProducts.FK_PRODUCT, 
                //    inventory => inventory.ID,
                //    (group, inventory) => new {group.preorderProducts,group.preorder,inventory})
                //    .Join
                //    (_inventoryColorRepository.SearchEntities(),
                //    group => group.inventory.ID,
                //    invColor => invColor.FK_INVENTORY,
                //    (group,invColor) => new {group.inventory,group.preorderProducts,group.preorder,invColor})
                //    .Where()


                //result.Data = response;
                result.Data = report;
                result.Message = "Reporte obtenido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el reporte: {ex.Message}.";
            }
            return result;
        }
        public ServiceResult GetDiffItemsByPreOrderId(int preOrderId)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                var report = _productRepository.SearchEntities()
                    .Join(_inventoryRepository.SearchEntities()
                    , product => product.ID, inventory => inventory.FK_PRODUCT,
                    (product, inventory) => new { product, inventory }
                    )
                    .Join(_preOrderProductRepository.SearchEntities(), group => group.product.ID
                    , preorderProducts => preorderProducts.FK_PRODUCT,
                    (group, preorderProducts) => new { group.inventory, group.product, preorderProducts })
                    .Join(_inventoryColorRepository.SearchEntities(),
                    group => group.inventory.ID,
                    invColor => invColor.FK_INVENTORY,
                    (group, invColor) => new { group.product, group.preorderProducts, group.inventory, invColor })
                    .Where(data => data.preorderProducts.FK_PREORDER == preOrderId)
                    .GroupBy(x => x.preorderProducts.ID)
                    .Select(data => new
                    {
                        idProd = data.Key,
                        productName = data.Select(d => d.product.NAME_PRODUCT).First(),
                        sizes = data.Select(d => d.preorderProducts.FK_SIZE)
                        .Join(_sizeRepository.SearchEntities(), fk => fk, size => size.ID,(fk,size) => new {fk, size }).Select(d => d.size).Distinct().ToList(),
                        availableSizes = data.Select(d => d.inventory.FK_SIZE).Distinct().ToList(),
                        sizesNeeded = data.Select(d => d.preorderProducts.FK_SIZE).Distinct().ToList(),
                        colorPrimaryNeeded = data.Select(d => d.preorderProducts.COLOR_PRIMARY).Distinct().ToList(),
                        colorSecondaryNeeded = data.Select(d => d.preorderProducts.COLOR_SECONDARY).Distinct().ToList(),
                        colorPrimaryAvailable = data.Select(d => d.invColor.FK_COLOR_PRIMARY).Distinct().ToList(),
                        colorSecondaryAvailable = data.Select(d => d.invColor.FK_COLOR_SECONDARY).Distinct().ToList(),
                        quantityDiff = data.Select(d => d.invColor.QUANTITY - d.preorderProducts.QUANTITY).First()
                    })
                    .Select(data => new
                    {
                        data.idProd,
                        data.productName,
                        missingSizes = data.sizesNeeded.Except(data.availableSizes).ToList(),
                        missingColors = new { primary = data.colorPrimaryNeeded.Except(data.colorPrimaryAvailable).ToList(),
                            secondary = data.colorSecondaryNeeded.Except(data.colorSecondaryAvailable).ToList()
                        },
                        completed =data.quantityDiff >= 0 ,
                        itemsToComplete = data.quantityDiff,
                        data.sizes
                    });
                //var report = _preOrderProductRepository.SearchEntities()
                //    .Join(
                //    _productRepository.SearchEntities(),
                //    preorderProdcut => preorderProdcut.FK_PRODUCT,
                //    product => product.ID,
                //    (preorderProduct, product) => new { preorderProduct, product }
                //    )
                //    .Select(data => new
                //    {
                //        idProd = data.product.ID,
                //        product = data.product.NAME_PRODUCT,
                //        sizeNeeded = _preOrderProductRepository.SearchEntities()
                //                       .Where(pr => pr.FK_PRODUCT == data.preorderProduct.FK_PRODUCT && pr.FK_SIZE != )
                //    }).ToList();
                //.Join(
                //_inventoryRepository.SearchEntities(),
                //preorderProducts => preorderProducts.FK_PRODUCT,
                //inventory => inventory.ID,
                //(preorderProducts, inventory) => new {preorderProducts,inventory})
                //.Join(_inventoryColorRepository.SearchEntities(),
                //group => group.inventory.ID,
                //invColor => invColor.FK_INVENTORY,
                //(group,invColor) => new{ group.preorderProducts,group.inventory, invColor })
                //var report = _preOrderRepository.SearchEntities().Join(
                //    _preOrderProductRepository.SearchEntities(),
                //    preorder=>preorder.ID,
                //    preorderProducts => preorderProducts.FK_PREORDER,
                //    (preorder,preorderProducts)=> new {preorder,preorderProducts})
                //    .Join(
                //    _inventoryRepository.SearchEntities(),
                //    group => group.preorderProducts.FK_PRODUCT, 
                //    inventory => inventory.ID,
                //    (group, inventory) => new {group.preorderProducts,group.preorder,inventory})
                //    .Join
                //    (_inventoryColorRepository.SearchEntities(),
                //    group => group.inventory.ID,
                //    invColor => invColor.FK_INVENTORY,
                //    (group,invColor) => new {group.inventory,group.preorderProducts,group.preorder,invColor})
                //    .Where()


                //result.Data = response;
                result.Data = report;
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
