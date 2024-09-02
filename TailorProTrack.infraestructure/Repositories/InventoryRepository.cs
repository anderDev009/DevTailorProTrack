
using System.ComponentModel.Design;
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
            Inventory inventory = this.GetEntity(entity.ID);

            inventory.USER_MOD = entity.USER_MOD;
            inventory.MODIFIED_AT = DateTime.Now;
            inventory.REMOVED = true;

            this._context.Update(inventory);
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

        public bool AddInventoryByBuy(List<BuyInventoryDetail> detail)
        {
            foreach (var item in detail)
            {

                Inventory invSearched = _context.Set<Inventory>().Where(i => i.FK_PRODUCT == item.FK_PRODUCT && i.FK_SIZE == item.FK_SIZE).FirstOrDefault();
                //comprobamos de que si no existe un producto con ese size se registre 
                if (invSearched == null)
                {
                    Inventory inv = new Inventory
                    {
                        FK_PRODUCT = item.FK_PRODUCT,
                        FK_SIZE = item.FK_SIZE,
                        CREATED_AT = DateTime.Now,
                        //temporal
                        USER_CREATED = 1
                    };
                    _context.Set<Inventory>().Add(inv);
                    _context.SaveChanges();
                    _context.Set<InventoryColor>().Add(new InventoryColor
                    {
                        CREATED_AT = DateTime.Now,
                        FK_COLOR_PRIMARY = item.COLOR_PRIMARY,
                        FK_COLOR_SECONDARY = item.COLOR_SECONDARY,
                        FK_INVENTORY = inv.ID,
                        QUANTITY = item.QUANTITY,
                        //temporal
                        USER_CREATED = 1
                    });
                    _context.SaveChanges();
                    UpdateQuantityInventory(inv.ID);
                    //break;
                }
                else
                {
                    //comprobamos que exista el invColor
                    if (!_context.Set<InventoryColor>().Any(c => c.FK_INVENTORY == invSearched.ID && c.FK_COLOR_PRIMARY == item.COLOR_PRIMARY && c.FK_COLOR_SECONDARY == item.COLOR_SECONDARY))
                    {
                        _context.Set<InventoryColor>().Add(new InventoryColor
                        {
                            CREATED_AT = DateTime.Now,
                            FK_COLOR_PRIMARY = item.COLOR_PRIMARY,
                            FK_COLOR_SECONDARY = item.COLOR_SECONDARY,
                            QUANTITY = item.QUANTITY,
                            FK_INVENTORY = invSearched.ID,

                            //temporal
                            USER_CREATED = 1
                        });
                        _context.SaveChanges();
                    }
                    else
                    {
                        InventoryColor invColor = _context.Set<InventoryColor>().Where(i => i.FK_INVENTORY == invSearched.ID && i.FK_COLOR_PRIMARY == item.COLOR_PRIMARY && i.FK_COLOR_SECONDARY == item.COLOR_SECONDARY).First();

                        invColor.QUANTITY += item.QUANTITY;
                        _context.Set<InventoryColor>().Update(invColor);
                        _context.SaveChanges();

                    }
                    UpdateQuantityInventory(invSearched.ID);
                    _context.SaveChanges();
                }

            }
            return true;
        }

        public bool UpdateQuantityInventory(int id)
        {
            int quantity = _context.Set<InventoryColor>().Where(i => i.FK_INVENTORY == id)
                .GroupBy(i => i.FK_INVENTORY)
                .Select(i => i.Sum(i => i.QUANTITY)).First();


            Inventory inv = _context.Set<Inventory>().Find(id);
            inv.QUANTITY = quantity;
            Update(inv);
            int success = _context.SaveChanges();
            if (success == 0)
            {
                return false;
            }
            return true;
        }

        public bool RemoveInventoryByBuy(List<BuyInventoryDetail> detail)
        {
            foreach (var item in detail)
            {
                Inventory invSearched = _context.Set<Inventory>().Where(i => i.FK_PRODUCT == item.FK_PRODUCT && i.FK_SIZE == item.FK_SIZE).FirstOrDefault();
                if (invSearched != null)
                {
                    InventoryColor invColor = _context.Set<InventoryColor>().Where(i => i.FK_INVENTORY == invSearched.ID && i.FK_COLOR_PRIMARY == item.COLOR_PRIMARY && i.FK_COLOR_SECONDARY == item.COLOR_SECONDARY).First();
                    invColor.QUANTITY -= item.QUANTITY;
                    _context.Set<InventoryColor>().Update(invColor);
                    _context.SaveChanges();
                    UpdateQuantityInventory(invSearched.ID);
                }
            }
            return true;    
        }
    }
}
