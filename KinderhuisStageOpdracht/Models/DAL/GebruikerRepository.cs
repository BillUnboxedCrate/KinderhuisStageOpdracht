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

        public Gebruiker FindByUsername(string username)
        {
            return _context.GebruikerSet.FirstOrDefault(g => g.Gebruikersnaam.ToLower() == username.ToLower());
        }

        public IQueryable<Gebruiker> FindAllAdmins()
        {
            return _context.GebruikerSet.Where(g => g is Admin);
        }

        public IQueryable<Gebruiker> FindAllOpvoeders()
        {
            return _context.GebruikerSet.Where(g => g is Opvoeder);
        }

        public IQueryable<Gebruiker> FindAllClients()
        {
            return _context.GebruikerSet.Where(g => g is Client);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}