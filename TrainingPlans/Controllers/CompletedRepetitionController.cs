using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Services;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Controllers
{
    [Route("api/user/{userId}/completed/{workoutId}/repetition")]
    [ApiController]
    public class CompletedRepetitionController : ControllerBase
    {
        private readonly ICompletedRepetitionService _completedRepetitionService;

        public CompletedRepetitionController(ICompletedRepetitionService completedRepetitionService)
        {
            _completedRepetitionService = completedRepetitionService;
        }

        [HttpGet("/{repetitionId}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int workoutId, [FromRoute] int repetitionId)
        {
            var repetition = await _completedRepetitionService.GetSingle(userId, workoutId, repetitionId);
            if (repetition is null)
                return NotFound();
            return Ok(repetition);
        }

        [HttpDelete("/{repetitionId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int workoutId, [FromRoute] int repetitionId)
        {
            var result = await _completedRepetitionService.DeleteRepetition(userId, workoutId, repetitionId);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromRoute] int workoutId, [FromBody] CompletedRepetitionVM viewModel)
        {
            var result = await _completedRepetitionService.UpdateRepetition(userId, workoutId, viewModel);
            if (result.HasValue)
                return Ok(result);
            return NotFound();
        }

    }
}
