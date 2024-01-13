

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class CategorySizeRepository : BaseRepository<CategorySize>, ICategorySizeRepository
    {
        private readonly TailorProTrackContext _context;

        public CategorySizeRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public override int Save(CategorySize entity)
        {
            entity.CREATED_AT = DateTime.Now;

            this._context.Add(entity);
            this._context.SaveChanges();
            
            return entity.ID;
        }

        public override void Update(CategorySize entity)
        {
            CategorySize categorySize = this.GetEntity(entity.ID);

            categorySize.CATEGORY = entity.CATEGORY;
            categorySize.MODIFIED_AT = DateTime.Now;
            categorySize.USER_MOD = entity.USER_MOD;

            this._context.Update(categorySize);
            this._context.SaveChanges();
        }

        public override void Remove(CategorySize entity)
        {
            CategorySize categorySize = this.GetEntity(entity.ID);

            categorySize.REMOVED = entity.REMOVED;
            categorySize.USER_MOD = entity.USER_MOD;
            categorySize.MODIFIED_AT = DateTime.Now;

            this._context.Update(categorySize);
            this._context.SaveChanges();
        }
    }
}
