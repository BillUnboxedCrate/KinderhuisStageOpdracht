using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderhuisStageOpdracht.Models.Domain
{
    public interface IGebruikerRepository
    {
        //Telt voor alle gebruikers
        IQueryable<Gebruiker> FindAll();
        Gebruiker FindById(int id);
        Gebruiker FindByUsername(string username);

        //Admins
        IQueryable<Admin> FindAllAdmins();

        //Opvoeders
        IQueryable<Opvoeder> FindAllOpvoeders();

        //Clienten
        IQueryable<Client> FindAllClients();
        

        void SaveChanges();
    }
}
