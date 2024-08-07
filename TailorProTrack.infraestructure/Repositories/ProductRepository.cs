﻿
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly TailorProTrackContext _context;
        
        public ProductRepository(TailorProTrackContext ctx ) : base(ctx) 
        {
            this._context = ctx;
        }

        public override List<Product> GetEntityToJoin(int id)
        {
            return this._entities.Where(data => data.ID == id).ToList();
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

        public void UpdateLastReplenishment(int id)
        {
            Product product = this.GetEntity(id);

            product.LAST_REPLENISHMENT = DateTime.Now;
            this._context.Update(product);
            this._context.SaveChanges();
        }

        //filters
        public List<Product> GetByMinorPrice(decimal price)
        {
            return this._entities.Where(data => data.SALE_PRICE < price).ToList();
        }

        public List<Product> GetByHigherPrice(decimal price)
        {
            return this._entities.Where(data => data.SALE_PRICE > price).ToList();

        }

        public List<Product> SearchByType(int fkType)
        {
            return this._entities.Where(data => data.FK_TYPE == fkType).ToList();
        }

        public List<Product> SearchByName(string name)
        {
           return this._entities.Where(data => EF.Functions.Like(data.NAME_PRODUCT,name)).ToList();    
        }
    }
}
