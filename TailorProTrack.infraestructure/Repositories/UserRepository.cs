
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;
//using BCrypt.Net;
namespace TailorProTrack.infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TailorProTrackContext _context;
        public UserRepository(TailorProTrackContext context) : base(context) { }

        public override int Save(User entity)
        {
            entity.CREATED_AT = DateTime.Now;
            entity.PASS = BCrypt.Net.BCrypt.HashPassword(entity.PASS,BCrypt.Net.BCrypt.GenerateSalt());
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }

        public override void Update(User entity)
        {
            User userToUpdate = this.GetEntity(entity.ID);

            userToUpdate.NAME_USER = entity.NAME_USER;
            userToUpdate.MODIFIED_AT = DateTime.Now;
            userToUpdate.USER_MOD = entity.USER_MOD;
            //en caso de actualizar la passw
            userToUpdate.PASS = BCrypt.Net.BCrypt.HashPassword(entity.PASS,BCrypt.Net.BCrypt.GenerateSalt());
            //
            this._context.Update(userToUpdate);
            this._context.SaveChanges();
        }

        public int UpdatePassword(User user)
        {
            User userToUpdate = this.GetEntity(user.ID);

            userToUpdate.MODIFIED_AT = DateTime.Now;
            userToUpdate.USER_MOD = user.USER_MOD;
            //en caso de actualizar la passw
            userToUpdate.PASS = BCrypt.Net.BCrypt.HashPassword(user.PASS, BCrypt.Net.BCrypt.GenerateSalt());
            //
            this._context.Update(userToUpdate);
            this._context.SaveChanges();
            return userToUpdate.ID;
        }

        public int UpdateUsername(User user)
        {
            User userToUpdate = this.GetEntity(user.ID);

            userToUpdate.NAME_USER = user.NAME_USER;
            userToUpdate.MODIFIED_AT = DateTime.Now;
            userToUpdate.USER_MOD = user.USER_MOD;
            //
            this._context.Update(userToUpdate);
            this._context.SaveChanges();
            return userToUpdate.ID;
        }
    }
}
