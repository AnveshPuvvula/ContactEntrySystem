using ContactEntrySystem.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ContactEntrySystem.DataAccess
{
    //ContactDB class acts as the main layer for data access. It implements the IContactDB interface.
    public class ContactDB : IContactDB
    {
        // Path where the database need to be stored. (Defined as a key in web.config).
        private static string DbPath;//AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["dbpath"]; 

        //sets the DBPath field.
        public void InitializeDB(string path)
        {
            DbPath = path;
        }

        //Creates a contact in the database.
        public Contact Create(Contact contact)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                contactCollection.Insert(contact);
                return contact;
            }
        }

        //Gets all contacts from the database.
        public List<Contact> GetAllContacts()
        {
            using (var db = new LiteDatabase(DbPath))
            {
                List<Contact> contactList = new List<Contact>();
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                var results = contactCollection.FindAll();
                foreach (Contact contact in results)
                {
                    contactList.Add(contact);
                }
                return contactList;
            }
        }

        //Updates a contact in the database by id.
        public string UpdateContact(Contact contact)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                contactCollection.Update(contact);

               

                return $"Successfully updated contact.";
            }
        }

        //Gets a contact from the database by id.
        public Contact GetContactById(long id)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                Contact contact = new Contact();
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                contact = contactCollection.FindById(id);
                return contact;
            }
        }

        //Deletes a contact from the database by id.
        public string DeleteContact(long id)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                contactCollection.Delete(id);

                return $"Successfully deleted contact with id : {id}";
            }
        }

        //Get the contacts from the dabase to generate the calllist.
        public List<Contact> GetCallList()
        {
            using (var db = new LiteDatabase(DbPath))
            {
                List<Contact> contactList = new List<Contact>();
                // Get contact collection
                var contactCollection = db.GetCollection<Contact>("contacts");
                var results = contactCollection.FindAll();
  
                foreach (Contact contact in results)
                {
                    contactList.Add(contact);
                }
               
                return contactList;
            }
        }

        //Drops the collection with a particular name in the database.
        public void DropDatabase(string path, string name)
        {
            using (var db = new LiteDatabase(path))
            {                     
                if(db.CollectionExists(name))
                    db.DropCollection(name);
            }
        }
    }
}