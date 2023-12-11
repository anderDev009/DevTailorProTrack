using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.infraestructure.Core
{
    public interface IBaseRepository<T> where T : class
    {
        void Remove(T entity);
        void Update(T entity);  
        void Save(T entity);
        List<T> FindAll(Expression<Func<T, bool>> filter);
        T GetEntity(int id);

        bool Exists(Expression<Func<T, bool>> filter);
    }
}
