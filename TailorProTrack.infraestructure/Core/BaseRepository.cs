using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TailorProTrack.domain.Core;
using TailorProTrack.infraestructure.Context;

namespace TailorProTrack.infraestructure.Core
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly TailorProTrackContext _context;
        protected DbSet<T> _entities;
        public BaseRepository(TailorProTrackContext ctx) 
        {
            this._context = ctx;
            _entities = this._context.Set<T>();
        }

        public int CountEntities()
        {
            return this._entities.Where(data => !data.REMOVED).Count();
        }

        public virtual  bool Exists(Expression<Func<T, bool>> filter)
        {
            return this._entities.Any(filter);
        }

        public virtual List<T> FindAll(Expression<Func<T, bool>> filter)
        {
            return this._entities.Where(filter).ToList();
        }
        public virtual List<T> GetEntities()
        {
            return this._entities.ToList();
        }

        public List<T> GetEntitiesPaginated(int page, int itemsPage)
        {
            return this._entities.Where(data => !data.REMOVED).Skip((page - 1) * itemsPage).Take(itemsPage).ToList();
        }

        public virtual T GetEntity(int id)
        {
            return this._entities.Find(id);
        }

        public virtual List<T> GetEntityToJoin(int id)
        {
            return this._entities.Where(data => data.ID == id).ToList();
        }

        public virtual void Remove(T entity)
        {
           
            T entityToRemove = this.GetEntity(entity.ID);

            entityToRemove.MODIFIED_AT = DateTime.Now;
            entityToRemove.USER_MOD = entity.USER_MOD;
            entityToRemove.REMOVED = true;
            this._entities.Update(entityToRemove);
        }

        public virtual int Save(T entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._entities.Add(entity);
            _context.SaveChanges();
            return entity.ID;
        }

        public IQueryable<T> SearchEntities()
        {
            return this._entities.AsQueryable();
        }

        public virtual void Update(T entity)
        {
            this._entities.Update(entity);
        }

    }
}
