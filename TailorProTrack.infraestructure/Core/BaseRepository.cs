using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TailorProTrack.infraestructure.Context;

namespace TailorProTrack.infraestructure.Core
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
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
            return this._entities.Count();
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
            return this._entities.Skip((page - 1) * itemsPage).Take(itemsPage).ToList();
        }

        public virtual T GetEntity(int id)
        {
            return this._entities.Find(id);
        }
        public virtual void Remove(T entity)
        {
            this._entities.Remove(entity);
        }

        public virtual int Save(T entity)
        {
            this._entities.Add(entity);
            return 0;
        }

        public virtual void Update(T entity)
        {
            this._entities.Update(entity);
        }

    }
}
