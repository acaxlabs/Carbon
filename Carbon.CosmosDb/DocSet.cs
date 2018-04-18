using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.CosmosDB
{
    public class DocSet<T> : IDocSet
    {
        private DocumentClient client;
        private string database;
        private string collection;

        public T First(Expression<Func<T, bool>> predicate)
        {
            return client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(database, collection), new FeedOptions { EnableCrossPartitionQuery = true }).Where(predicate).ToList().First();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(database, collection), new FeedOptions { EnableCrossPartitionQuery = true }).Where(predicate).ToList().FirstOrDefault();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(database, collection), new FeedOptions { EnableCrossPartitionQuery = true }).Where(predicate);
        }

        public async Task UpsertAsync(T item)
        {
            await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(database, collection), item);
        }

        public void Upsert(T item)
        {
            Task task = UpsertAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            Uri uri = UriFactory.CreateDocumentUri(database, collection, id.ToString());
            PartitionKey key = new PartitionKey(id.ToString());
            await client.DeleteDocumentAsync(uri, new RequestOptions() { PartitionKey = key });
        }

        public void Delete(Guid id)
        {
            Task task = DeleteAsync(id);
        }

        public void init(DocumentClient client, string database, string collection)
        {
            this.client = client;
            this.database = database;
            this.collection = collection;
        }
    }

}
