using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiConsume.Models;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> LoginAsync(string URL, User obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            if (request != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> RegisterAsync(string URL, User obj)
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
