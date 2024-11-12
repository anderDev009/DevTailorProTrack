
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		private readonly TailorProTrackContext _context;
		private readonly IInventoryColorRepository _inventoryColorRepository;
		private readonly IInventoryRepository _inventoryRepository;
		private readonly IBuyInventoryRepository _buyInventoryRepository;

        public OrderRepository(TailorProTrackContext context, IInventoryColorRepository inventoryColorRepository, 
			IInventoryRepository inventoryRepository,
			IBuyInventoryRepository buyInventoryRepository) : base(context)
		{
			this._context = context;
			_inventoryColorRepository = inventoryColorRepository;
			_inventoryRepository = inventoryRepository;
			_buyInventoryRepository = buyInventoryRepository;
		}

		public override int Save(Order entity)
		{

			entity.CREATED_AT = DateTime.UtcNow;
			entity.CHECKED = false;
			this._context.Add(entity);
			this._context.SaveChanges();
            _buyInventoryRepository.MarkBuysUsed();

            return entity.ID;
		}

		public override void Update(Order entity)
		{
			Order order = this.GetEntity(entity.ID);

			order.CHECKED = entity.CHECKED;
			order.AMOUNT = entity.AMOUNT;
			order.FK_CLIENT = entity.FK_CLIENT;
			order.FK_USER = entity.FK_USER;
			order.MODIFIED_AT = DateTime.Now;
			order.USER_MOD = entity.USER_MOD;
			order.DESCRIPTION_JOB = entity.DESCRIPTION_JOB;
			order.FK_PREORDER = entity.FK_PREORDER;

			this._context.Update(order);
			this._context.SaveChanges();
		}

		public override void Remove(Order entity)
		{
			List<OrderProduct> orderProducts = _context.Set<OrderProduct>()
													   .Where(x => x.FK_ORDER == entity.ID)
													   .ToList();
			//logica para devolver el inventario color
			foreach (var product in orderProducts)
			{
				//obteniendo el inventario color para agregarle la cantidad
				InventoryColor invColor = _inventoryColorRepository.GetEntity(product.FK_INVENTORYCOLOR);
				invColor.QUANTITY += product.QUANTITY;
				//actualizando la cantidad 
				_inventoryColorRepository.Update(invColor);
				//actualizar el inventario mientras se van agregando 
				_inventoryRepository.UpdateQuantityInventory(invColor.FK_INVENTORY);
				_context.Set<OrderProduct>().Remove(product);
			}
			//eliminar la orden
			_context.Set<Order>().Remove(entity);
			_context.SaveChanges();
		}

		public void UpdateAmount(Order order)
		{
			Order orderToUpdate = this.GetEntity(order.ID);

			//actualizando el monto
			orderToUpdate.AMOUNT = order.AMOUNT;

			this._context.Update(orderToUpdate);
			this._context.SaveChanges();
		}

		public void UpdateStatusOrder(Order order)
		{
			Order orderToUpdate = this.GetEntity(order.ID);
			//validarlo luego
			if (orderToUpdate == null) throw new Exception("No existe");
			orderToUpdate.STATUS_ORDER = order.STATUS_ORDER;
			orderToUpdate.USER_MOD = order.USER_MOD;
			orderToUpdate.MODIFIED_AT = DateTime.Now;

			this._context.Update(orderToUpdate);
			this._context.SaveChanges();
		}

		public void CheckCompleteOrder(int id)
		{
			var entity = _context.Set<Order>().Find(id);
			if (!entity.CHECKED)
			{
				entity.CHECKED = true;
				entity.STATUS_ORDER = "completada";
			}
			else
			{
				entity.CHECKED = false;
				entity.STATUS_ORDER = "pendiente";
			}

			_context.Set<Order>().Update(entity);
			_context.SaveChanges();
		}

		public bool ConfirmOrdersIsComplete(int idPreOrder)
		{
			var orders = _context.Set<Order>().Where(x => x.FK_PREORDER == idPreOrder).ToList();
			//en caso de que no hayan ordenes se marca como falso
			if(orders.Count == 0) return false;
			bool isCompleted = true;
			foreach (var order in orders)
			{
				if (order.STATUS_ORDER != "completada")
				{
					isCompleted = false;

				}

			}
			return isCompleted;
		}
	}
}

