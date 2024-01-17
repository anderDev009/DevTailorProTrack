using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        private readonly TailorProTrackContext _context;

        public ColorRepository(TailorProTrackContext ctx) : base(ctx)
        {
            this._context = ctx;
        }

        public override int Save(Color entity)
        {
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }
        public override void Update(Color entity)
        {
            Color color = this.GetEntity(entity.ID);
            
            color.MODIFIED_AT = DateTime.Now;
            color.USER_MOD = entity.USER_MOD;
            color.COLORNAME = entity.COLORNAME;
            color.CODE_COLOR = entity.CODE_COLOR;

            this._context.Update(color);
            this._context.SaveChanges();
        }
    }
}
