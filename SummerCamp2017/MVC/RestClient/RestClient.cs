using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestClient
{
    public class RestClient<T>
    {
        public string WebServiceUrl = "";

        public List<T> Get()
        {
            var httpClient = new HttpClient();
            var json = httpClient.GetStringAsync(WebServiceUrl).Result;
            var obj = JsonConvert.DeserializeObject<List<T>>(json);

            return obj;
        }

        public T GetById(int id)
        {
            var httpClient = new HttpClient();

            var json = httpClient.GetStringAsync(WebServiceUrl + id).Result;

            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public List<T> GetByAnnouncement(int id)
        {
            var httpClient = new HttpClient();

            var json = httpClient.GetStringAsync(WebServiceUrl + id).Result;

            var obj = JsonConvert.DeserializeObject<List<T>>(json);

            return obj;
        }

        public bool Post(T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = httpClient.PostAsync(WebServiceUrl, httpContent).Result;

            return result.IsSuccessStatusCode;
        }

        public bool Put(int id, T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = httpClient.PutAsync(WebServiceUrl + id, httpContent).Result;

            return result.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.DeleteAsync(WebServiceUrl + id).Result;

            return response.IsSuccessStatusCode;
        }

        public HttpResponseMessage Close(int id, string email)
        {
            var httpClient = new HttpClient();
            // var content = new FormUrlEncodedContent(new[] {
            //    new KeyValuePair<string, string>("", email)
            //}); 
            var response = httpClient.PostAsJsonAsync(WebServiceUrl + id, email).Result;

            return response;
        }

        public HttpResponseMessage Extend(int id, string email)
        {
            var httpClient = new HttpClient();
            //var content = new FormUrlEncodedContent(new[] {
            //    new KeyValuePair<string, string>("", email)
            //}); 
            var response = httpClient.PostAsJsonAsync(WebServiceUrl + id, email).Result;

            return response;
        }
    }
}
