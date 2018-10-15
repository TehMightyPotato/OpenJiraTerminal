using System;
using System.Collections.Generic;
using System.Text;

namespace OpenJiraTerminal
{
    class LoginManager
    {
        private string login;
        private string base64passwrd;

        public LoginManager(string login, string password)
        {
            this.login = login;
            this.base64passwrd = password;
        }

        public string GetLoginCredentials()
        {
            return login + ":" + base64passwrd;
        }
    }
}
