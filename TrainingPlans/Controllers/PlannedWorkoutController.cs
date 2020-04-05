﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Database.Models;
using TrainingPlans.Services;
using System.Net;
using TrainingPlans.ViewModels;
using TrainingPlans.ExceptionHandling;
using FluentValidation;
using TrainingPlans.ViewModels.Validators;

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
        public async Task<IActionResult> GetWorkouts([FromRoute] int userId, [FromQuery] string from, [FromQuery] string to)
        {
            var plan = await _plannedWorkoutService.GetInDateRange(from, to, userId);
            return Ok(plan);
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
