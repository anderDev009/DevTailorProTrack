using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        void UpdateLastReplenishment(int id);

        List<Product> GetByMinorPrice (decimal price);
        List<Product> GetByHigherPrice (decimal price);

        List<Product> SearchByType(int fkType);
        List<Product> SearchByName(string name);
    }
}
