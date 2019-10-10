using Carbon.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Carbon.Net
{
    /// <summary>
    /// A System.Net.WebClient wrapper that provides common HTTP methods for sending and receiving data from a resource.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        /// Gets or sets the base URI for requests made by a System.Net.System.Net.WebClient.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets a collection of header name/value pairs associated with the request.
        /// Default header "Content-type" is set to "application/json"
        /// </summary>
        public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection()
        {
            { "Content-type", "application/json" }
        };

        /// <summary>
        /// Default Json Serializer Settings, can be overriden in constructor
        /// </summary>
        private JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public string LastRequestJson { get; set; }
        public HttpWebResponse LastErrorResponse { get; set; }

        /// <summary>
        /// ctor allows for overwriting json serailzer settings
        /// </summary>
        public Client(JsonSerializerSettings _settings)
        {
            this._settings = _settings;
        }

        /// <summary>
        /// default ctor
        /// </summary>
        public Client() { }

        #region Synchronous Methods
        /// <summary>
        /// Makes a http "Get" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <returns>the resource of type "T"</returns>
        public T Get<T>(string url)
        {
            try
            {
                string json = GetJson(url);
                T response = FromJson<T>(json);
                return response;
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }
        /// <summary>
        /// Makes a http "Get" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <returns>the resource as json</returns>
        public string Get(string url)
        {
            try
            {
                return GetJson(url);
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }
        /// <summary>
        /// Makes a http "POST" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource of type "T"</returns>
        public T Post<T>(string url, object data)
        {
            try
            {
                string jsonRequest = ToJson(data);
                string jsonResponse = PostJson(url, jsonRequest);
                T response = FromJson<T>(jsonResponse);
                return response;
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }
        /// <summary>
        /// Makes a http "POST" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource as json</returns>
        public string Post(string url, object data)
        {
            try
            {
                string jsonRequest = ToJson(data);
                return PostJson(url, jsonRequest);
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }

        /// <summary>
        /// Makes a http "PUT" request for a resource
        /// </summary>
        /// <typeparam name="T">the type the response will be deserialized to from json</typeparam>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource of type "T"</returns>
        public T Put<T>(string url, object data)
        {
            try
            {
                string jsonRequest = ToJson(data);
                string jsonResponse = PutJson(url, jsonRequest);
                T response = FromJson<T>(jsonResponse);
                return response;
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }

        /// <summary>
        /// Makes a http "PUT" request for a resource
        /// </summary>
        /// <param name="url">the resource's endpoint</param>
        /// <param name="data">the data to post</param>
        /// <returns>the resource as json</returns>
        public string Put(string url, object data)
        {
            try
            {
                string jsonRequest = ToJson(data);
                return PutJson(url, jsonRequest);
            }
            catch (WebException exception)
            {
                string responseText = ParseWebException(exception);
                throw new WebException(responseText);
            }
        }
        #endregion

        #region Private Methods


        private string GetJson(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                if (!string.IsNullOrEmpty(BaseUrl)) client.BaseAddress = BaseUrl;
                client.Headers.Add(Headers);
                return client.DownloadString(url);
            }
        }
        
        private string PostJson(string url, string json)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                if (!string.IsNullOrEmpty(BaseUrl)) client.BaseAddress = BaseUrl;
                client.Headers.Add(Headers);
                client.Encoding = System.Text.Encoding.UTF8;
                return client.UploadString(url, json);
            }
        }

        private string PutJson(string url, string json)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                if (!string.IsNullOrEmpty(BaseUrl)) client.BaseAddress = BaseUrl;
                client.Headers.Add(Headers);
                client.Encoding = System.Text.Encoding.UTF8;
                return client.UploadString(url, "PUT", json);
            }
        }
        
        private string ToJson(object data)
        {
            LastRequestJson = JsonConvert.SerializeObject(data, _settings);
            return LastRequestJson;
        }

        private T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        private string ParseWebException(WebException exception)
        {
            string responseText = string.Empty;
            LastErrorResponse = (HttpWebResponse)exception.Response;
            var responseStream = exception.Response?.GetResponseStream();

            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            if(responseText.Length > 512) //Can't be over 512 in length will cause an exception
            {
                responseText = responseText.Substring(0, 500) + "...";
            }
            return responseText;
        }

        #endregion

        #region Not Implemented
        public T Remove<T>(string url, object data)
        {
            throw new NotImplementedException();
        }
        
        public string Remove(string url, object data)
        {
            throw new NotImplementedException();
        }
#endregion
    }
}
