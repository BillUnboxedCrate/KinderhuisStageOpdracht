using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class KamerControleOpdrachtRepository:IKamerControleOpdrachtRepository
    {
        private readonly ProjectContext _context;

        public KamerControleOpdrachtRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<KamerControleOpdracht> FindAll()
        {
            return _context.KamerControleOpdrachtSet.OrderBy(k => k.Titel);
        }

        public KamerControleOpdracht FindById(int id)
        {
            return _context.KamerControleOpdrachtSet.Find(id);
        }

        public KamerControleOpdracht FindByName(string name)
        {
            return _context.KamerControleOpdrachtSet.FirstOrDefault(k => k.Titel == name);
        }
    }
}