using ContactEntrySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactEntrySystem.DataAccess
{
    public interface IContactDB
    {
        void InitializeDB(string path);
        Contact Create(Contact contact);
        List<Contact> GetAllContacts();
        string UpdateContact(Contact contact);
        Contact GetContactById(long id);
        string DeleteContact(long id);
        List<Contact> GetCallList();
        void DropDatabase(string path, string name);
    }
}
