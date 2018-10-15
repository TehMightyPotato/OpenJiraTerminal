using System;
using System.Collections.Generic;
using System.Text;

namespace JsonObjects
{
    public class CreateUserJson
    {
        public string name { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public string notification { get; set; }
    }
}
