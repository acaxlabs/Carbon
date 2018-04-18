using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.CosmosDB
{
    public interface IDocSet
    {
        void init(DocumentClient client, string database, string collection);
    }
}
