using octo.Domain.Model;

namespace akctive.Presentation.Interface
{
    public interface IAkctivityServices
    {
        Task<IEnumerable<Akctivity>> GetAllActivitiesAsync();
        Task<IEnumerable<Akctivity>> GetActivitiesForUserAsync(string userId);
        Task AddActivityAsync(Akctivity activity);
    }

    public interface IHeartRateRepository
    {
        Task<IEnumerable<HeartRate>> GetAllHeartRatesAsync();
        Task<IEnumerable<HeartRate>> GetHeartRatesForUserAsync(string userId);
        Task AddHeartRateAsync(HeartRate heartRate);
    }

    public interface IHealthMetricRepository
    {
        Task<HealthMetric> GetHealthMetricForUserAsync(string userId);
        Task AddHealthMetricAsync(HealthMetric healthMetric);
    }
}
