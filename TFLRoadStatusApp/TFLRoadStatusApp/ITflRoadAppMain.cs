using System.Threading.Tasks;

namespace TflRoadStatusApp
{
    public interface ITflRoadAppMain
    {
        public Task<int> RunAsync(string[] args);
    }   
}
