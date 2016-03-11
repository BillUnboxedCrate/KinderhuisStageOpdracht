using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public interface IStrafRepository
    {
        IQueryable<Straf> FindAll();
        Straf FindById(int id);
        Straf FindByNaam(string naam);
        void AddStraf(Straf straf);

        void SaveChanges();

    }
}
