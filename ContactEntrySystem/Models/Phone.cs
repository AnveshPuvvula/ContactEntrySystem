using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactEntrySystem.Models
{
        public class Phone
        {
            [JsonIgnore]
            public int ID { get; set; }
            public string number { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public PhoneType type { get; set; }
        }
 }