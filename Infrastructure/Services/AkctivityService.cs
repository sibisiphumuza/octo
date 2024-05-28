using akctive.Infrastructure.Repository;
using akctive.Presentation.Interface;
using octo.Domain.Model;

namespace akctive.Infrastructure.Services
{
    public class AkctivityService(AkctivityRepo akctivityRepository)
    {
        private readonly AkctivityRepo _akctivityRepository = akctivityRepository;

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
    }
}
