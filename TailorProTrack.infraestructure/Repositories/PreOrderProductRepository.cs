
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
            return _entities
                     .Join(_ctx.PRODUCT,
                     preOrderProducts => preOrderProducts.FK_PRODUCT,
                     product => product.ID,
                     (preorderProducts, product) => new { preorderProducts, product })
                     .Where(data => data.preorderProducts.FK_PREORDER == preOrderId)
                     .Sum(data => (data.product.SALE_PRICE * data.preorderProducts.QUANTITY));
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
    }
}
