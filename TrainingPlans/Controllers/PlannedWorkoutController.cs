using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Services;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Controllers
{
    [Route("api/user/{userId}/plan")]
    [ApiController]
    public class PlannedWorkoutController : ControllerBase
    {
        private readonly IPlannedWorkoutService _plannedWorkoutService;
        public PlannedWorkoutController(IPlannedWorkoutService plannedWorkoutService)
        {
            _plannedWorkoutService = plannedWorkoutService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateWorkout([FromRoute] int userId, [FromBody] PlannedWorkoutVM workout)
        {
            var resultId = await _plannedWorkoutService.Create(workout, userId);
            return Ok(resultId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromRoute] int userId, [FromQuery] string from, [FromQuery] string to,
            [FromQuery] bool includeReps = false)
        {
            var plan = await _plannedWorkoutService.GetInDateRange(from, to, userId, includeReps);
            return Ok(plan);
        }

        [HttpGet("/{workoutId}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int workoutId, [FromQuery] bool includeReps = false)
        {
            var workout = await _plannedWorkoutService.GetSingle(userId, workoutId, includeReps);
            if (workout is null)
                return NotFound();
            return Ok(workout);
        }

        [HttpDelete("/{workoutId}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int userId, [FromRoute] int workoutId)
        {
            var result = await _plannedWorkoutService.DeleteWorkout(userId, workoutId);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }
    }
}
