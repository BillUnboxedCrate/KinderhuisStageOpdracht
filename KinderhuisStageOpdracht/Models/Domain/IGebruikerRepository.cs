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
        IQueryable<Gebruiker> FindAllAdmins();

        //Opvoeders
        IQueryable<Gebruiker> FindAllOpvoeders();

        //Clienten
        IQueryable<Gebruiker> FindAllClients();

        void AddOpvoeder(Opvoeder opvoeder);
        void AddClient(Client client);

        void UpdateGebruiker(Gebruiker gebruiker);
        void UpdateOpvoeder(Opvoeder opvoeder);
        void UpdateClient(Client client);

        void DeleteGebruiker(int id);

        void SaveChanges();
    }
}
