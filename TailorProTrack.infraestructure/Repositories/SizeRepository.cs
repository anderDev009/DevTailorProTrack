using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        private readonly TailorProTrackContext _context;
        public SizeRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _context = ctx;
        }
        public override List<Size> GetEntities()
        {
            return this._entities.Where(s => !s.REMOVED)
                 .Join(_context.CATEGORYSIZE,
                       size => size.FKCATEGORYSIZE,
                       categorySize => categorySize.ID,
                       (size, categorySize) => new { size, categorySize })
                 .Select(data => new Size
                 {
                     ID = data.size.ID,
                     FKCATEGORYSIZE = data.size.FKCATEGORYSIZE,
                     SIZE = data.size.SIZE
                 })
                 .ToList();
        }
        public override int Save(Size entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }

   
        public override void Update(Size entity)
        {
            Size sizeToUpdate = this.GetEntity(entity.ID);


            sizeToUpdate.MODIFIED_AT = DateTime.Now;
            sizeToUpdate.USER_MOD = entity.USER_MOD;
            sizeToUpdate.SIZE = entity.SIZE;
            sizeToUpdate.FKCATEGORYSIZE = entity.FKCATEGORYSIZE;

            this._context.Update(sizeToUpdate);
            this._context.SaveChanges();
        }

        public List<Size> FilterByName(string name)
        {
            return this._entities.Where(s => EF.Functions.Like(s.SIZE, $"{name}%"))
                .Join(_context.CATEGORYSIZE,
                      size => size.FKCATEGORYSIZE,
                      categorySize => categorySize.ID,
                      (size, categorySize) => new { size, categorySize })
                .Select(data => new Size
                {
                    ID = data.size.ID,
                    FKCATEGORYSIZE = data.size.FKCATEGORYSIZE,
                    SIZE = data.size.SIZE,
                    categorySize = data.categorySize,
                }).ToList();
        }

        public List<Size> FilterByIdCategory(int categoryId)
        {
            return this._entities.Where(s => s.FKCATEGORYSIZE == categoryId).Join(_context.CATEGORYSIZE,
                      size => size.FKCATEGORYSIZE,
                      categorySize => categorySize.ID,
                      (size, categorySize) => new { size, categorySize })
                .Select(data => new Size
                {
                    ID = data.size.ID,
                    FKCATEGORYSIZE = data.size.FKCATEGORYSIZE,
                    SIZE = data.size.SIZE,
                    categorySize = data.categorySize,
                }).ToList();
        }

        public List<Size> SizeByAsociatedProductId(int productId)
        {
            List<Size> sizes = _context.Set<Size>()
                .Include(x => x.ProductSize)
                .Where(c => c.ProductSize.Any(x => x.FK_PRODUCT == productId))
                .ToList();

            return sizes;
        }
    }
}
