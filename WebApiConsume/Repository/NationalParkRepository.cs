using System.Net.Http;
using WebApiConsume.Models;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NationalParkRepository(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClientFactory = httpClient;
        }
    }
}
