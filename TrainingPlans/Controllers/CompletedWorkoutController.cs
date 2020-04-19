using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Services;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Controllers
{
    [Route("api/user/{userId}/completed")]
    [ApiController]
    public class CompletedWorkoutController : ControllerBase
    {
        private readonly ICompletedWorkoutService _completedWorkoutService;
        public CompletedWorkoutController(ICompletedWorkoutService completedWorkoutService)
        {
            _completedWorkoutService = completedWorkoutService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CompleteUnplannedWorkout([FromRoute] int userId, [FromBody] CompletedWorkoutVM workout)
        {
            var result = await _completedWorkoutService.CompleteUnplannedWorkout(userId, workout);
            return Ok(result);
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompletePlannedWorkout([FromRoute] int userId, [FromBody] CompletedWorkoutVM workout)
        {
            var result = await _completedWorkoutService.CompletePlannedWorkout(userId, workout);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromRoute] int userId, [FromQuery] string from, [FromQuery] string to,
            [FromQuery] bool includeReps = false)
        {
            var history = await _completedWorkoutService.GetCompletedWorkoutsInDateRange(userId, from, to, includeReps);
            return Ok(history);
        }

        [HttpGet("{workoutId}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int workoutId, [FromQuery] bool includeReps = false)
        {
            var workout = await _completedWorkoutService.GetCompletedWorkout(userId, workoutId, includeReps);
            if (workout is null)
                return NotFound();
            return Ok(workout);
        }

        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int userId, [FromRoute] int workoutId)
        {
            var result = await _completedWorkoutService.DeleteCompletedWorkout(userId, workoutId);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{workoutId}")]
        public async Task<IActionResult> UpdateWorkout([FromRoute] int userId, [FromRoute] int workoutId, [FromBody] CompletedWorkoutVM updatedWorkout)
        {
            var result = await _completedWorkoutService.UpdateCompletedWorkout(userId, workoutId, updatedWorkout);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }
    }
}
