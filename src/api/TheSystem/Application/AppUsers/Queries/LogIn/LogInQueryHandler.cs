using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.AppUsers.Queries.LogIn;

public sealed class LogInQueryHandler : IQueryHandler<LogInQuery, AppUserViewModel>
{
    private readonly ISignInManager _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public LogInQueryHandler(
        ISignInManager signInManager, 
        IUserRepository userRepository,
        IMapper mapper,
        ITokenService tokenService)
    {
        _signInManager = signInManager;
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<Result<AppUserViewModel>> Handle(LogInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.UserName);

        if (user is null)
        {
            return Result<AppUserViewModel>
                .Fail().WithError($"User with username ${request.UserName} does not exist");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (signInResult.Succeeded == false)
        {
            return Result<AppUserViewModel>
                .Fail().WithError($"Wrong password for user {request.UserName}");
        }

        var tokenResult = await _tokenService.CreateToken(user);

        if (tokenResult.IsSuccess == false)
        {
            return Result<AppUserViewModel>
                .Fail().WithErrors(tokenResult.Errors);
        }

        var userViewModel = _mapper.Map<AppUserViewModel>(user);
        userViewModel.Token = tokenResult.Value;

        return Result<AppUserViewModel>.Success(userViewModel);
    }
}
