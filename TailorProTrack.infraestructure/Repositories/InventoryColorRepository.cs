﻿using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class InventoryColorRepository : BaseRepository<InventoryColor>, IInventoryColorRepository
    {
        private readonly TailorProTrackContext _context;

        public InventoryColorRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            _context = ctx;
        }
        public override int Save(InventoryColor entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }
        public override void Remove(InventoryColor entity)
        {
            InventoryColor colorToRemove = this.GetEntity(entity.ID);

            colorToRemove.MODIFIED_AT = DateTime.Now;
            colorToRemove.REMOVED = true;
            colorToRemove.USER_MOD = entity.USER_MOD;

            this._context.Update(colorToRemove);
            this._context.SaveChanges();    
        }
        public override void Update(InventoryColor entity)
        {
            InventoryColor inventoryColor = this.GetEntity(entity.ID);

            inventoryColor.QUANTITY = entity.QUANTITY;
            inventoryColor.FK_COLOR_PRIMARY= entity.FK_COLOR_PRIMARY;
            inventoryColor.FK_COLOR_SECONDARY= entity.FK_COLOR_SECONDARY;
            inventoryColor.FK_INVENTORY = entity.FK_INVENTORY;
            inventoryColor.MODIFIED_AT = DateTime.Now;
            inventoryColor.USER_MOD = entity.USER_MOD;


            this._context.Set<InventoryColor>().Update(inventoryColor);
            this._context.SaveChanges();
        }

        public InventoryColor SearchAvailabilityToAddOrder(int SizeId, int OrderId, int colorPrimary, int? colorSecondary)
        {
            return _entities.Join(_context.INVENTORY,
                invColor => invColor.FK_INVENTORY,
                inventory => inventory.ID, (invColor,inventory) => new {invColor,inventory})
                .Where(data => data.inventory.FK_SIZE == SizeId && data.inventory.FK_PRODUCT ==  OrderId 
                && data.invColor.FK_COLOR_PRIMARY == colorPrimary)
                .Select(data => data.invColor).First();
        }

     
    }
}
