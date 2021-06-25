using ContactEntrySystem.DataAccess;
using ContactEntrySystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactEntrySystem.Controllers
{
    public class ContactsController : ApiController
    {
        private IContactDB _DbContext;

        //Constructor used to inject dependency using the microsoft unity container.
        public ContactsController(IContactDB iContactDB, string path = null)
        {
            // initializing the data access class to perform required actions on the database.
            _DbContext = iContactDB;
            
            // setting the dbpath property in the data access class which is used to create and connect to the database.
            if (!(ConfigurationManager.AppSettings["dbpath"] == null))
                _DbContext.InitializeDB(ConfigurationManager.AppSettings["dbpath"]);
            else
                _DbContext.InitializeDB(path);
           
        }

        [HttpPost]
        public Contact Create(Contact contact)
        {
            return _DbContext.Create(contact);
        }

        [HttpGet]
        public List<Contact> GetAllContacts()
        {
            return _DbContext.GetAllContacts();
        }

        [HttpPut]
        public string UpdateContact(Contact contact)
        {
            return _DbContext.UpdateContact(contact);
        }

        [HttpGet]
        public Contact GetContactById(long id)
        {
            return _DbContext.GetContactById(id);
        }

        [HttpDelete]
        public string DeleteContact(long id)
        {
            return _DbContext.DeleteContact(id);
        }

        [HttpGet]
        public List<CallList> GetCallList(PhoneType type)
        {
            List<Contact> contactList = _DbContext.GetCallList();
            List<CallList> returnList = new List<CallList>();

            // Using linq to retrive only contacts with type 'home' and ordering them by last name then first name.
            returnList = contactList.Where(x => x.phone.Any(p => p.type.Equals(type)))
                              .Select(
                                       x => new CallList()
                                       {
                                           name = x.name,
                                           number = x.phone.Where(p => p.type.Equals(type)).FirstOrDefault().number
                                       }
                                    ).OrderBy(l => l.name.last).ThenBy(f => f.name.first).ToList();

            return returnList;
        }
    }
}
