

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class TypeProdRepository : BaseRepository<Type_prod>, ITypeProdRepository
    {
        private readonly TailorProTrackContext _context;
        
        public TypeProdRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            this._context = ctx;
        }

        public override int Save(Type_prod entity)
        {
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }
        public override void Update(Type_prod entity)
        {
            Type_prod typeToUpdate = this.GetEntity(entity.ID);

            typeToUpdate.TYPE_PROD = entity.TYPE_PROD;
            typeToUpdate.MODIFIED_AT = DateTime.Now;
            typeToUpdate.USER_MOD = entity.USER_MOD;

            this._context.Update(typeToUpdate);
            this._context.SaveChanges();
        }

        public override void Remove(Type_prod entity)
        {
            entity.REMOVED = true;

            this._context.Update(entity);
            this._context.SaveChanges();
        }
    }
}
