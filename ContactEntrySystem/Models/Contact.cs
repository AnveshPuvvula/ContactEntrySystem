using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactEntrySystem.Models
{
    public class Contact
    {
        public long id { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }
        public List<Phone> phone { get; set; }
        public string email { get; set; }
    }
}