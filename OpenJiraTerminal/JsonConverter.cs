using System;
using System.Collections.Generic;
using System.Text;
using JsonObjects;
using Newtonsoft.Json;

namespace OpenJiraTerminal
{
    class JsonConverter
    {
        public string BuildJsonFromObject(object obj)
        {
            if (obj == null) return null;
            return JsonConvert.SerializeObject(obj);
        }

        public object BuildObjectFromJson(string json)
        {
            if (json == null) return null;
            return null;
        }

        private object GetMatchingClassToJson(string json)
        {
            return null;
        }
    }
}
