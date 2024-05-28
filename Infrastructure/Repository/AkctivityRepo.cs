using akctive.Presentation.Interface;
using Microsoft.EntityFrameworkCore;
using octo.Domain.Model;

namespace akctive.Infrastructure.Repository
{
    public class AkctivityRepo : IAkctivityServices
    {
        private readonly DbContext _context;

        public AkctivityRepo(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Akctivity>> GetAllActivitiesAsync()
        {
            return await _context.Set<Akctivity>().ToListAsync();
        }

        public async Task<IEnumerable<Akctivity>> GetActivitiesForUserAsync(string userId)
        {
            return await _context.Set<Akctivity>()
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task AddActivityAsync(Akctivity activity)
        {
            await _context.Set<Akctivity>().AddAsync(activity);
            await _context.SaveChangesAsync();
        }
    }

    public class HeartRateRepository : IHeartRateRepository
    {
        private readonly DbContext _context;

        public HeartRateRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeartRate>> GetAllHeartRatesAsync()
        {
            return await _context.Set<HeartRate>().ToListAsync();
        }

        public async Task<IEnumerable<HeartRate>> GetHeartRatesForUserAsync(string userId)
        {
            return await _context.Set<HeartRate>()
                .Where(hr => hr.UserId == userId)
                .ToListAsync();
        }

        public async Task AddHeartRateAsync(HeartRate heartRate)
        {
            await _context.Set<HeartRate>().AddAsync(heartRate);
            await _context.SaveChangesAsync();
        }
    }

    public class HealthMetricRepository : IHealthMetricRepository
    {
        private readonly DbContext _context;

        public HealthMetricRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<HealthMetric> GetHealthMetricForUserAsync(string userId)
        {
            return await _context.Set<HealthMetric>()
                .SingleOrDefaultAsync(hm => hm.UserId == userId);
        }

        public async Task AddHealthMetricAsync(HealthMetric healthMetric)
        {
            await _context.Set<HealthMetric>().AddAsync(healthMetric);
            await _context.SaveChangesAsync();
        }
    }
}
