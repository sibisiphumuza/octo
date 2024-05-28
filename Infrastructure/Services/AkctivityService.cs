using akctive.Infrastructure.Repository;
using akctive.Presentation.Interface;
using octo.Domain.Model;

namespace akctive.Infrastructure.Services
{
    public class AkctivityService(AkctivityRepo akctivityRepository, IHeartRateRepository heartRateRepository, IHealthMetricRepository healthMetricRepository) : IAkctivityServices
    {
        private readonly AkctivityRepo _akctivityRepository = akctivityRepository;
        private readonly IHeartRateRepository _heartRateRepository = heartRateRepository;
        private readonly IHealthMetricRepository _healthMetricRepository = healthMetricRepository;

        public async Task<IEnumerable<Akctivity>> GetAllActivitiesAsync()
        {
            return await _akctivityRepository.GetAllActivitiesAsync();
        }
        public async Task<IEnumerable<Akctivity>> GetActivitiesForUserAsync(string userId)
        {
            return await _akctivityRepository.GetActivitiesForUserAsync(userId);
        }
        public async Task AddActivityAsync(Akctivity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            // Add your business logic/validation here if needed

            await _akctivityRepository.AddActivityAsync(activity);
        }
        public async Task<IEnumerable<HeartRate>> GetAllHeartRatesAsync()
        {
            return await _heartRateRepository.GetAllHeartRatesAsync();
        }
        public async Task<IEnumerable<HeartRate>> GetHeartRatesForUserAsync(string userId)
        {
            return await _heartRateRepository.GetHeartRatesForUserAsync(userId);
        }
        public async Task AddHeartRateAsync(HeartRate heartRate)
        {
            if (heartRate == null)
            {
                throw new ArgumentNullException(nameof(heartRate));
            }

            // Add your business logic/validation here if needed

            await _heartRateRepository.AddHeartRateAsync(heartRate);
        }
        public async Task<HealthMetric> GetHealthMetricForUserAsync(string userId)
        {
            return await _healthMetricRepository.GetHealthMetricForUserAsync(userId);
        }
        public async Task AddHealthMetricAsync(HealthMetric healthMetric)
        {
            if (healthMetric == null)
            {
                throw new ArgumentNullException(nameof(healthMetric));
            }

            // Add your business logic/validation here if needed

            await _healthMetricRepository.AddHealthMetricAsync(healthMetric);
        }
    }
}
