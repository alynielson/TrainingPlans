using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Database.Models;
using TrainingPlans.Services;
using System.Net;
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
            try
            {
                var resultId = await _plannedWorkoutService.Create(workout, userId);
                return Ok(resultId);
            }
            catch (RestException ex)
            {
                return HandleRestException(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromRoute] int userId, [FromQuery] string from, [FromQuery] string to)
        {
            try
            {
                var plan = await _plannedWorkoutService.GetInDateRange(from, to, userId);
                return Ok(plan);
            }
            catch (RestException ex)
            {
                return HandleRestException(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult HandleRestException(RestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(exception.Message);
                default:
                    return BadRequest(exception.Message);
            }
        }
    }
}
