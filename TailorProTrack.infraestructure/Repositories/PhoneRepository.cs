

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
    {
        private readonly TailorProTrackContext _context;
        public PhoneRepository(TailorProTrackContext context): base(context)
        {
            this._context = context;
        }

        public override int Save(Phone entity)
        {
            entity.CREATED_AT = DateTime.Now;

            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }

        public override void Update(Phone entity)
        {
            Phone phoneToUpdate = this.GetEntity(entity.ID);

            phoneToUpdate.NUMBER = entity.NUMBER;
            phoneToUpdate.TYPE = entity.TYPE;
            //
            phoneToUpdate.USER_MOD = entity.USER_MOD;
            phoneToUpdate.MODIFIED_AT = entity.MODIFIED_AT;


        }

        public override void Remove(Phone entity)
        {
            Phone phoneToRemove = this.GetEntity(entity.ID);

            phoneToRemove.REMOVED = true;
            phoneToRemove.MODIFIED_AT = DateTime.Now;
            phoneToRemove.USER_MOD = entity.USER_MOD;

            this._context.Update(phoneToRemove);
        }

        public void SaveMany(List<Phone> phones)
        {
            DateTime now = DateTime.Now;
            foreach(Phone item in phones)
            {
                item.CREATED_AT = now;
                var newPhone = new Phone 
                {
                    FK_CLIENT = item.FK_CLIENT,
                    NUMBER = item.NUMBER,
                    TYPE = item.TYPE,
                    USER_CREATED = item.USER_CREATED,
                    CREATED_AT = now
                };

                this._context.Add(newPhone);
            }

            this._context.SaveChanges();
        }
    }
}
