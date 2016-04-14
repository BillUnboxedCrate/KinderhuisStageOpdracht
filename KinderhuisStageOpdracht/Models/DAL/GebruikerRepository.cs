using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return _context.GebruikerSet.OrderBy(g => g.Voornaam).Where(g => g.Visable);
        }

        public Gebruiker FindById(int id)
        {
            return _context.GebruikerSet.FirstOrDefault(g => g.Id == id && g.Visable);
        }

        public Gebruiker FindByUsername(string username)
        {
            return _context.GebruikerSet.FirstOrDefault(g => g.Gebruikersnaam.ToLower() == username.ToLower() && g.Visable);
        }

        public IQueryable<Gebruiker> FindAllAdmins()
        {
            return _context.GebruikerSet.Where(g => g is Admin).Where(g => g.Visable);
        }

        public IQueryable<Gebruiker> FindAllOpvoeders()
        {
            return _context.GebruikerSet.Where(g => g is Opvoeder).Where(g => g.Visable).OrderBy(g => g.Opvangtehuis.Id);
        }

        public IQueryable<Gebruiker> FindAllClients()
        {
            return _context.GebruikerSet.Where(g => g is Client).Where(g => g.Visable).OrderBy(g => g.Opvangtehuis.Id);
        }

        public void AddOpvoeder(Opvoeder opvoeder)
        {
            _context.GebruikerSet.Add(opvoeder);
        }

        public void AddClient(Client client)
        {
            _context.GebruikerSet.Add(client);
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            _context.Entry(gebruiker).State = EntityState.Modified;
        }

        public void UpdateOpvoeder(Opvoeder opvoeder)
        {
            _context.Entry(opvoeder).State = EntityState.Modified;
        }

        public void UpdateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }
        
        public void DeleteGebruiker(int id)
        {
            var gebruiker = _context.GebruikerSet.Find(id); 
            _context.GebruikerSet.Remove(gebruiker);
            _context.Entry(gebruiker).State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}