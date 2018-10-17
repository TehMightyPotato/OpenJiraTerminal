using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

public enum RequestType { POST,GET }
public enum Api { addUser}

namespace OpenJiraTerminal
{
    class ConnectionHandler
    {
        private string loginCredentials;

        public string url;

        private string query;

        public HttpClient httpClient;

        public ConnectionHandler(string url, string loginCredentials)
        {
            this.url = PrepareUrl(url);
            this.loginCredentials = loginCredentials;
            GetHttpClient();
        }

        public void UpdateVariables(string url, string loginCredentials)
        {
            this.loginCredentials = loginCredentials;
            GetHttpClient();
        }

        private HttpClient GetHttpClient()
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", BuildAuthHeader());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " | Please check your URL for typos");
                return null;
            }
        }

        private string BuildAuthHeader()
        {
            var plainbytes = Encoding.UTF8.GetBytes(loginCredentials);
            return Convert.ToBase64String(plainbytes);
        }

        public string QueryServer(RequestType type ,string query, string jsonObject = null)
        {

            if(type == RequestType.GET)
            {
                try
                {
                    var response = httpClient.GetAsync(query).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        return result;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message + " | An error occured while querying the server");
                    return null;
                }
            }
            else
            {
                try
                {
                    var response = httpClient.PostAsync(query, new StringContent(jsonObject,Encoding.UTF8,"application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        return result;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message + " | failed to query server");
                    return null;
                }
            }

            
        }

        #region url prep
        private string PrepareUrl(string url)
        {
            if (url.StartsWith("http://"))
            {
                return url;
            }
            else if (url.StartsWith("https://"))
            {
                return "http://" + url.Substring(8);
            }
            else
            {
                return "http://" + url;
            }
        }
        #endregion
        #region test function
        public bool TestConnection()
        {
            try
            {
                Console.WriteLine("Querying Server, please wait...");
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
