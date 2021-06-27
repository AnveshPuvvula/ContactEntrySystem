# ContactEntrySystem
A contact entry system with CRUD operations on the contact collection.

Requirements:
- .NET framework v4.7.2.
- Visual Studio (preferably VS2019).

Database Specific Requirements:
- Create a directory ‘c:/Temp’. The application uses this directory to create a LiteDB repository. 
- The path can be changed by changing the ‘dbpath’ key in the web.config file in the ContactEntrySystem project and also the ‘dbpath’ key in the app.config file in the         	 ContactsUnitTest project.

Steps to run the application: 
- Download the code, extract the contents into a folder and open the ‘ContactEntrySystem’ solution file in visual studio ( preferably VS2019).
- The application is already configured to use IIS Express. If using IIS follow step- 3.
- If using Local IIS - Configure IIS and setup [repo location] -> ContactEntrySystem-> ContactEntrySystem as the physical address to host the project. In visual studio right       click on project and go to properties -> web. Under servers select Local IIS-> Specify the project URL previously configured on IIS and click ‘create virtual directory’.
- Build the solution and run it. The homepage will be displayed with basic info about the project. 
- Open postman and perform necessary operations. 

Operations:
- Create (POST) -   https://localhost:44321/contacts/create
                    
                    Body :
                    
                    
                    {
                      "name": {
                        "first": "Harold",
                        "middle": "Francis",
                        "last": "Gilkey"
                      },
                      "address": {
                        "street": "8360 High Autumn Row",
                        "city": "Cannon",
                        "state": "Delaware",
                        "zip": "19797"
                      },
                      "phone": [
                        {
                          "number": "302-611-9148",
                          "type": "home"
                        },
                        {
                          "number": "302-532-9427",
                          "type": "mobile"
                        }
                      ],
                      "email": "harold.gilkey@yahoo.com"
                    }

- Update (PUT) -  https://localhost:44321/contacts/UpdateContact
                  
                  Body:
                  
                  
                  {
                          "id": 1,
                          "name": {
                              "first": "HaroldUpdate",
                              "middle": "Francis Update",
                              "last": "Gilkey"
                          },
                          "address": {
                              "street": "8360 High Autumn Row Update",
                              "city": "Cannon",
                              "state": "Delaware",
                              "zip": "19799"
                          },
                          "phone": [
                             {
                              "number": "302-600-9148",
                              "type": "home"
                              },
                              {
                              "number": "302-500-9427",
                              "type": "mobile"
                              }
                                  ],
                          "email": "a.p@yahoo.com"
                      }

- GetContactById (GET) - https://localhost:44321/contacts/GetContactById?id=1
- GetAllContacts (GET) - https://localhost:44321/contacts/GetAllContacts
- Delete (DELETE) - https://localhost:44321/contacts/DeleteContact?id=1
- GetCallList (GET) - https://localhost:44321/contacts/GetCallList?type=home
