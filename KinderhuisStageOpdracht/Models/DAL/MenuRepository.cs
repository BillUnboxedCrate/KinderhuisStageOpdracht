using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class MenuRepository: IMenuRepository
    {
        private readonly ProjectContext _context;

        public MenuRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Menu> FindAll()
        {
            return _context.MenuSet.OrderBy(m => m.Id);
        }

        public Menu FindById(int id)
        {
            return _context.MenuSet.Find(id);
        }

        public void AddMenu(Menu menu)
        {
            _context.MenuSet.Add(menu);
        }

        public void DeleteMenu(int id)
        {
            Menu menu = _context.MenuSet.Find(id);
            _context.MenuSet.Remove(menu);
        }

        public void UpdateMenu(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}