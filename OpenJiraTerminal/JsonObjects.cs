using System;
using System.Collections.Generic;
using System.Text;

namespace JsonObjects
{
    public class CreateUserJson
    {
        public CreateUserJson(string name, string password, string email, string displayName, string notification = "false")
        {
            this.name = name;
            this.password = password;
            this.emailAddress = email;
            this.displayName = displayName;
            this.notification = notification;
        }
        public string name { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public string notification { get; set; }
    }
    public class CreateUserResponseJson
    {
        public string self { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
        public string locale { get; set; }
        public string expand { get; set; }
    }
}
