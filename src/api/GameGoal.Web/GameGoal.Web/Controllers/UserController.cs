using AutoMapper;
using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using GameGoal.Web.RequestModels;
using GameGoal.Web.Services.Abstractions;
using GameGoal.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGoal.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow;

        public UserController(
            IMapper mapper,
            ITokenService tokenService,
            IUnitOfWork uow
            )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
            => Ok(
                _mapper.Map<IEnumerable<UserViewModel>>(
                await _uow.UserRepository.GetUsersAsync()));

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> RegisterUser([FromBody] RegisterUserRequestModel registerUserRequest)
        {
            var existingUser = await _uow.UserRepository.GetUserByUsernameAsync(registerUserRequest.UserName);

            if (existingUser is not null)
            {
                return BadRequest("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(registerUserRequest);

            var result = await _uow.UserRepository.CreateUserAsync(newUser, registerUserRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }

            var userViewModel = _mapper.Map<UserViewModel>(newUser);
            var authResult = await _tokenService.CreateToken(newUser);

            if (!authResult.Success)
            {
                return BadRequest(string.Join(", ", authResult.Errors));
            }

            userViewModel.Token = authResult.Value;

            return Ok(userViewModel);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> LogIn([FromBody] LoginUserRequestModel loginUserRequest)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(loginUserRequest.UserName);

            if (user is null)
            {
                return BadRequest($"There is not user with username {loginUserRequest.UserName}");
            }

            var result = await _uow.SignInManager
                .CheckPasswordSignInAsync(user, loginUserRequest.Password ?? string.Empty, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("Wrong password");
            }

            var roles = await _uow.UserRepository.GetUserRoles(user);
            var userDto = _mapper.Map<UserViewModel>(user);
            var authResult = await _tokenService.CreateToken(user);

            if (!authResult.Success)
            {
                return BadRequest(string.Join(", ", authResult.Errors));
            }

            userDto.Token = authResult.Value;

            return Ok(userDto);
        }
    }
}
