
namespace Carbon.Http
{
    public interface IClient
    {
        string BaseUrl { get; set; }

        /// <summary>
        /// Makes an http "Get" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <returns>the resource of type "T"</returns>
        T Get<T>(string url);
        /// <summary>
        /// Makes an http "Get" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <returns>the resource as json</returns>
        string Get(string url);
        /// <summary>
        /// Makes a http "POST" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource of type "T"</returns>
        T Post<T>(string url, object data);
        /// <summary>
        /// Makes a http "POST" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource as json</returns>
        string Post(string url, object data);
        /// <summary>
        /// Makes a http "Put" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to Put</param>
        /// <returns>the resource of type "T"</returns>
        T Put<T>(string url, object data);
        /// <summary>
        /// Makes a http "Put" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to Put</param>
        /// <returns>the resource as json</returns>
        string Put(string url, object data);
        /// <summary>
        /// Makes a http "Remove" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to Remove</param>
        /// <returns>the resource of type "T"</returns>
        T Remove<T>(string url, object data);
        /// <summary>
        /// Makes a http "Remove" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to Remove</param>
        /// <returns>the resource as json</returns>
        string Remove(string url, object data);
    }
}
