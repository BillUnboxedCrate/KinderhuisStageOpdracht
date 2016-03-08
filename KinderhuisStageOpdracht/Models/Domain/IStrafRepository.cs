using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    interface IStrafRepository
    {
        IQueryable<Straf> FindAll();
        Straf FindById(int id);

        void SaveChanges();

    }
}
