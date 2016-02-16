using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    interface IMenuRepository
    {
        IQueryable<Menu> FindAll();
        Menu FindById(int id);
        void AddMenu(Menu menu);
        void DeleteMenu(int id);
        void UpdateMenu(Menu menu);

        void SaveChanges();
    }
}
