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
    }
}
