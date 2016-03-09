using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public interface IKamerControleItemRepository
    {
        IQueryable<KamerControleItem> FindAll();
        KamerControleItem FindById(int id);
        KamerControleItem FindByName(string name);
    }
}
