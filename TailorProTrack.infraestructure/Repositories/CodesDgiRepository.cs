

using System.Security.Cryptography.X509Certificates;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class CodesDgiRepository(TailorProTrackContext ctx) : BaseRepository<CodesDgi>(ctx), ICodesDgiRepository
    {
        private readonly TailorProTrackContext _ctx = ctx;

        public override int Save(CodesDgi entity)
        {
            //asignando el valor inicial del primer numero
            entity.CURRENT_NUMBER = entity.INITIAL_NUMBER;

            CodesDgi code = _ctx.Set<CodesDgi>().FirstOrDefault();
            if (code != null)
            {
                entity.ID = code.ID;
                _ctx.Entry(code).CurrentValues.SetValues(entity);
                _ctx.SaveChanges();
                return code.ID;
            }
            return base.Save(entity);
        }

        public int UseCode()
        {
            CodesDgi code = _ctx.Set<CodesDgi>().FirstOrDefault();
           
            if (code.CURRENT_NUMBER >= code.END_NUMBER || code == null)
            {
                return 0;
            }
            int currentNumber = code.CURRENT_NUMBER;
            code.CURRENT_NUMBER++;
            this.Update(code);
            return currentNumber;
        }

        public int ReverseCode()
        {
            CodesDgi code = _ctx.Set<CodesDgi>().FirstOrDefault();
            if (code == null || code.CURRENT_NUMBER <= code.INITIAL_NUMBER)
            {
                return 0;
            }

            code.CURRENT_NUMBER--;
            this.Update(code);
            return code.CURRENT_NUMBER;
        }
    }

}
