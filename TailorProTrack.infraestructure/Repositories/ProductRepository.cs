
using System.Linq.Expressions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly TailorProTrackContext _context;
        public ProductRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            this._context = ctx;
        }


        public override void Remove(Product entity)
        {
            entity.REMOVED = false;
            entity.USER_MOD = entity.USER_MOD;
            this._context.Update(entity);
            this._context.SaveChanges();
        }
        public override void Update(Product entity)
        {
            var productToUpdate = base.GetEntity(entity.ID);
            productToUpdate.SALE_PRICE = entity.SALE_PRICE;
            productToUpdate.NAME_PRODUCT = entity.NAME_PRODUCT;
            productToUpdate.FK_TYPE = entity.FK_TYPE;
            productToUpdate.DESCRIPTION_PRODUCT = entity.DESCRIPTION_PRODUCT;
            productToUpdate.USER_MOD = entity.USER_MOD;
            productToUpdate.MODIFIED_AT = DateTime.Now;
            this._context.Update(productToUpdate);
            this._context.SaveChanges();
        }
        public override int Save(Product entity)
        {
            entity.CREATED_AT = DateTime.Now; 
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }

        public override bool Exists(Expression<Func<Product, bool>> filter)
        {
            return base.Exists(filter);
        }
    }
}
