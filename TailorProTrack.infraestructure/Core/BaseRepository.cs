using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.infraestructure.Context;

namespace TailorProTrack.infraestructure.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TailorProTrackContext _context;
        private DbSet<T> _entities;
        public BaseRepository(TailorProTrackContext ctx) 
        {
            this._context = ctx;
            _entities = this._context.Set<T>();
        } 
        public bool Exists(Expression<Func<T, bool>> filter)
        {
            return this._entities.Any();
        }

        public List<T> FindAll(Expression<Func<T, bool>> filter)
        {
            return this._entities.Where(filter).ToList();
        }

        public T GetEntity(int id)
        {
            return this.GetEntity(id);
        }

        public void Remove(T entity)
        {
            this._entities.Remove(entity);
        }

        public void Save(T entity)
        {
            this._entities.Add(entity);
        }

        public void Update(T entity)
        {
            this._entities.Update(entity);
        }
    }
}
