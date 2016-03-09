using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class KamerControleItemRepository:IKamerControleItemRepository
    {
        private readonly ProjectContext _context;

        public KamerControleItemRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<KamerControleItem> FindAll()
        {
            return _context.KamerControleItemSet.OrderBy(k => k.Titel);
        }

        public KamerControleItem FindById(int id)
        {
            return _context.KamerControleItemSet.Find(id);
        }

        public KamerControleItem FindByName(string name)
        {
            return _context.KamerControleItemSet.FirstOrDefault(k => k.Titel == name);
        }
    }
}