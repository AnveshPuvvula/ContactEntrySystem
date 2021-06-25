using System;
using System.Collections.Generic;
using System.Configuration;
using ContactEntrySystem.Controllers;
using ContactEntrySystem.DataAccess;
using ContactEntrySystem.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ContactsTest
{
    [TestFixture,NonParallelizable]
    public class ContactDBTest
    {
        // Test objects declaration.
        private Contact contactTest1;
        private Contact contactTest2;
        private Contact contactTest3;
        private Contact contactUpdate;
        private Name nameTest1;
        private Name nameTest2;
        private Name nameTest3;
        private Name nameUpdate;
        private List<Contact> contactListTest = new List<Contact>();

        //Declaring and initializing the path where the database for unit tests are stored.
        private static string dbpath = "C:/temp/ContactsTest.db";
        
        //Creating and assigning the controller object used for the test cases.
        private IContactDB contactDB = new ContactDB();

     

        //Setup method to set the test object values for running the tests.
        [SetUp]
        public void Setup()
        {
            // Address for all test cases.
            Address addressTest = new Address();
            addressTest.city = "CityTest";
            addressTest.state = "StateTest";
            addressTest.street = "StreetTest";
            addressTest.zip = "ZipTest";

            // Test case 1------------------------------------------------------------------------------------------------------------------------
            nameTest1 = new Name { first = "FirstNameTest1", last = "LastNameTest1", middle = "MiddleNameTest1" };

            contactTest1 = new Contact { address = addressTest, name = nameTest1, id = 1234L, email = "EmailTest1" };

            List<Phone> phoneListTest1 = new();
            Phone phoneTest1 = new Phone
            {
                ID = 1234,
                number = "PhoneNumberTest1",
                type = PhoneType.home
            };

            phoneListTest1.Add(phoneTest1);
            contactTest1.phone = phoneListTest1;

            // Adding test case 1 to contact list.
            contactListTest.Add(contactTest1);
            
            // Test case 2 ------------------------------------------------------------------------------------------------------------------------
            nameTest2 = new Name { first = "FirstNameTest2", last = "LastNameTest2", middle = "MiddleNameTest2" };

            contactTest2 = new Contact { address = addressTest, name = nameTest2, id = 5678L, email = "EmailTest2" };


            List<Phone> phoneListTest2 = new();
            Phone phoneTest2 = new Phone
            {
                //phoneTest1.(contactTest2);
                ID = 1234,
                number = "PhoneNumberTest2",
                type = PhoneType.work
            };

            phoneListTest2.Add(phoneTest2);
            contactTest2.phone = phoneListTest2;

            // Adding test case 2 to contact list.
            //contactListTest.Add(contactTest2);


            //Update/Delete Test Cases
            // Test case 3 ------------------------------------------------------------------------------------------------------------------------
            nameTest3 = new Name { first = "FirstNameTest3", last = "LastNameTest3", middle = "MiddleNameTest3" };

            contactTest3 = new Contact { address = addressTest, name = nameTest3, id = 9012L, email = "EmailTest3" };


            List<Phone> phoneListTest3 = new();
            Phone phoneTest3 = new Phone
            {
                //phoneTest1.(contactTest2);
                ID = 8709,
                number = "TEST_NUMBER_3",
                type = PhoneType.work
            };

            phoneListTest3.Add(phoneTest3);
            contactTest3.phone = phoneListTest3;

            //Updated Name to be passed to test update contact.
            nameUpdate = new Name { first = "UpdateFirstName3", last = "UpdateLastName3", middle = "UpdateMiddleName3" };

            //contact to be paased to test update contact.
            contactUpdate = new Contact { address = addressTest, name = nameUpdate, id = 9012L, email = "EmailTest3" };

        }

        [OneTimeSetUp]
        public void Init()
        {
            //Clean database after each execution of test sets to avoid errors.          
            contactDB.InitializeDB(dbpath);
            contactDB.DropDatabase(dbpath, "contacts");
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            //Clean database after each execution of test sets to avoid errors.
            contactDB.DropDatabase(dbpath, "contacts");
        }

        [Test]
        public void TestCreateContact()
        {
            Contact result = contactDB.Create(contactTest1);
            string actual = JsonConvert.SerializeObject(result);
            string expected = JsonConvert.SerializeObject(contactTest1);
            Assert.AreEqual(expected, actual);
            
        }

        [Test, NonParallelizable]
        public void TestGetAllContacts()
        {
            
            List<Contact> result = contactDB.GetAllContacts();
            string actual = JsonConvert.SerializeObject(result[0]);
            string expected = JsonConvert.SerializeObject(contactListTest[0]);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGetContactById()
        {
            Contact result = contactDB.GetContactById(contactTest1.id);
            string actual = JsonConvert.SerializeObject(result);
            string expected = JsonConvert.SerializeObject(contactTest1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdateContact()
        {
            Contact resultCreate = contactDB.Create(contactTest3);

            contactDB.UpdateContact(contactUpdate);
            Contact result = contactDB.GetContactById(contactUpdate.id);
            string actual = result.name.first;
            string expected = contactUpdate.name.first;
            string deleteResult = contactDB.DeleteContact(contactTest3.id);
            Assert.AreEqual(expected, actual);


        }

        [Test]
        public void TestGetCallList()
        {
            List<Contact> result = contactDB.GetCallList();
            string actual = result[0].phone[0].number;
            string expected = contactTest1.phone[0].number;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeleteContact()
        {

            Contact resultCreate = contactDB.Create(contactTest3);

            contactDB.DeleteContact(contactTest3.id);
            Contact result = contactDB.GetContactById(contactTest3.id);

            Assert.IsNull(result);
        }

    }
}
