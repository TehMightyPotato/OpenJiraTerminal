using System;
using System.Collections.Generic;
using System.Text;

namespace JsonObjects
{
    enum JsonObjectType
    {
        AddUserResponse,GetProjectResponse
    }
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

    public class CreateProjectJson
    {
        public CreateProjectJson(string key, string name, string lead)
        {
            this.key = key;
            this.name = name;
            this.lead = lead;
        }
        public string key { get; set; }
        public string name { get; set; }
        public string lead { get; set; }
    }

    public class GetProjectsResponseJson
    {
        public string expand { get; set; }
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string projectTypeKey { get; set; }
    }

    public class AssignProjectRoleJson
    {
        public List<string> user { get; set; }
        public AssignProjectRoleJson()
        {
            user = new List<string>();
        }
    }
}
