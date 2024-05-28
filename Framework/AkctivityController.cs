using akctive.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using octo.Domain.Model;

namespace akctive.Framework
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkctivityController : ControllerBase
    {
        private readonly AkctivityService _akctivityService;

        public AkctivityController(AkctivityService akctivityService)
        {
            _akctivityService = akctivityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            var activities = await _akctivityService.GetAllActivitiesAsync();
            return Ok(activities);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetActivitiesForUser(string userId)
        {
            var activities = await _akctivityService.GetActivitiesForUserAsync(userId);
            return Ok(activities);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] Akctivity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _akctivityService.AddActivityAsync(activity);
            return Ok();
        }

        [HttpGet("heart-rate")]
        public async Task<IActionResult> GetAllHeartRates()
        {
            var heartRates = await _akctivityService.GetAllHeartRatesAsync();
            return Ok(heartRates);
        }

        [HttpGet("heart-rate/{userId}")]
        public async Task<IActionResult> GetHeartRatesForUser(string userId)
        {
            var heartRates = await _akctivityService.GetHeartRatesForUserAsync(userId);
            return Ok(heartRates);
        }

        [HttpGet("health-metric/{userId}")]
        public async Task<IActionResult> GetHealthMetricForUser(string userId)
        {
            var healthMetric = await _akctivityService.GetHealthMetricForUserAsync(userId);
            return Ok(healthMetric);
        }

        [HttpPost("health-metric")]
        public async Task<IActionResult> AddHealthMetric([FromBody] HealthMetric healthMetric)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _akctivityService.AddHealthMetricAsync(healthMetric);
            return Ok();
        }
    }
}
