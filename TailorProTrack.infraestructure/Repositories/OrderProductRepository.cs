
using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly TailorProTrackContext _context;
        //repositorio de compras
        private readonly IBuyInventoryRepository _buyInventoryRepository;
        public OrderProductRepository(TailorProTrackContext context, IBuyInventoryRepository buyInventoryRepository) : base(context)
        {
            _context = context;
            _buyInventoryRepository = buyInventoryRepository;
        }



        public override int Save(OrderProduct entity)
        {
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.FK_ORDER;
        }

        public override void Update(OrderProduct entity)
        {
            OrderProduct orderP = this.GetByForeignKeys(entity.FK_ORDER, entity.FK_INVENTORYCOLOR);
            orderP.QUANTITY = entity.QUANTITY;
            orderP.FK_ORDER = entity.FK_ORDER;
            orderP.FK_INVENTORYCOLOR = entity.FK_INVENTORYCOLOR;

            this._context.Update(orderP);
            this._context.SaveChanges();
        }

        public override void Remove(OrderProduct entity)
        {
            OrderProduct orderP = this.GetEntity(entity.ID);
            this._context.Remove(orderP);
        }

        public OrderProduct GetByForeignKeys(int idOrder, int idProduct)
        {
            return this._entities
                                  .Where(data => data.FK_INVENTORYCOLOR == idOrder &&
                                         data.FK_ORDER == idProduct)
                                  .Single();

        }

        public void SaveMany(List<OrderProduct> products)
        {
            //comprobando por si hay un producto registrado en compras que no han sido marcadas como usadas
            var buyDetails = this._context.Set<BuyInventoryDetail>()
                                          .Include(x => x.BuyInventory)
                                          .Where(x => x.BuyInventory.USED == false)
                                          .ToList();

            foreach ( var buy in buyDetails)
            {
               

            }
            foreach (var item in products)
            {
                //obteniendo el inventory color de cada producto
                var invColor = this._context.Set<InventoryColor>()
                                           .Include(x => x.Inventory)
                                           .Where(x => x.FK_INVENTORY == item.FK_INVENTORYCOLOR)
                                           .FirstOrDefault();

                //Extrayendo los detalles de la compra
                var buy = buyDetails.Where(x => x.COLOR_PRIMARY == invColor.FK_COLOR_PRIMARY && x.FK_PRODUCT == invColor.Inventory.FK_PRODUCT && x.FK_SIZE == invColor.Inventory.FK_SIZE).ToList();
                //comprobando si hay algun producto en la compra
                if (buy.Count != 0)
                {
                    //marcando la compra como utilizada en caso de que se haya encontrado un producto
                    foreach(var buyItem in buy)
                    {
                        this._buyInventoryRepository.CheckUsed(buyItem.FK_BUY_INVENTORY);
                    }

                }
                //guardando
                item.CREATED_AT = DateTime.Now; 
                this._context.Add(item);
            }
            this._context.SaveChanges();
        }

        public void ValidateMany(List<OrderProduct> products)
        {
            
            foreach(var item in products)
            {
                
            }
        }

 
    }
}
