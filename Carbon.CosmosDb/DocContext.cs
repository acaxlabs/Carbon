using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.CosmosDB
{
    public abstract class DocContext
    {
        protected DocumentClient client;

        public DocContext()
        {
            string name = this.GetType().Name;
            string serviceEndpoint = ConfigurationManager.AppSettings[$"{name}.ServiceEndpoint"];
            if (string.IsNullOrEmpty(serviceEndpoint)) throw new Exception($"{name}.ServiceEndpoint is not in the appSettings.");
            string authKey = ConfigurationManager.AppSettings[$"{name}.AuthKey"];
            if (string.IsNullOrEmpty(authKey)) throw new Exception($"{name}.AuthKey is not in the appSettings.");
            string database = ConfigurationManager.AppSettings[$"{name}.Database"];
            if (string.IsNullOrEmpty(database)) throw new Exception($"{name}.Database is not in the appSettings.");
            init(serviceEndpoint, authKey, database);
        }

        public DocContext(string serviceEndpoint, string authKey, string database)
        {
            init(serviceEndpoint, authKey, database);
        }

        private void init(string serviceEndpoint, string authKey, string database)
        {
            client = new DocumentClient(new Uri(serviceEndpoint), authKey);
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!typeof(IDocSet).IsAssignableFrom(prop.PropertyType)) continue;
                IDocSet set = (IDocSet)Activator.CreateInstance(prop.PropertyType);
                set.init(client, database, prop.Name.ToLower());
                prop.SetValue(this, set);
            }
        }
    }
}
