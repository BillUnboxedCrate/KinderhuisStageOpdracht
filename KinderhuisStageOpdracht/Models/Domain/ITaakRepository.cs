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
        void AddTaak(Taak taak);
        void DeleteTaak(int taakId);
        void UpdateTaak(Taak taak);

        void SaveChanges();
    }
}
