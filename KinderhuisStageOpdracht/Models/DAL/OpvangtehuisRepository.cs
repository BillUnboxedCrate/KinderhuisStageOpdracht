using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class OpvangtehuisRepository:IOpvangtehuisRepository
    {
        private readonly ProjectContext _context;

        public OpvangtehuisRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Opvangtehuis> FindAll()
        {
            return _context.OpvangtehuisSet.OrderBy(oh => oh.Naam);
        }

        public Opvangtehuis FindById(int id)
        {
            return _context.OpvangtehuisSet.Find(id);
        }

        public Opvangtehuis FindByName(string name)
        {
            return _context.OpvangtehuisSet.SingleOrDefault(oh => oh.Naam == name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}