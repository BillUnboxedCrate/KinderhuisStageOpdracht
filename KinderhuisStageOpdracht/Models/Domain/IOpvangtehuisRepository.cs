using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public interface IOpvangtehuisRepository
    {
        IQueryable<Opvangtehuis> FindAll();
        Opvangtehuis FindById(int id);
        Opvangtehuis FindByName(string name);
    }
}