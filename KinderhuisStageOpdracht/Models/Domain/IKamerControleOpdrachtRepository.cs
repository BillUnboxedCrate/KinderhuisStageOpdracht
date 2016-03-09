using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public interface IKamerControleOpdrachtRepository
    {
        IQueryable<KamerControleOpdracht> FindAll();
        KamerControleOpdracht FindById(int id);
        KamerControleOpdracht FindByName(string name);
    }
}
