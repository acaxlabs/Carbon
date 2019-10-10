using Carbon.Http;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.FirebaseClient
{
    public class CarbonFire : IClient
    {
        static IFirebaseConfig Config = new FirebaseConfig
        {
            AuthSecret = ConfigurationManager.AppSettings["Firebase.AuthSecret"],
            BasePath = ConfigurationManager.AppSettings["Firebase.BasePath"]

        };

        IFirebaseClient client = new FireSharp.FirebaseClient(Config);
        public string BaseUrl { get; set; } = ConfigurationManager.AppSettings["Firebase.BasePath"];

        public T Get<T>(string url)
        {
            FirebaseResponse response = client.Get(url);
            return response.ResultAs<T>();
        }

        public string Get(string url)
        {
            FirebaseResponse response = client.Get(url);
            return response.Body;
        }

        public T Post<T>(string url, object data)
        {
            SetResponse response = client.Set(url, data);
            return response.ResultAs<T>();
        }

        public string Post(string url, object data)
        {
            SetResponse response = client.Set(url, data);
            return response.Body;
        }

        public T Put<T>(string url, object data)
        {
            FirebaseResponse response = client.Update(url, data);
            return response.ResultAs<T>();
        }

        public string Put(string url, object data)
        {
            FirebaseResponse response = client.Update(url, data);
            return response.Body;
        }

        public T Remove<T>(string url, object data)
        {
            FirebaseResponse response = client.Delete(url);
            return response.ResultAs<T>();
        }

        public string Remove(string url, object data)
        {
            FirebaseResponse response = client.Delete(url);
            return response.Body;
        }

        public T Push<T>(string url, object data)
        {
            PushResponse response = client.Push(url, data);
            return response.ResultAs<T>();
        }

        public string Push(string url, object data)
        {
            PushResponse response = client.Push(url, data);
            return response.Body;
        }
    }
}
