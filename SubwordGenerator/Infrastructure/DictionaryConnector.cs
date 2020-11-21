using System;
using Newtonsoft.Json.Linq;

namespace SubwordGenerator.Infrastructure
{
    public class DictionaryConnector
    { 
        public string SearchDefinition(string word)
        {
            try
            {
                var webRequest = System.Net.WebRequest.Create("https://api.dictionaryapi.dev/api/v2/entries/en/" + word);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.ContentType = "application/json";
                    webRequest.Timeout = 10000;
                }
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var responseJson = sr.ReadToEnd();
                        dynamic response = JArray.Parse(responseJson);
                        string definition = response.First.meanings.First.definitions.First.definition;
                        return definition;
                    }
                }
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
