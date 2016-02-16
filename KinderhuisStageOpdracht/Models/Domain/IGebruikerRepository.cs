using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    interface IGebruikerRepository
    {
        //Telt voor alle gebruikers
        IQueryable<Gebruiker> FindAll();
        Gebruiker FindById(int id);

        //Admins
        IQueryable<Admin> FindAllAdmins();

        //Opvoeders
        IQueryable<Opvoeder> FindAllOpvoeders();

        //Clienten
        IQueryable<Client> FindAllClients();
        

        void SaveChanges();
    }
}
