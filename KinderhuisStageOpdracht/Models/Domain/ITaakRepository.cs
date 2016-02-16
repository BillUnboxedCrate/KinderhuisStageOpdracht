using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    interface ITaakRepository
    {
        IQueryable<Taak> FindAll();
        Taak FindById(int id);
        void AddMenu(Taak taak);
        void DeleteMenu(int id);
        void UpdateMenu(Taak taak);

        void SaveChanges();
    }
}
