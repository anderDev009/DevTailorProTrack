﻿
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        private readonly TailorProTrackContext _context;
        public InventoryRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            this._context = ctx;
        }
        public override void Remove(Inventory entity)
        {
            this._context.Update(entity);
            this._context.SaveChanges();
        }
        public override void Update(Inventory entity)
        {
            Inventory inventoryToUpdate = this.GetEntity(entity.ID);

            inventoryToUpdate.QUANTITY = entity.QUANTITY;
            inventoryToUpdate.FK_SIZE = entity.FK_SIZE;
            inventoryToUpdate.FK_PRODUCT = entity.FK_PRODUCT;
            inventoryToUpdate.MODIFIED_AT = DateTime.Now;
            inventoryToUpdate.USER_MOD = entity.USER_MOD;


            this._context.Update(inventoryToUpdate);
            this._context.SaveChanges();
        }

        public override int Save(Inventory entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }
    }
}