using System.Net.Http;
using WebApiConsume.Models;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Repository
{
    
    public class TrailsRepository : Repository<Trails>, ITrailsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailsRepository(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClientFactory = httpClient;
        }
    }
}
