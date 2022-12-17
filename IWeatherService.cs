using System.Threading.Tasks;

namespace AnnonceAPI.Services
{
    public interface IWeatherService
    {
        Task<string> GetLocalWeatherAsync(string location);
    }
}
