using AutoMapper;
using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using GameGoal.Web.RequestModels.Goal;
using GameGoal.Web.Services.Abstractions;
using GameGoal.Web.ViewModels.Goal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGoal.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public sealed class GoalController : GameGoalControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGoalPrioritizer _goalPrioritizer;

        public GoalController(
            IMapper mapper,
            IUnitOfWork uow,
            IGoalPrioritizer goalPrioritizer)
        {
            _mapper = mapper;
            _uow = uow;
            _goalPrioritizer = goalPrioritizer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GoalViewModel>>> GetGoalsOfCurrentUser()
        {
            var currentUser = await _uow.UserRepository.GetUserWithGoalsById(GetUserId());

            if (currentUser is null)
            {
                return BadRequest($"User with id {GetUserId()} has not been found");
            }

            return Ok(_mapper.Map<IEnumerable<GoalViewModel>>(currentUser.Goals));
        }

        [HttpGet("best")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GoalViewModel>> GetBestGoal()
        {
            var currentUser = await _uow.UserRepository.GetUserWithGoalsById(GetUserId());

            if (currentUser is null)
            {
                return BadRequest($"User with id {GetUserId()} has not been found");
            }

            var bestGoalResult = _goalPrioritizer.AdviseBestGoal(currentUser);

            if (!bestGoalResult.Success)
            {
                return BadRequest(string.Join(", ", bestGoalResult.Errors));
            }

            return Ok(_mapper.Map<GoalViewModel>(bestGoalResult.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GoalViewModel>> CreateNewGoal([FromBody] CreateGoalRequestModel createGoalRequest)
        {
            var goal = _mapper.Map<Goal>(createGoalRequest);

            var currentUser = await _uow.UserRepository.GetUserWithGoalsById(GetUserId());

            if (currentUser is null)
            {
                return BadRequest($"User with id {GetUserId()} has not been found");
            }

            currentUser.Goals.Add(goal);

            return await _uow.ConfirmAsync()
                ? Ok(_mapper.Map<GoalViewModel>(goal))
                : BadRequest($"Could not add a goal to user {currentUser.UserName}");
        }
    }
}
