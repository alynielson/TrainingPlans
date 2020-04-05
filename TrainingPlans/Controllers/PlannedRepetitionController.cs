using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Services;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Controllers
{
    [Route("api/user/{userId}/plan/{workoutId}/repetition")]
    [ApiController]
    public class PlannedRepetitionController : ControllerBase
    {
        private readonly IPlannedRepetitionService _plannedRepetitionService;

        public PlannedRepetitionController(IPlannedRepetitionService plannedRepetitionService)
        {
            _plannedRepetitionService = plannedRepetitionService;
        }

        [HttpGet("/{repetitionId}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int workoutId, [FromRoute] int repetitionId)
        {
            var repetition = await _plannedRepetitionService.GetSingle(userId, workoutId, repetitionId);
            if (repetition is null)
                return NotFound();
            return Ok(repetition);
        }

        [HttpDelete("/{repetitionId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int workoutId, [FromRoute] int repetitionId)
        {
            var result = await _plannedRepetitionService.DeleteRepetition(userId, workoutId, repetitionId);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }
    }
}
