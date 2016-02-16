using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class GebruikerRepository:IGebruikerRepository
    {
        private readonly ProjectContext _context;

        public GebruikerRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Gebruiker> FindAll()
        {
            return _context.GebruikerSet.OrderBy(g => g.Voornaam);
        }

        public Gebruiker FindById(int id)
        {
            return _context.GebruikerSet.Find(id);
        }

        public IQueryable<Admin> FindAllAdmins()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Opvoeder> FindAllOpvoeders()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Client> FindAllClients()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}