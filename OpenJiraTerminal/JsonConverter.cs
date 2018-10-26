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

        public object BuildObjectFromJson(JsonObjectType type, string json)
        {
            if (json == null) return null;
            switch (type)
            {
                case JsonObjectType.AddUserResponse:
                    return JsonConvert.DeserializeObject<CreateUserResponseJson>(json);
                case JsonObjectType.GetProjectResponse:
                    return JsonConvert.DeserializeObject<List<GetProjectsResponseJson>>(json);
            }
            return null;
        }

        private object GetMatchingClassToJson(string json)
        {
            return null;
        }
    }
}
