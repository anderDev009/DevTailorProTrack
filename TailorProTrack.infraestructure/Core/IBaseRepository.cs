
using System.Linq.Expressions;


namespace TailorProTrack.infraestructure.Core
{
    public interface IBaseRepository<T> where T : class
    {       
        void Remove(T entity);
        void Update(T entity);  
        int Save(T entity);
        List<T> GetEntities();
        List<T> FindAll(Expression<Func<T, bool>> filter);
        T GetEntity(int id);

        bool Exists(Expression<Func<T, bool>> filter);
    }
}
