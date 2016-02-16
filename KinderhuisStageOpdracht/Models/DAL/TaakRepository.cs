using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class TaakRepository:ITaakRepository
    {
        private readonly ProjectContext _context;

        public TaakRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Taak> FindAll()
        {
            return _context.TaakSet.OrderBy(t => t.Id);
        }

        public Taak FindById(int id)
        {
            return _context.TaakSet.Find(id);
        }

        public void AddTaak(Taak taak)
        {
            _context.TaakSet.Add(taak);
        }

        public void DeleteTaak(int taakId)
        {
            Taak taak = _context.TaakSet.Find(taakId);
            _context.TaakSet.Remove(taak);
        }

        public void UpdateTaak(Taak taak)
        {
            _context.Entry(taak).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}