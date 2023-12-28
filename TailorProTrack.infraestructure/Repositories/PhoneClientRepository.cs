
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PhoneClientRepository : BaseRepository<PhoneClient>, IPhoneClientRepository
    {
        private readonly TailorProTrackContext context;

        public PhoneClientRepository(TailorProTrackContext context): base(context)
        {
            this.context = context;
        }

        public override int Save(PhoneClient entity)
        {
            this.context.Add(entity);
            this.context.SaveChanges();
            return entity.FK_PHONE;
        }

        public override void Update(PhoneClient entity)
        {
            PhoneClient phoneClient = this.GetEntity(entity.FK_PHONE);

            phoneClient.FK_CLIENT = entity.FK_CLIENT;
            phoneClient.FK_PHONE = entity.FK_PHONE;

            this.context.Update(phoneClient);
            this.context.SaveChanges();
        }

        public override void Remove(PhoneClient entity)
        {
            PhoneClient phoneClient = this.GetEntity(entity.FK_PHONE);

            this.context.Remove(phoneClient);
            this.context.SaveChanges();
        }
    }
}
