using Microsoft.EntityFrameworkCore;
using System.Net;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly TailorProTrackContext _context;
        private readonly INoteCreditRepository _noteCreditRepository;
        public ClientRepository(TailorProTrackContext context) : base(context)
        {
            this._context = context;
        }

        public override int Save(Client entity)
        {
            entity.CREATED_AT = DateTime.Now;
 
            this._context.Add(entity);
            this._context.SaveChanges();


			return entity.ID;
        }
        public override void Update(Client entity)
        {
            Client clientToUpdate = this.GetEntity(entity.ID);
            if(clientToUpdate == null)
            {
                throw new System.Exception("Cliente no encontrado");
            }

            clientToUpdate.FIRST_NAME = entity.FIRST_NAME;
            clientToUpdate.LAST_NAME = entity.LAST_NAME;
            clientToUpdate.FIRST_SURNAME = entity.FIRST_SURNAME;
            clientToUpdate.LAST_SURNAME = entity.LAST_SURNAME;
            clientToUpdate.RNC = entity.RNC;
            clientToUpdate.DNI = entity.DNI;
            //------
            clientToUpdate.MODIFIED_AT = DateTime.Now;
            clientToUpdate.USER_MOD = entity.USER_MOD;
            //------
            this._context.Update(clientToUpdate);
            this._context.SaveChanges();
        }

        
        public override void Remove(Client entity)
        {
            Client clientToRemove = this.GetEntity(entity.ID);

            clientToRemove.REMOVED = true;
            clientToRemove.USER_MOD = entity.USER_MOD;
            clientToRemove.MODIFIED_AT = DateTime.Now;

            this._context.Update(clientToRemove);
            this._context.SaveChanges();
        }

        public List<Client> GetAll()
        {
            return this._entities.ToList();
        }

        public List<Client> FilterByDni(string dni)
        {
            return this._entities.Where(cl => EF.Functions.Like(cl.DNI,$"{dni}%")).ToList();
        }

        public List<Client> FilterByRnc(string rnc)
        {
            return this._entities.Where(cl => EF.Functions.Like(cl.RNC, $"{rnc}%")).ToList();
        }

        public List<Client> FilterByFullName(string fullName)
        {
            return this._entities.Where(cl => EF.Functions.Like((cl.FIRST_NAME
                + " " + cl.LAST_NAME+ " " + cl.FIRST_SURNAME+ " " + cl.LAST_SURNAME), $"{fullName}%")).ToList();
           
        }
    }
}
