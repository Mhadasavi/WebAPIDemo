using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public async Task<bool> CreateAsync(string URL, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            if (request != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string URL, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, URL+id);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string URL)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URL );
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else return null;
        }

        public async Task<T> GetAsyn(string URL, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URL+id);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else return null;
        }

        public async Task<bool> UpdateAsync(string URL, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, URL);
            if (request != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
