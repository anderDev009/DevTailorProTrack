using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override void Save(Size entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity);
            this._context.SaveChanges();
        }

        public override void Remove(Size entity)
        {
            this._context.Update(entity);
            this._context.SaveChanges();
        }
        public override void Update(Size entity)
        {
            Size sizeToUpdate = this.GetEntity(entity.ID);
           

            sizeToUpdate.MODIFIED_AT = DateTime.Now;
            sizeToUpdate.USER_MOD = entity.USER_MOD;
            sizeToUpdate.SIZE = entity.SIZE;

            this._context.Update(sizeToUpdate);
            this._context.SaveChanges();
        }

    }
}
