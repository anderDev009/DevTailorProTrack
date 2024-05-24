
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PreOrderProductRepository : BaseRepository<PreOrderProducts>, IPreOrderProductsRepository
    {
        private readonly TailorProTrackContext _ctx;
        private readonly IProductRepository _productRepository;
        public PreOrderProductRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public decimal GetAmountByIdPreOrder(int preOrderId)
        {
            decimal totalAmount = 0;
            var preOrder = _ctx.Set<PreOrderProducts>().Where(x => x.FK_PREORDER == preOrderId)
                .Include(x => x.Product)
                .ToList();
            foreach (var product in preOrder)
            {
                if (product.CUSTOM_PRICE != null || product.CUSTOM_PRICE != 0)
                {
                    totalAmount += product.QUANTITY * (decimal)product.CUSTOM_PRICE;
                }
                else
                {
                    totalAmount += product.QUANTITY * product.Product.SALE_PRICE;
                }
            }
            return totalAmount;
            //return _entities
            //         .Join(_ctx.PRODUCT,
            //         preOrderProducts => preOrderProducts.FK_PRODUCT,
            //         product => product.ID,
            //         (preorderProducts, product) => new { preorderProducts, product })
            //         .Where(data => data.preorderProducts.FK_PREORDER == preOrderId)
            //         .Sum(data => (data.product.SALE_PRICE * data.preorderProducts.QUANTITY));
        }

        public List<PreOrderProducts> GetByPreOrderId(int PreOrderId)
        {
            return this._entities.Where(data => data.FK_PREORDER == PreOrderId).ToList();
        }



        public void SaveMany(List<PreOrderProducts> preOrderProducts)
        {
            _entities.AddRange(preOrderProducts);
            this._ctx.SaveChanges();
        }

        public override void Update(PreOrderProducts entity)
        {
            PreOrderProducts products = this.GetEntity(entity.ID);

            products.QUANTITY = entity.QUANTITY;
            products.COLOR_PRIMARY = entity.COLOR_PRIMARY;
            products.COLOR_SECONDARY = entity.COLOR_SECONDARY;
            products.FK_PRODUCT = entity.FK_PRODUCT;
            products.FK_SIZE = entity.FK_SIZE;
            products.FK_PREORDER = entity.FK_PREORDER;

            this._ctx.Update(products);
            this._ctx.SaveChanges();
        }
        public List<PreOrderProducts> GetMissingColorsByIdPreOrder(int IdPreOrder)
        {
            var preOrder = _ctx.Set<PreOrderProducts>().Where(p => p.FK_PREORDER == IdPreOrder).ToList();
            List<PreOrderProducts> itemsMissing = new List<PreOrderProducts>();
            foreach (var item in preOrder)
            {
                //obteniendo el inventario
                var inventory = _ctx.Set<Inventory>().Where(i => i.FK_PRODUCT == item.FK_PRODUCT).First();
                //obteniendo el detalle del inventario
                var invColor = _ctx.Set<InventoryColor>()
                    .Where(i => i.FK_INVENTORY == inventory.ID && i.FK_COLOR_PRIMARY == item.COLOR_PRIMARY
                            && i.FK_COLOR_SECONDARY == item.COLOR_SECONDARY).First();
                //en caso de que sea null se agrega e indica la cantidad faltante
                if (invColor == null)
                {
                    itemsMissing.Add(item);
                    break;
                }
                int quantity = invColor.QUANTITY - item.QUANTITY;
                if (quantity < 0)
                {
                    //en caso de que la cantidad sea negativa se convierte en positivo indicando 
                    //que esa es la cantidad faltante.
                    item.QUANTITY = Math.Abs(quantity);
                    itemsMissing.Add(item);
                }
            }
            return itemsMissing;
        }

        public List<PreOrderProducts> GetMissingProducts()
        {
            List<PreOrderProducts> products = _ctx.Set<PreOrderProducts>()
                                                  .Include(x => x.Size)
                                                  .Include(x => x.PreOrder)
                                                  .Include(x => x.Product)
                                                  .Include(x => x.ColorPrimary)
                                                  .Include(x => x.ColorSecondary)
                                                  //.Where(x => )
                                                  .ToList();

            ; List<PreOrderProducts> productsCleaned = new();
            foreach (var product in products)
            {

                //asegurando de que no se haya agregado a la lista
                var temp = productsCleaned.Where(x => x.FK_PRODUCT == product.FK_PRODUCT
                                                && x.FK_SIZE == product.FK_SIZE && x.COLOR_PRIMARY == product.COLOR_PRIMARY
                                                && x.COLOR_SECONDARY == product.COLOR_SECONDARY).FirstOrDefault
                                                ();
                if (temp == null)
                {
                    temp = new();

                }
                else
                {
                    productsCleaned.Remove(temp);
                }


                var inventory = _ctx.Set<Inventory>()
                    .Include(x => x.InventoryColor)
                    .Where(x => x.FK_PRODUCT == product.FK_PRODUCT
                && x.FK_SIZE == product.FK_SIZE
                && x.InventoryColor.Any(x => x.FK_COLOR_PRIMARY == product.COLOR_PRIMARY
                                    && x.FK_COLOR_SECONDARY == product.COLOR_SECONDARY)
                ).FirstOrDefault();

                //obteniendo el color para saber si esta disponible
                if (inventory != null)
                {
                    int quantity = inventory.InventoryColor.Where(x => x.FK_COLOR_PRIMARY == product.COLOR_PRIMARY
                                    && x.FK_COLOR_SECONDARY == product.COLOR_SECONDARY).First().QUANTITY;
                    temp.QUANTITY += quantity - product.QUANTITY;

                }
                else
                {
                    temp.QUANTITY -= product.QUANTITY;
                }
                //asgignando los atributos 
                temp.ID = product.ID;
                temp.FK_PRODUCT = product.FK_PRODUCT;
                temp.Product = product.Product;
                temp.FK_SIZE = product.FK_SIZE;
                temp.Size = product.Size;
                temp.ColorPrimary = product.ColorPrimary;
                temp.ColorSecondary = product.ColorSecondary;
                temp.COLOR_PRIMARY = product.COLOR_PRIMARY;
                temp.COLOR_SECONDARY = product.COLOR_SECONDARY;
                productsCleaned.Add(temp);
            }
            return productsCleaned.Where(x => x.QUANTITY < 0).ToList();
        }
    }
}
