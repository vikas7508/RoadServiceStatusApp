using System.Net.Http;
using System.Threading.Tasks;

namespace TflRoadStatusApp.Helpers.Interfaces
{
    public interface ITflRoadService
    {
        public Task<HttpResponseMessage> Get(string uri);
    }
}
